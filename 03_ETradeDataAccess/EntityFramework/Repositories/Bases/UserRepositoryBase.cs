using System;
using System.Collections.Generic;
using System.Text;
using AppCore.Repository;
using ETradeEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETradeDataAccess.EntityFramework.Repositories.Bases
{
    public abstract class UserRepositoryBase : RepositoryBase<User>
    {
        protected UserRepositoryBase(DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
