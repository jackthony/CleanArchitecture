using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository
{
    public class EMP_DietaRepository : IEmp_DietaRepository
    {

        private readonly AppDbContext _dbContext;
        public EMP_DietaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


 

        public async Task<EMP_DietaModel> GetByRucAndCargo(string ruc, int cargo)
        {
            var dieta = await _dbContext.Dietas.FirstOrDefaultAsync(e => e.sRUC == ruc && e.nTipoCargo == cargo);

            return dieta;
        }
    }
}
