using System;
using System.Collections.Generic;
using System.Text;
using ETradeDataAccess.EntityFramework.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace ETradeDataAccess.EntityFramework.Repositories
{
    public class ProductRepository : ProductRepositoryBase
    {
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
