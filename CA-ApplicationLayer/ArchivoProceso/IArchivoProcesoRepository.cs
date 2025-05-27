using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;

namespace CA_ApplicationLayer.ArchivoProceso
{
    public interface IArchivoProcesoRepository
    {
        Task<int> AddAsync(ArchivoProcesoEntity entity, CancellationToken ct = default);
        Task<bool> UpdateAsync(ArchivoProcesoEntity entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);

        Task<ArchivoProcesoModel?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<ItemsPaginatorEntity<ArchivoProcesoModel>> GetAllAsyncPagination(
            int pageIndex, int pageSize, int nIdEntidadRelacionada, CancellationToken ct = default);
    }

}
