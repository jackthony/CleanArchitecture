using CA_ApplicationLayer;
using CA_ApplicationLayer.Departamentos;
using CA_EntrerpriseLayer;
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
    public class DepartamentosRepository: IDepartamentosRepository<DepartmentosModel>
    {
        private readonly AppDbContext _dbContext;

        public DepartamentosRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DepartmentosModel>> GetAllAsync()
        {
            var query = _dbContext.Departmentos.AsQueryable();

            var lstItem = await query.ToListAsync();

            return lstItem;
        }

        public Task<ItemsPaginatorEntity<DepartmentosModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentosModel> GetById()
        {
            throw new NotImplementedException();
        }
    }
}
