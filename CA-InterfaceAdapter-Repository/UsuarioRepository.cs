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
                sApellidoPaterno = entity.sApellidoPaterno,
                sApellidoMaterno = entity.sApellidoMaterno,
                sNombres = entity.sNombres,
                sContrasena = entity.sContrasena,
                nIdCargo = entity.nIdCargo,
                nIdRol = entity.nIdRol,
                sCorreoElectronico = entity.sCorreoElectronico,
                nEstado = entity.nEstado,
                dtFechaRegistro = entity.dtFechaRegistro,
                nUsuarioRegistro = entity.nUsuarioRegistro,
                bCambiarClave = true
            };

            await _dbContext.Usuarios.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.nIdUsuario;
        }

        public async Task<bool> UpdateAsync(UsuarioEntity entity)
        {
            var model = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.nIdUsuario == entity.nIdUsuario);
            if (model == null) return false;

            model.nIdCargo = entity.nIdCargo;
            model.nIdRol = entity.nIdRol;
            model.nEstado = entity.nEstado;
            model.dtFechaModificacion = entity.dtFechaModificacion;
            model.nUsuarioModificacion = entity.nUsuarioModificacion;

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
                query = query.Where(u => (u.sApellidoPaterno.ToLower() + " " + u.sApellidoMaterno.ToLower() + " " + u.sNombres.ToLower()).Contains(nombreUsuario.ToLower()));
            }

            return await query.OrderBy(u => u.nIdUsuario).ToListAsync();
        }

        public async Task<UsuarioModel?> GetByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.nIdUsuario == id);
        }

        public async Task<bool> ExistsAsync(string nombreUsuario)
        {
            return await _dbContext.Usuarios.AnyAsync(u => u.sCorreoElectronico == nombreUsuario);
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
            //var query = _dbContext.Usuarios.AsQueryable();

            var query = from usuario in _dbContext.Usuarios

                        join consCargo in _dbContext.Constante
                            on new { Codigo = 11, Valor = usuario.nIdCargo }
                            equals new { Codigo = consCargo.nConCodigo, Valor = consCargo.nConValor }

                        join consPerfil in _dbContext.Roles
                            on new { Codigo = usuario.nIdRol }
                            equals new { Codigo = consPerfil.nIdRol }

                        join consUser in _dbContext.Constante
                            on new { Codigo = 10, Valor = usuario.nEstado }
                            equals new { Codigo = consUser.nConCodigo, Valor = consUser.nConValor }



                        select new UsuarioModel
                        {
                            nIdUsuario = usuario.nIdUsuario,
                            sApellidoPaterno = usuario.sApellidoPaterno,
                            sApellidoMaterno = usuario.sApellidoMaterno,
                            sNombres = usuario.sNombres,
                            sContrasena = "******",
                            nIdCargo = usuario.nIdCargo,
                            nIdRol = usuario.nIdRol,
                            sCorreoElectronico = usuario.sCorreoElectronico,
                            nEstado = usuario.nEstado,
                            dtFechaRegistro = usuario.dtFechaRegistro,
                            nUsuarioRegistro = usuario.nUsuarioRegistro,
                            dtFechaModificacion = usuario.dtFechaModificacion,
                            nUsuarioModificacion = usuario.nUsuarioModificacion,
                            sCargoDescripcion = consCargo.sConDescripcion,
                            sPerfilDescripcion = consPerfil.sNombreRol,
                            sEstadoDescripcion = consUser.sConDescripcion
                        };

            if (!string.IsNullOrEmpty(paramSearch))
            {
                query = query.Where(u => EF.Functions.Like(u.sApellidoPaterno.ToLower() + " " + u.sApellidoMaterno.ToLower() + " " + u.sNombres.ToLower(), "%" + paramSearch.ToLower() + "%"));
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

        public async Task<UsuarioModel> GetByEmailAsync(UsuarioEntity entity)
        {
            var user = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.sCorreoElectronico == entity.sCorreoElectronico);

            return user;
        }

        public async Task<bool> ChangePasswordAdminAsync(UsuarioEntity entity)
        {
            var model = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.nIdUsuario == entity.nIdUsuario);
            if (model == null) return false;

            model.bCambiarClave = true;
            model.sContrasena = entity.sContrasena;
            model.nUsuarioModificacion = entity.nUsuarioModificacion;
            model.dtFechaModificacion = entity.dtFechaModificacion;
            model.nUsuarioModificacion = entity.nUsuarioModificacion;

            _dbContext.Usuarios.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangePassword(UsuarioEntity entity)
        {
            var model = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.nIdUsuario == entity.nIdUsuario);
            if (model == null) return false;

            model.sContrasena = entity.nuevaClave;
            model.dtFechaModificacion = entity.dtFechaModificacion;
            model.nUsuarioModificacion = entity.nUsuarioModificacion;
            model.bCambiarClave = false;

            _dbContext.Usuarios.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
