using Data.Core.Repositories.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        IStateRepository States { get; }
        ICountryRepository Countries { get; }
        int Save();
        Task<int> SaveAsync();
    }
}
