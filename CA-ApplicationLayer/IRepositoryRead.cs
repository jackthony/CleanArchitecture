using CA_EntrerpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public interface IRepositoryRead<TModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetById(int id);
        Task<ItemsPaginatorEntity<TModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch);

    }
}
