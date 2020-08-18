using Entities.Location;
using Data.Core.Repositories.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Data.Persitence.Repositories.Location
{
    public class StateRespository : Repository<State>, IStateRepository
    {
        public StateRespository(DbContext context) : base(context)
        {
        }

        public State GetStateWithCities(int id)
        {
            return ApplicationContext.States.Include(x => x.Country).OrderBy(x => x.Name)
                .SingleOrDefault(x => x.Id == id);
        }

        public IPagedList<State> GetStatesWithCities(int pageIndex, int pageSize = 10)
        {
            return ApplicationContext.States.Include(x => x.Cities).OrderBy(x => x.Name).
                ToPagedList(pageIndex, pageSize);
        }

        public ApplicationContext ApplicationContext
        {
            get { return Context as ApplicationContext; }
        }
    }

}
