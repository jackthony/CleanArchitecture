using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Adapters
{
    public interface IExternalService<T>
    {
        public Task<IEnumerable<T>> GetContentAsync();
    }
}
