using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ETradeBusiness.Models;
using ETradeBusiness.Services.Bases;
using ETradeDataAccess.EntityFramework.Repositories.Bases;

namespace ETradeBusiness.Services
{
    public class LocationService : ILocationService
    {
        private readonly CountryRepositoryBase _countryRepository;

        public LocationService(CountryRepositoryBase countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public void Add(CountryModel model, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public CountryModel GetById(int id)
        {
            try
            {
                return GetQuery().SingleOrDefault(country => country.Id == id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public IQueryable<CountryModel> GetQuery(Expression<Func<CountryModel, bool>> predicate = null)
        {
            try
            {
                return _countryRepository.GetEntityQuery("Cities").OrderBy(c => c.Name).Select(country =>
                    new CountryModel()
                    {
                        Id = country.Id,
                        Guid = country.Guid,
                        Name = country.Name,
                        Cities = country.Cities.Select(city => new CityModel()
                        {
                            Id = city.Id,
                            Guid = city.Guid,
                            Name = city.Name,
                            CountryId = city.CountryId
                        }).ToList()
                    });
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(CountryModel model, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
