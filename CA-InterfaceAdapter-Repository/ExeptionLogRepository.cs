using CA_ApplicationLayer.ExceptionLog;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapter_Repository
{
    public class ExeptionLogRepository : IExeptionLogRepository
    {
        private readonly AppDbContext _dbContext;

        public ExeptionLogRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(ExeptionLogEntity entity)
        {
            var model = new ExeptionLogModel
            {
                Code = entity.Code,
                Mensaje = entity.Mensaje,
                FechaRegistro = DateTime.Now,
            };

            await _dbContext.ExeptionsLogs.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.IdLog;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ExeptionLogEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
