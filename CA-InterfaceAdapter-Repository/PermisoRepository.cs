using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CA_ApplicationLayer.Usuarios;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly AppDbContext _dbContext;

        public PermisoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PermisoModel>> GetAllAsync()
        {
            return await _dbContext.Permisos
                .Where(p => p.bActivo && !p.bEliminado)
                .OrderBy(p => p.nIdPermiso)
                .ToListAsync();
        }

        public async Task<ItemsPaginatorEntity<PermisoModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch)
        {
            var query = _dbContext.Permisos
                .Where(p => p.bActivo && !p.bEliminado);

            if (!string.IsNullOrWhiteSpace(paramSearch))
            {
                string paramLower = paramSearch.Trim().ToLower();
                query = query.Where(p =>
                    EF.Functions.Like(p.sNombrePermiso.ToLower(), $"%{paramLower}%") ||
                    EF.Functions.Like(p.sDescripcion!.ToLower(), $"%{paramLower}%"));
                    //EF.Functions.Like(p.Proceso.sNombreProceso.ToLower(), $"%{paramLower}%"));
            }

            var totalRows = await query.CountAsync();

            var lstItem = await query
                .OrderBy(p => p.nIdPermiso)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ItemsPaginatorEntity<PermisoModel>()
            {
                lstItem = lstItem,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows
            };
        }

        public async Task<PermisoModel> GetById(int id)
        {
            var permiso = await _dbContext.Permisos
                .FirstOrDefaultAsync(p => p.nIdPermiso == id && p.bActivo && !p.bEliminado);

            if (permiso == null)
                throw new KeyNotFoundException($"Permiso con id {id} no encontrado.");

            return permiso;
        }
    }
}
