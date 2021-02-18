using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETradeBusiness.Models;
using ETradeBusiness.Services.Bases;
using ETradeDataAccess.EntityFramework.Repositories.Bases;

namespace ETradeBusiness.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepositoryBase _categoryRepository;

        public CategoryService(CategoryRepositoryBase categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Add(CategoryModel model, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public CategoryModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<CategoryModel> GetQuery(
            System.Linq.Expressions.Expression<Func<CategoryModel, bool>> predicate = null)
        {
            try
            {
                return _categoryRepository.GetEntityQuery().OrderBy(c => c.Name).Select(c => new CategoryModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Guid = c.Guid
                });
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
                return _categoryRepository.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(CategoryModel model, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
