using Entities.Location;
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
        public CityRepository(ApplicationContext context) : base(context)
        {
        }

        public ApplicationContext ApplicationContext
        {
            get { return Context as ApplicationContext; }
        }
    }
}
