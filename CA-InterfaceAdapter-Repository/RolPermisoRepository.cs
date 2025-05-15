using CA_ApplicationLayer.Usuarios;
using CA_InterfaceAdapters_Data;
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
        public async Task<IEnumerable<RolPermisoModel>> GetPermisosPorRolAsync(int nIdRol)
        {
            var query = _dbContext.RolPermisos.AsQueryable().Where(data => data.nIdRol == nIdRol);

            var lstItem = await query.ToListAsync();

            return lstItem;
        }
    }
}
