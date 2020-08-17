using Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace Data.Core.Repositories.Location
{
    public interface IStateRepository :IRepository<State>
    {
        State GetStateWithCities(int id);
        IPagedList<State> GetStatesWithCities(int pageIndex, int pageSize);
    }
}
