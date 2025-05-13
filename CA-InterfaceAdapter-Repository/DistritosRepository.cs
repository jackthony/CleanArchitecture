using CA_ApplicationLayer.Distritos;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository
{
    public class DistritosRepository : IDistritosRepository<DistritosModel>
    {
        private readonly AppDbContext _dbContext;

        public DistritosRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DistritosModel>> GetAllAsync(string provinceCode)
        {
            var query = _dbContext.Distritos
                          .Where(p => p.sProvinceCode == provinceCode)
                          .AsQueryable();

            var lstItem = await query.ToListAsync();

            return lstItem;
        }
    }
}
