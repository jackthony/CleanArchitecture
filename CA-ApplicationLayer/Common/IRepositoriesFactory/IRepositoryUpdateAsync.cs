using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Common.IRepositoriesFactory
{
    public interface IRepositoryUpdateAsync<TEntity>
    {
        Task UpdateAsync(TEntity entity);
    }
}
