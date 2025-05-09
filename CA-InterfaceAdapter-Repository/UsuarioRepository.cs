using CA_ApplicationLayer.Usuarios;
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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(UsuarioEntity entity)
        {
            var model = new UsuarioModel
            {
                sNombresApellidos = entity.sNombresApellidos,
                sContrasena = entity.sContrasena,
                nIdCargo = entity.nIdCargo,
                nIdRol = entity.nIdRol,
                sCorreoElectronico = entity.sCorreoElectronico,
                bActivo = entity.bActivo,
                dtFechaRegistro = entity.dtFechaRegistro,
                sUsuarioRegistro = entity.sUsuarioRegistro
            };

            await _dbContext.Usuarios.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.nIdUsuario;
        }

        public async Task<bool> UpdateAsync(UsuarioEntity entity)
        {
            var model = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.nIdUsuario == entity.nIdUsuario);
            if (model == null) return false;

            model.sContrasena = entity.sContrasena;
            model.nIdCargo = entity.nIdCargo;
            model.nIdRol = entity.nIdRol;
            model.sCorreoElectronico = entity.sCorreoElectronico;
            model.bActivo = entity.bActivo;
            model.dtFechaModificacion = entity.dtFechaModificacion;
            model.sUsuarioModificacion = entity.sUsuarioModificacion;

            _dbContext.Usuarios.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.nIdUsuario == id);
            if (model == null) return false;

            _dbContext.Usuarios.Remove(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync(string? nombreUsuario = null)
        {
            var query = _dbContext.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                query = query.Where(u => u.sNombresApellidos.Contains(nombreUsuario));
            }

            return await query.OrderBy(u => u.nIdUsuario).ToListAsync();
        }

        public async Task<UsuarioModel?> GetByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.nIdUsuario == id);
        }

        public async Task<bool> ExistsAsync(string nombreUsuario)
        {
            return await _dbContext.Usuarios.AnyAsync(u => u.sNombresApellidos == nombreUsuario);
        }

        public Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemsPaginatorEntity<UsuarioModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? paramSearch)
        {
            var query = _dbContext.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(paramSearch))
            {
                query = query.Where(u => u.sNombresApellidos.Contains(paramSearch));
            }


            var totalRows = await query.CountAsync();
                       var lstItem = await query
                           .OrderBy(e => e.nIdUsuario)
                           .Skip((pageIndex-1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync();
                       
            return new ItemsPaginatorEntity<UsuarioModel>()
            {
                lstItem = lstItem,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows
            };
        }
    }
}
