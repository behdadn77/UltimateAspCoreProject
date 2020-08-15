using Data.Core.Entities.Location;
using Data.Core.Repositories.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Persitence.Repositories.Location
{
    public class StateRespository : Repository<State>, IStateRepository
    {
        public StateRespository(DbContext context) : base(context)
        {
        }

        public State GetStateWithCities(int id)
        {
            return DBContext.States.Include(a => a.Country).SingleOrDefault(a => a.Id == id);
        }

        public DBContext DBContext
        {
            get { return Context as DBContext; }
        }
    }
}
