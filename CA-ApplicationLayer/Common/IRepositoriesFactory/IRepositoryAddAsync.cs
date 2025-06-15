using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Common.IRepositoriesFactory
{
    public interface IRepositoryAddAsync<T>
    {
        Task<int> AddAsync(T entity);
    }
}
