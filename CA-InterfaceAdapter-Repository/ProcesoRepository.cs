using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_ApplicationLayer.Usuarios;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository
{
    public class ProcesoRepository : IProcesoRepository
    {
        private readonly AppDbContext _dbContext;

        public ProcesoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProcesoModel>> GetAllAsync()
        {
            return await _dbContext.Procesos
                .Where(p => p.bActivo && !p.bEliminado)
                .OrderBy(p => p.nIdProceso)
                .ToListAsync();
        }

        public async Task<ItemsPaginatorEntity<ProcesoModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch)
        {
            var query = _dbContext.Procesos
                .Where(p => p.bActivo && !p.bEliminado);

            if (!string.IsNullOrWhiteSpace(paramSearch))
            {
                string paramLower = paramSearch.Trim().ToLower();
                query = query.Where(p =>
                    EF.Functions.Like(p.sNombreProceso.ToLower(), $"%{paramLower}%") ||
                    EF.Functions.Like(p.sDescripcion!.ToLower(), $"%{paramLower}%"));
            }

            var totalRows = await query.CountAsync();

            var lstItem = await query
                .OrderBy(p => p.nIdProceso)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ItemsPaginatorEntity<ProcesoModel>()
            {
                lstItem = lstItem,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows
            };
        }

        public async Task<ProcesoModel> GetById(int id)
        {
            var proceso = await _dbContext.Procesos
                .FirstOrDefaultAsync(p => p.nIdProceso == id && p.bActivo && !p.bEliminado);

            if (proceso == null)
                throw new KeyNotFoundException($"Proceso con id {id} no encontrado.");

            return proceso;
        }
    }
}
