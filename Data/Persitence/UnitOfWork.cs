using Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Persistence
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly DBContext context;

        public UnitOfWork(DBContext context)
        {
            this.context = context;
        }

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
