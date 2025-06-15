using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Common.IRepositoriesFactory
{
    public interface IRepositoryDeleteAsync<TEntity>
    {
        Task DeleteAsync(TEntity entity);
    }
}
