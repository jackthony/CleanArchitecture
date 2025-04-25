using CA_EntrerpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public interface IRepository<TTEntity, TModel>
    {
        Task<TModel> GetByIdAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<ItemsPaginatorEntity<TModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch);
        Task<int> AddAsync(TTEntity entity);
        Task<bool> UpdateAsync(TTEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

    }
}
