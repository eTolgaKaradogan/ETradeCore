using System;
using System.Collections.Generic;
using System.Text;
using AppCore.Business.Bases;
using ETradeBusiness.Models;
using ETradeEntities.Entities;

namespace ETradeBusiness.Services.Bases
{
    public interface ICategoryService : IService<Category, CategoryModel>
    {
    }
}
