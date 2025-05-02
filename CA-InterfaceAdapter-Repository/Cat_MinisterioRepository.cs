using CA_ApplicationLayer.CAT_Ministerio;
using CA_ApplicationLayer.Departamentos;
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
    public class Cat_MinisterioRepository : ICat_MinisterioRepository<CatMinisterioModel>
    {
        private readonly AppDbContext _dbContext;

        public Cat_MinisterioRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CatMinisterioModel>> GetAllAsync()
        {
            var query = _dbContext.Ministerio.AsQueryable();

            var lstItem = await query.ToListAsync();

            return lstItem;
        }

        public Task<ItemsPaginatorEntity<CatMinisterioModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch)
        {
            throw new NotImplementedException();
        }

        public Task<CatMinisterioModel> GetById()
        {
            throw new NotImplementedException();
        }
    }
}
