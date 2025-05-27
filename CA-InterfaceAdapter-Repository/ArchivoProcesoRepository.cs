using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_ApplicationLayer;
using CA_ApplicationLayer.ArchivoProceso;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository
{

    public class ArchivoProcesoRepository : IArchivoProcesoRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper<ArchivoProcesoEntity, ArchivoProcesoModel> _mapper;

        public ArchivoProcesoRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> AddAsync(ArchivoProcesoEntity e, CancellationToken ct = default)
        {

            var model= new ArchivoProcesoModel
            {
                nIdArchivoProceso = e.nIdArchivoProceso,
                nIdEntidadRelacionada = e.nIdEntidadRelacionada,
                sRutaFisica = e.sRutaFisica,
                sExtension = e.sExtension,
                //sTipoDocumento = e.sTipoDocumento,
                sDescripcion = e.sDescripcion,
                dtFechaCreacion = e.dtFechaCreacion,
                nIdUsuarioCreacion = e.nIdUsuarioCreacion,
                dtFechaModif = e.dtFechaModif,
                nIdUsuarioModif = e.nIdUsuarioModif,
                bEliminado = e.bEliminado
            };

            _ctx.ArchivoProcesos.Add(model);

            await _ctx.SaveChangesAsync(ct);
            return model.nIdArchivoProceso;
        }

        public async Task<bool> UpdateAsync(ArchivoProcesoEntity entity, CancellationToken ct = default)
        {
            var model = await _ctx.ArchivoProcesos
                                  .FirstOrDefaultAsync(x => x.nIdArchivoProceso == entity.nIdArchivoProceso, ct);

            if (model is null) return false;

            // mapear campos editables
            model.sDescripcion = entity.sDescripcion;
            //model.sTipoDocumento = entity.sTipoDocumento;
            model.dtFechaModif = entity.dtFechaModif;
            model.nIdUsuarioModif = entity.nIdUsuarioModif;

            _ctx.ArchivoProcesos.Update(model);
            return await _ctx.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var model = await _ctx.ArchivoProcesos.FirstOrDefaultAsync(x => x.nIdArchivoProceso == id, ct);
            if (model is null) return false;

            model.bEliminado = true;
            model.dtFechaModif = DateTime.UtcNow;
            return await _ctx.SaveChangesAsync(ct) > 0;
        }

        public Task<ArchivoProcesoModel?> GetByIdAsync(int id, CancellationToken ct = default)
            => _ctx.ArchivoProcesos.FirstOrDefaultAsync(x => x.nIdArchivoProceso == id && !x.bEliminado, ct);

        public async Task<ItemsPaginatorEntity<ArchivoProcesoModel>> GetAllAsyncPagination(
            int pageIndex, int pageSize, int nIdEntidadRelacionada, CancellationToken ct = default)
        {
            var qry = _ctx.ArchivoProcesos
                          .Where(x => x.nIdEntidadRelacionada == nIdEntidadRelacionada && !x.bEliminado)
                          .OrderByDescending(x => x.dtFechaCreacion);

            var total = await qry.CountAsync(ct);
            var items = await qry.Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync(ct);

            return new ItemsPaginatorEntity<ArchivoProcesoModel>
            {
                lstItem = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = total
            };
        }
    }
}
