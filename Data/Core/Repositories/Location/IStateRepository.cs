using Data.Core.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Core.Repositories.Location
{
    public interface IStateRepository :IRepository<State>
    {
        State GetStateWithCities(int id);
    }
}
