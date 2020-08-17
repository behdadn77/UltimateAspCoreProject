using Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace Data.Core.Repositories.Location
{
    public interface ICountryRepository : IRepository<Country>
    {
        Country GetCountryWithStates(int id);
        Country GetCountryWithCities(int id);
        IPagedList<Country> GetCountriesWithStates(int pageIndex, int pageSize);
        IPagedList<Country> GetCountriesWithCities(int pageIndex, int pageSize);
    }
}
