using CA_ApplicationLayer.Usuarios;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapter_Repository
{
    public class RolPermisoRepository : IRolPermisoRepository
    {
        private readonly AppDbContext _dbContext;
        public RolPermisoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PermisosPorRolEntity>> GetPermisosPorRolAsync(int nIdRol)
        {
            var query = from rp in _dbContext.RolPermisos
                        join p in _dbContext.Permisos on rp.nIdPermiso equals p.nIdPermiso
                        join pr in _dbContext.Procesos on p.nIdProceso equals pr.nIdProceso
                        where rp.nIdRol == nIdRol && p.bActivo && !p.bEliminado && pr.bActivo && !pr.bEliminado
                        select new { pr.sNombreProceso, p.sNombrePermiso };

            var list = await query.ToListAsync();

            var grouped = list
                .GroupBy(x => x.sNombreProceso)
                .Select(g => new PermisosPorRolEntity
                {
                    Module = g.Key,
                    Actions = g.Select(p => p.sNombrePermiso).Distinct().ToList()
                }).ToList();

            return grouped;
        }
    }
}
