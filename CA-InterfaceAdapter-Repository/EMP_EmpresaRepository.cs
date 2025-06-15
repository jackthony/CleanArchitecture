using CA_ApplicationLayer;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapter_Repository
{
    public class EMP_EmpresaRepository: IEmp_EmpresaRepository
    {
        private readonly AppDbContext _dbContext;

        public EMP_EmpresaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(EMP_EmpresaEntity entity)
        {
            var empresaModel = new EMP_EmpresaModel()
            {
                nIdEmpresa = entity.nIdEmpresa,
                sRuc = entity.sRuc,
                sRazonSocial = entity.sRazonSocial,
                nIdProponente = entity.nIdProponente,
                nIdRubroNegocio = entity.nIdRubroNegocio,
                sIdDepartamento = entity.sIdDepartamento,
                sIdProvincia = entity.sIdProvincia,
                sIdDistrito = entity.sIdDistrito,
                sDireccion = entity.sDireccion,
                sComentario = entity.sComentario,
                nNumeroMiembros = entity.nNumeroMiembros,
                mIngresosUltimoAnio = entity.mIngresosUltimoAnio,
                mUtilidadUltimoAnio = entity.mUtilidadUltimoAnio,
                mConformacionCapitalSocial = entity.mConformacionCapitalSocial,
                bRegistradoMercadoValores = entity.bRegistradoMercadoValores,
                bActivo = entity.bActivo,
                dtFechaRegistro = entity.dtFechaRegistro,
                nUsuarioRegistro = entity.nUsuarioRegistro
            };

            await _dbContext.Empresas.AddAsync(empresaModel);
            await _dbContext.SaveChangesAsync();
            return empresaModel.nIdEmpresa;
        }
        public async Task<bool> UpdateAsync(EMP_EmpresaEntity entity)
        {
            var empresaModel = await _dbContext.Empresas.FirstOrDefaultAsync(e => e.nIdEmpresa == entity.nIdEmpresa);

            if (empresaModel == null)
                return false;

            empresaModel.nIdRubroNegocio = entity.nIdRubroNegocio;
            empresaModel.sIdDepartamento = entity.sIdDepartamento;
            empresaModel.sIdProvincia = entity.sIdProvincia;
            empresaModel.sIdDistrito = entity.sIdDistrito;
            empresaModel.sDireccion = entity.sDireccion;
            empresaModel.sComentario = entity.sComentario;
            empresaModel.nNumeroMiembros = entity.nNumeroMiembros;
            empresaModel.mIngresosUltimoAnio = entity.mIngresosUltimoAnio;
            empresaModel.mUtilidadUltimoAnio = entity.mUtilidadUltimoAnio;
            empresaModel.mConformacionCapitalSocial = entity.mConformacionCapitalSocial;
            empresaModel.bRegistradoMercadoValores = entity.bRegistradoMercadoValores;
            empresaModel.bActivo = entity.bActivo;
            empresaModel.dtFechaModificacion = entity.dtFechaModificacion;
            empresaModel.nUsuarioModificacion = entity.nUsuarioModificacion;

            _dbContext.Empresas.Update(empresaModel);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empresaModel = await _dbContext.Empresas.FirstOrDefaultAsync(e => e.nIdEmpresa == id);

            if (empresaModel == null)
            {
                return false;
            }
            empresaModel.bActivo = !empresaModel.bActivo;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<ItemsPaginatorEntity<EMP_EmpresaModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? nameEnterprise)
        {

            var query = from empresa in _dbContext.Empresas

                        join constante in _dbContext.Constante
                            on new { Codigo = 1024, Valor = empresa.nIdRubroNegocio }
                            equals new { Codigo = constante.nConCodigo, Valor = constante.nConValor }

                        join ministerio in _dbContext.Ministerio
                            on new { Codigo = empresa.nIdProponente }
                            equals new { Codigo = ministerio.nIdMinisterio }

                        join provincia in _dbContext.Provincias
                            on new { Codigo = empresa.sIdProvincia }
                            equals new { Codigo = provincia.sCode }

                        select new EMP_EmpresaModel
                        {
                            nIdEmpresa = empresa.nIdEmpresa,
                            sRuc = empresa.sRuc,
                            sRazonSocial = empresa.sRazonSocial,
                            nIdProponente = empresa.nIdProponente,
                            nIdRubroNegocio = empresa.nIdRubroNegocio,
                            sIdDepartamento = empresa.sIdDepartamento,
                            sIdProvincia = empresa.sIdProvincia,
                            sIdDistrito = empresa.sIdDistrito,
                            sDireccion = empresa.sDireccion,
                            sComentario = empresa.sComentario,
                            mIngresosUltimoAnio = empresa.mIngresosUltimoAnio,
                            mUtilidadUltimoAnio = empresa.mUtilidadUltimoAnio,
                            mConformacionCapitalSocial = empresa.mConformacionCapitalSocial,
                            nNumeroMiembros = empresa.nNumeroMiembros,
                            bRegistradoMercadoValores = empresa.bRegistradoMercadoValores,
                            bActivo = empresa.bActivo,
                            dtFechaRegistro = empresa.dtFechaRegistro,
                            nUsuarioRegistro = empresa.nUsuarioRegistro,
                            dtFechaModificacion = empresa.dtFechaModificacion,
                            nUsuarioModificacion = empresa.nUsuarioModificacion,
                            sDescripcionRubro = constante.sConDescripcion,
                            sNombreMinisterio = ministerio.sNombreMinisterio,
                            sProvinciaDescripcion = provincia.sName
                        };



            var totalRows = await query.CountAsync();
            var lstItem = await query
                .OrderBy(e => e.bActivo ? 0 : 1) // Primero ordena por bActivo, donde 'true' será 0 y 'false' será 1
                .ThenBy(e => e.nIdEmpresa) // Luego ordena por nIdEmpresa
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!string.IsNullOrEmpty(nameEnterprise))
            {

                var lowerCriterio = nameEnterprise.Trim().ToLowerInvariant();

                lstItem = lstItem.Where(e =>
                    (e.sRazonSocial ?? "").ToLower().Contains(lowerCriterio) ||
                    (e.sRuc ?? "").ToLower().Contains(lowerCriterio) ||
                    (e.sDireccion ?? "").ToLower().Contains(lowerCriterio) ||
                    (e.sComentario ?? "").ToLower().Contains(lowerCriterio) ||
                    (e.sNombreMinisterio ?? "").ToLower().Contains(lowerCriterio) || 
                    (e.sDescripcionRubro ?? "").ToLower().Contains(lowerCriterio) ||
                    (e.sProvinciaDescripcion ?? "").ToLower().Contains(lowerCriterio) ||
                    (e.dtFechaRegistro.ToString("dd/MM/yyyy") ?? "").ToLower().Contains(lowerCriterio) ||
                    (e.dtFechaModificacion?.ToString("dd/MM/yyyy") ?? "").ToLower().Contains(lowerCriterio)
                ).ToList();
                
            }

            int indiceInicio = ((pageIndex - 1) * pageSize) + 1;
            for (int i = 0; i < lstItem.Count; i++)
            {
                lstItem[i].indice = (indiceInicio + i).ToString();
            }

            return new ItemsPaginatorEntity<EMP_EmpresaModel>()
            {
                lstItem = lstItem,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows
            };
        }

        public async Task<IEnumerable<EMP_EmpresaModel>> GetAllAsync()
        {
            
            return await _dbContext.Empresas.ToListAsync();
        }

        public async Task<EMP_EmpresaModel> GetById(int id)
        {
            var empresa = await _dbContext.Empresas
            .Where(e => e.nIdEmpresa == id)
            .Select(e => new EMP_EmpresaModel
            {
            nIdEmpresa = e.nIdEmpresa,
            sRuc = e.sRuc,
            sRazonSocial = e.sRazonSocial,
            nIdProponente = e.nIdProponente,
            nIdRubroNegocio = e.nIdRubroNegocio,
            sIdDepartamento = e.sIdDepartamento,
            sIdProvincia = e.sIdProvincia,
            sIdDistrito = e.sIdDistrito,
            sDireccion = e.sDireccion,
            sComentario = e.sComentario,
            mIngresosUltimoAnio = e.mIngresosUltimoAnio,
            mUtilidadUltimoAnio = e.mUtilidadUltimoAnio,
            mConformacionCapitalSocial = e.mConformacionCapitalSocial,
            nNumeroMiembros = e.nNumeroMiembros,
            bRegistradoMercadoValores = e.bRegistradoMercadoValores,
            bActivo = e.bActivo,
            dtFechaRegistro = e.dtFechaRegistro,
            nUsuarioRegistro = e.nUsuarioRegistro,
            dtFechaModificacion = e.dtFechaModificacion,
            nUsuarioModificacion = e.nUsuarioModificacion,
            // Aquí puedes mapear también las propiedades [NotMapped] si tienes las tablas relacionadas
            // Ejemplo para sDescripcionRubro, sNombreMinisterio, sProvinciaDescripcion si deseas
        })
        .FirstOrDefaultAsync();

            return empresa;
        }
    }
}
