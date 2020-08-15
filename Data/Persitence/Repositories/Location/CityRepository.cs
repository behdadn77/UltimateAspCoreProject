using Data.Core.Entities.Location;
using Data.Core.Repositories.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Persitence.Repositories.Location
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(DBContext context) : base(context)
        {
        }

        public DBContext DBContext
        {
            get { return Context as DBContext; }
        }
    }
}
