using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Repository
{
    public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : RecordBase, new()
    {
        private readonly DbContext db;

        protected RepositoryBase(DbContext dbContext)
        {
            db = dbContext;
        }

        // Herhangi bir filtre olmadan tüm entity'leri liste olarak döner
        public virtual List<TEntity> GetEntities(params string[] entitiesToInclude)
        {
            try
            {
                return GetEntityQuery(entitiesToInclude).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen lambda expression'a göre entity'leri liste olarak döner 
        public virtual List<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude)
        {
            try
            {
                return GetEntityQuery(entitiesToInclude).Where(predicate).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Entity için herhangi bir kayıt döndürmeyen bir LINQ query oluşturur ve bu query'i döner
        public virtual IQueryable<TEntity> GetEntityQuery()
        {
            try
            {
                return db.Set<TEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Lazy loading yapılmayan durumlarda entity için herhangi bir kayıt döndürmeyen bir LINQ query oluşturur, parametre olarak gönderilen bu entity'ye bağlı entity'leri query'ye ekler ve bu query'i döner
        public virtual IQueryable<TEntity> GetEntityQuery(params string[] entitiesToInclude)
        {
            try
            {
                var entityQuery = db.Set<TEntity>().AsQueryable();
                foreach (string entityToInclude in entitiesToInclude)
                {
                    entityQuery = entityQuery.Include(entityToInclude);
                }
                return entityQuery.AsQueryable();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen lambda expression'a göre entity için kayıt döndürmeyen bir LINQ query oluşturur ve bu query'i döner 
        public virtual IQueryable<TEntity> GetEntityQuery(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude)
        {
            try
            {
                var entityQuery = GetEntityQuery(entitiesToInclude);
                return db.Set<TEntity>().AsQueryable().Where(predicate);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Primary key olan ID'ye göre tek bir entity döner.
        public virtual TEntity GetEntityById(int id, params string[] entitiesToInclude)
        {
            try
            {
                return db.Set<TEntity>().Find(id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen lambda expression'a göre entity için kayıt döndürmeyen bir LINQ  query oluşturur ve bu query'i döner.
        public virtual TEntity GetEntity(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return db.Set<TEntity>().SingleOrDefault(predicate);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen entity'i DB set'e ekler
        public virtual void AddEntity(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        // Parametre olarak gönderilen entity'i DB set'te güncellemek için DB set'teki entity'nin durumunu değiştirir
        public virtual void UpdateEntity(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }

        // Primary key olan ID'ye sahip entity'i DB set'ten siler. Eğer entity'de IsDeleted adında bir özellik varsa entity'i silmez, IsDeleted'ı true olarak günceller
        public virtual void DeleteEntity(int id)
        {
            var entity = GetEntityById(id);
            DeleteEntity(entity);
        }

        // Parametre olarak gönderilen entity'i DB set'ten siler. Eğer entity'de IsDeleted adında bir özellik varsa entity'i silmez, IsDeleted'ı true olarak günceller  
        public virtual void DeleteEntity(TEntity entity)
        {

            db.Set<TEntity>().Remove(entity);
            
        }

        // Unit of Work için SaveChanges() methodu: Repository üzerindeki değişikliklerin veritabanına kaydedildiği method
        public virtual int SaveChanges()
        {
            try
            {
                return db.SaveChanges();

            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            db.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
