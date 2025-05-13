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
    public class ConstanteRepository : IConstanteRepository<ConstanteModel>
    {
        private readonly AppDbContext _dbContext;

        public ConstanteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ConstanteModel>> GetAllAsync(int nConCodigo)
        {
            var query = _dbContext.Constante
                          .Where(p => p.nConCodigo == nConCodigo)
                          .AsQueryable();

            var lstItem = await query.ToListAsync();

            return lstItem;
        }
    }
}
