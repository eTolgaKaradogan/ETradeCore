using System;
using System.Collections.Generic;
using System.Text;
using AppCore.Repository;
using ETradeEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETradeDataAccess.EntityFramework.Repositories.Bases
{
    public abstract class ProductRepositoryBase : RepositoryBase<Product> // Base olduğu için abstract tanımlamak çok güzel olur.
    {
        protected ProductRepositoryBase(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
