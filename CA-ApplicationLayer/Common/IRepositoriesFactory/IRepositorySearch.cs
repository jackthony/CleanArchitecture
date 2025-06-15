using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Common.IRepositoriesFactory
{
    public interface IRepositorySearch <TModel, TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TModel, bool>> predicate);
    }
}
