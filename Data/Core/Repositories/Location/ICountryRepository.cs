using Data.Core.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Core.Repositories.Location
{
    public interface ICountryRepository : IRepository<Country>
    {
        Country GetCountryWithStates(int id);
        Country GetCountryWithCities(int id);
    }
}
