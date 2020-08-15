using Data.Core.Entities.Location;
using Data.Core.Repositories.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Persitence.Repositories.Location
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(DbContext context) : base(context)
        {
        }

        public Country GetCountryWithStates(int id)
        {
            return DBContext.Countries.Include(a => a.States).SingleOrDefault(a => a.Id == id);
        }

        public Country GetCountryWithCities(int id)
        {
            return DBContext.Countries.Include(a => a.States).ThenInclude(a => a.Cities).SingleOrDefault(a => a.Id == id);
        }

        public DBContext DBContext
        {
            get { return Context as DBContext; }
        }
    }
}
