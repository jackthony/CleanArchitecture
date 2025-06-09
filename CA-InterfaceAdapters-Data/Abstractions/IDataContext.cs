using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Data.Abstractions
{
    public interface IDataContext : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
