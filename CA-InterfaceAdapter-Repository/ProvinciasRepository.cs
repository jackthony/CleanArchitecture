using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.Provincias;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapter_Repository
{
    public class ProvinciasRepository : IProvinciasRepository<ProvinciasModel>
    {
        private readonly AppDbContext _dbContext;

        public ProvinciasRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProvinciasModel>> GetAllAsync(string departmentCode)
        {
            var query = _dbContext.Provincias
                          .Where(p => p.sDepartmentCode == departmentCode)
                          .AsQueryable();

            var lstItem = await query.ToListAsync();

            return lstItem;
        }
    }
}
