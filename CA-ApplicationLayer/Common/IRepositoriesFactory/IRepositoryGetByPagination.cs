using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Common.IRepositoriesFactory
{
    public interface IRepositoryGetByPagination<T>
    {
        IQueryable<T> GetByPagination(
            int pageNumber,
            int pageSize,
            out int totalCount,
            Func<IQueryable<T>, IQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? filter = null);
    }
}
