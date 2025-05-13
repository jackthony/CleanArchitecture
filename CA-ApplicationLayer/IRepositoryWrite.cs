using CA_EntrerpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public interface IRepositoryWrite<TTEntity>
    {
        Task<int> AddAsync(TTEntity entity);
        Task<bool> UpdateAsync(TTEntity entity);
        Task<bool> DeleteAsync(int id);

    }
}
