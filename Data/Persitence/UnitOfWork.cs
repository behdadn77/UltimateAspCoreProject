using Data.Core;
using Data.Core.Repositories.Location;
using Data.Persitence.Repositories.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext context;

        public UnitOfWork(DBContext context)
        {
            this.context = context;
            Cities = new CityRepository(this.context);
            States = new StateRespository(this.context);
            Countries = new CountryRepository(this.context);
        }

        public ICityRepository Cities { get; private set; }
        public IStateRepository States { get; private set; }
        public ICountryRepository Countries { get; private set; }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
