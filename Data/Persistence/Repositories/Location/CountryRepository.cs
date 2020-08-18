using Entities.Location;
using Data.Core.Repositories.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Data.Persitence.Repositories.Location
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(DbContext context) : base(context)
        {
        }

        public Country GetCountryWithStates(int id)
        {
            return ApplicationContext.Countries.Include(x => x.States).OrderBy(x => x.Name)
                .SingleOrDefault(x => x.Id == id);
        }

        public Country GetCountryWithCities(int id)
        {
            return ApplicationContext.Countries.Include(x => x.States).ThenInclude(x => x.Cities).OrderBy(x => x.Name)
                .SingleOrDefault(x => x.Id == id);
        }

        public IPagedList<Country> GetCountriesWithStates(int pageIndex, int pageSize = 10)
        {
            return ApplicationContext.Countries.Include(x => x.States).OrderBy(x => x.Name)
                .ToPagedList(pageIndex, pageSize);
        }
        public IPagedList<Country> GetCountriesWithCities(int pageIndex, int pageSize = 10)
        {
            return ApplicationContext.Countries.Include(x => x.States).ThenInclude(x=>x.Cities).OrderBy(x => x.Name)
                .ToPagedList(pageIndex, pageSize);
        }

        public ApplicationContext ApplicationContext
        {
            get { return Context as ApplicationContext; }
        }
    }
}
