using System;
using System.Collections.Generic;
using System.Text;
using AppCore.Repository;
using ETradeEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETradeDataAccess.EntityFramework.Repositories.Bases
{
    public abstract class CityRepositoryBase : RepositoryBase<City>
    {
        protected CityRepositoryBase(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
