using System;
using System.Collections.Generic;
using System.Text;
using ETradeDataAccess.EntityFramework.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace ETradeDataAccess.EntityFramework.Repositories
{
    public class CategoryRepostory : CategoryRepositoryBase
    {
        public CategoryRepostory(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
