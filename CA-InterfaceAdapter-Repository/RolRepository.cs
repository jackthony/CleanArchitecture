    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CA_ApplicationLayer.EMP_Empresa;
    using CA_ApplicationLayer.Usuarios;
    using CA_EntrerpriseLayer;
    using CA_InterfaceAdapters_Data;
    using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

    namespace CA_InterfaceAdapter_Repository
    {
        public class RolRepository : IRolRepository
        {

            private readonly AppDbContext _dbContext;
            public RolRepository(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }
        public async Task<IEnumerable<RolModel>> GetAllAsync()
        {
            return await _dbContext.Roles
                .Where(r => r.bActivo && !r.bEliminado)
                .OrderBy(r => r.nIdRol)
                .ToListAsync();
        }

        public async Task<ItemsPaginatorEntity<RolModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch)
        {
            var query = _dbContext.Roles
                .Where(r => r.bActivo && !r.bEliminado);

            if (!string.IsNullOrWhiteSpace(paramSearch))
            {
                string paramLower = paramSearch.Trim().ToLower();
                query = query.Where(r =>
                    EF.Functions.Like(r.sNombreRol.ToLower(), $"%{paramLower}%") ||
                    EF.Functions.Like(r.sDescripcion!.ToLower(), $"%{paramLower}%"));
            }

            var totalRows = await query.CountAsync();
            List<RolModel> lstItem;

            if (pageSize == 0)
            {
                // Traer todos los registros sin paginar
                lstItem = await query.OrderBy(r => r.nIdRol).ToListAsync();
                pageIndex = 1;
                pageSize = totalRows;
            }
            else
            {
                // Paginación normal
                lstItem = await query
                    .OrderBy(r => r.nIdRol)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            //var lstItem = await query
            //    .OrderBy(r => r.nIdRol)
            //    .Skip((pageIndex - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            return new ItemsPaginatorEntity<RolModel>()
            {
                lstItem = lstItem,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows
            };
        }

        public async Task<RolModel> GetById(int id)
        {
            var rol = await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.nIdRol == id && r.bActivo && !r.bEliminado);

            if (rol == null)
                throw new KeyNotFoundException($"Rol con id {id} no encontrado.");

            return rol;
        }
    }
    }
