using ETradeBusiness.Models;
using ETradeBusiness.Services.Bases;
using ETradeDataAccess.EntityFramework.Repositories.Bases;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using ETradeEntities.Entities;

namespace ETradeBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepositoryBase _productRepository;

        public ProductService(ProductRepositoryBase productRepository)
        {
            _productRepository = productRepository;
        }
        public void Add(ProductModel model, bool saveChanges = true)
        {
            try
            {
                var entity = new Product()
                {
                    CategoryId = model.CategoryId,
                    CreateDate = model.CreateDate,
                    Guid = Guid.NewGuid().ToString(),
                    IsDeleted = false,
                    Name = model.Name.Trim(),
                    StockAmount = model.StockAmount,
                    UnitPrice = model.UnitPrice
                };
                _productRepository.AddEntity(entity);

                if (saveChanges) // birden fazla kayıt eklemek için 
                {
                    _productRepository.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            try
            {
                _productRepository.DeleteEntity(id);
                if (saveChanges)
                {
                    SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public ProductModel GetById(int id)
        {
            /* select Products.Id, Products.Guid, Products.CategoryId, Products.CreateDate, Products.Name as DisplayCategoryName, Products.ImagePath,
             Products.Name, Products.StockAmount, Products.UnitPrice, Products.UpdateDate from Products left outer join Category on Products.CategoryId = CategoryId*/
            try
            {
                return GetQuery().SingleOrDefault(p => p.Id == id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public IQueryable<ProductModel> GetQuery(Expression<Func<ProductModel, bool>> predicate = null)
        {
            try
            {
                // AutoMapper 
                var query = _productRepository.GetEntityQuery(p => !p.IsDeleted,"Category").Select(p => new ProductModel()
                {
                    Id = p.Id,
                    Guid = p.Guid,
                    CategoryId = p.CategoryId,
                    CreateDate = p.CreateDate,
                    DisplayCategoryName = p.Category.Name,
                    ImagePath = p.ImagePath,
                    Name = p.Name,
                    StockAmount = p.StockAmount,
                    UnitPrice = p.UnitPrice,
                    UpdateDate = p.UpdateDate,
                    DisplayUpdateDate = p.UpdateDate.HasValue ? p.UpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""
                });
                return query;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return _productRepository.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(ProductModel model, bool saveChanges = true)
        {
            try
            {
                var entity = _productRepository.GetEntityById(model.Id);
                entity.CategoryId = model.CategoryId;
                entity.UpdateDate = DateTime.Now;
                entity.Name = model.Name.Trim();
                entity.UnitPrice = model.UnitPrice;
                entity.StockAmount = model.StockAmount;
                _productRepository.UpdateEntity(entity);
                if (saveChanges)
                {
                    SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
