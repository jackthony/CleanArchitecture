using CA_ApplicationLayer.Dir_Director;
using CA_ApplicationLayer.EMP_Empresa;
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
    public class Dir_DirectorRepository : IDir_DirectorRepository
    {
        private readonly AppDbContext _dbContext;

        public Dir_DirectorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddAsync(Dir_DirectorEntity entity)
        {
            var directorModel = new Dir_DirectorModel()
            {
                nIdEmpresa = entity.nIdEmpresa,
                nTipoDocumento = entity.nTipoDocumento,
                sNumeroDocumento = entity.sNumeroDocumento,
                sNombres = entity.sNombres,
                sApellidos = entity.sApellidos,
                dFechaNacimiento = entity.dFechaNacimiento,
                nGenero = entity.nGenero,
                sDistrito = entity.sDistrito,
                sProvincia = entity.sProvincia,
                sDepartamento = entity.sDepartamento,
                sDireccion = entity.sDireccion,
                sTelefono = entity.sTelefono,
                sCorreo = entity.sCorreo,
                nCargo = entity.nCargo,
                nTipoDirector = entity.nTipoDirector,
                sProfesion = entity.sProfesion,
                mDieta = entity.mDieta,
                nEspecialidad = entity.nEspecialidad,
                dFechaNombramiento = entity.dFechaNombramiento,
                dFechaDesignacion = entity.dFechaDesignacion,
                dFechaRenuncia = entity.dFechaRenuncia,
                sComentario = entity.sComentario,
                dtFechaRegistro = entity.dtFechaRegistro,
                sUsuarioRegistro = entity.sUsuarioRegistro
            };

            await _dbContext.Director.AddAsync(directorModel);
            await _dbContext.SaveChangesAsync();
            return directorModel.nIdRegistro;
        }

        public async Task<bool> UpdateAsync(Dir_DirectorEntity entity)
        {
            var directorModel = await _dbContext.Director.FirstOrDefaultAsync(e => e.nIdRegistro == entity.nIdRegistro);


            if (directorModel == null)
                return false;

            directorModel.sDistrito = entity.sDistrito;
            directorModel.sProvincia = entity.sProvincia;
            directorModel.sDepartamento = entity.sDepartamento;
            directorModel.sDireccion = entity.sDireccion;
            directorModel.sTelefono = entity.sTelefono;
            directorModel.sCorreo = entity.sCorreo;
            directorModel.nCargo = entity.nCargo;
            directorModel.nTipoDirector = entity.nTipoDirector;
            directorModel.sProfesion = entity.sProfesion;
            directorModel.mDieta = entity.mDieta;
            directorModel.nEspecialidad = entity.nEspecialidad;
            directorModel.dFechaNombramiento = entity.dFechaNombramiento;
            directorModel.dFechaDesignacion = entity.dFechaDesignacion;
            directorModel.dFechaRenuncia = entity.dFechaRenuncia;
            directorModel.sComentario = entity.sComentario;
            directorModel.dtFechaModificacion = entity.dtFechaModificacion;
            directorModel.sUsuarioModificacion = entity.sUsuarioModificacion;

            _dbContext.Director.Update(directorModel);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var directorModel = await _dbContext.Director.FirstOrDefaultAsync(e => e.nIdRegistro == id);

            if (directorModel == null)
            {
                return false;
            }

            _dbContext.Director.Remove(directorModel);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<ItemsPaginatorEntity<Dir_DirectorModel>> GetAllAsyncPaginationByEmpresa(int pageIndex, int pageSize, int nIdEmpresa)
        {
            //var query = _dbContext.Director.AsQueryable().Where(e => e.nIdEmpresa == nIdEmpresa);

            var query = from director in _dbContext.Director

                        join constante in _dbContext.Constante
                            on new { Codigo = 1, Valor = director.nTipoDocumento }
                            equals new { Codigo = constante.nConCodigo, Valor = constante.nConValor }

                        join cargo in _dbContext.Constante
                            on new { Codigo = 2, Valor = director.nCargo }
                            equals new { Codigo = cargo.nConCodigo, Valor = cargo.nConValor }

                        join tipoDirector in _dbContext.Constante
                            on new { Codigo = 3, Valor = director.nTipoDirector }
                            equals new { Codigo = tipoDirector.nConCodigo, Valor = tipoDirector.nConValor }

                        where director.nIdEmpresa == nIdEmpresa
                        orderby director.nIdRegistro
                        select new Dir_DirectorModel
                        {
                            nIdRegistro = director.nIdRegistro,
                            nIdEmpresa = director.nIdEmpresa,
                            nTipoDocumento = director.nTipoDocumento,
                            sNumeroDocumento = director.sNumeroDocumento,
                            sNombres = director.sNombres,
                            sApellidos = director.sApellidos,
                            dFechaNacimiento = director.dFechaNacimiento,
                            nGenero = director.nGenero,
                            sDistrito = director.sDistrito,
                            sProvincia = director.sProvincia,
                            sDepartamento = director.sDepartamento,
                            sDireccion = director.sDireccion,
                            sTelefono = director.sTelefono,
                            sCorreo = director.sCorreo,
                            nCargo = director.nCargo,
                            nTipoDirector = director.nTipoDirector,
                            sProfesion = director.sProfesion,
                            mDieta = director.mDieta,
                            nEspecialidad = director.nEspecialidad,
                            dFechaNombramiento = director.dFechaNombramiento,
                            dFechaDesignacion = director.dFechaDesignacion,
                            dFechaRenuncia = director.dFechaRenuncia,
                            sComentario = director.sComentario,
                            dtFechaRegistro = director.dtFechaRegistro,
                            sUsuarioRegistro = director.sUsuarioRegistro,
                            dtFechaModificacion = director.dtFechaModificacion,
                            sUsuarioModificacion = director.sUsuarioModificacion,
                            sTipoDocumentoDescripcion = constante.sConDescripcion,
                            sCargoDescripcion = cargo.sConDescripcion,
                            sTipoDirectorDescripcion = tipoDirector.sConDescripcion
                        };


            var totalRows = await query.CountAsync();
            var lstItem = await query
                .OrderBy(e => e.nIdRegistro)
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ItemsPaginatorEntity<Dir_DirectorModel>()
            {
                lstItem = lstItem,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows
            };
        }
    }
}
