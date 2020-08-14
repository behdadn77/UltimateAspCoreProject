using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Core
{
    interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
