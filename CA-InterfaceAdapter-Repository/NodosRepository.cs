using CA_ApplicationLayer.Nodos;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapter_Repository
{
    public class NodosRepository : INodoRepository
    {
        private readonly AppDbContext _dbContext;
        public NodosRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> AddAsync(NodoEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NodoModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NodoModel>> GetByEnterpriseAsync(int idEmpresa, Guid? idCarpetaPadre = null)
        {
            var idEmpresaParam = new SqlParameter("@IdEmpresa", idEmpresa);
            var idCarpetaParam = idCarpetaPadre.HasValue
                ? new SqlParameter("@IdCarpetaPadre", idCarpetaPadre.Value)
                : new SqlParameter("@IdCarpetaPadre", DBNull.Value);

            var nodos = await _dbContext
                .Set<NodoModel>()
                .FromSqlRaw("EXEC sp_ListarContenidoCarpeta @IdEmpresa, @IdCarpetaPadre", idEmpresaParam, idCarpetaParam)
                .AsNoTracking()
                .ToListAsync();

            return nodos;
        }

        public Task<ItemsPaginatorEntity<NodoModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch)
        {
            throw new NotImplementedException();
        }

        public Task<NodoModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(NodoEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
