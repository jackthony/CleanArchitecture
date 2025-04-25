using CA_ApplicationLayer;
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
                sNombreEmpresa = entity.sNombreEmpresa,
                sRuc = entity.sRuc,
                sRazonSocial = entity.sRazonSocial,
                nIdProponente = entity.nIdProponente,
                nIdRubroNegocio = entity.nIdRubroNegocio,
                nIdDepartamento = entity.nIdDepartamento,
                nIdProvincia = entity.nIdProvincia,
                nIdDistrito = entity.nIdDistrito,
                sDireccion = entity.sDireccion,
                sComentario = entity.sComentario,
                mIngresosUltimoAnio = entity.mIngresosUltimoAnio,
                mUtilidadUltimoAnio = entity.mUtilidadUltimoAnio,
                mConformacionCapitalSocial = entity.mConformacionCapitalSocial,
                nNumeroMiembros = entity.nNumeroMiembros,
                bRegistradoMercadoValores = entity.bRegistradoMercadoValores,
                bActivo = entity.bActivo,
                dtFechaRegistro = entity.dtFechaRegistro,
                sUsuarioRegistro = entity.sUsuarioRegistro
            };

            await _dbContext.Empresas.AddAsync(empresaModel);
            await _dbContext.SaveChangesAsync();
            return empresaModel.nIdEmpresa;
        }
        public async Task<bool> UpdateAsync(EMP_EmpresaEntity entity)
        {
            TimeZoneInfo peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime peruDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);

            var empresaModel = await _dbContext.Empresas.FirstOrDefaultAsync(e => e.nIdEmpresa == entity.nIdEmpresa);

            if (empresaModel == null)
                return false;

            empresaModel.sRuc = entity.sRuc;
            empresaModel.sRazonSocial = entity.sRazonSocial;
            empresaModel.nIdProponente = entity.nIdProponente;
            empresaModel.nIdRubroNegocio = entity.nIdRubroNegocio;
            empresaModel.nIdDepartamento = entity.nIdDepartamento;
            empresaModel.nIdProvincia = entity.nIdProvincia;
            empresaModel.nIdDistrito = entity.nIdDistrito;
            empresaModel.sDireccion = entity.sDireccion;
            empresaModel.sComentario = entity.sComentario;
            empresaModel.mIngresosUltimoAnio = entity.mIngresosUltimoAnio;
            empresaModel.mUtilidadUltimoAnio = entity.mUtilidadUltimoAnio;
            empresaModel.mConformacionCapitalSocial = entity.mConformacionCapitalSocial;
            empresaModel.nNumeroMiembros = entity.nNumeroMiembros;
            empresaModel.bRegistradoMercadoValores = entity.bRegistradoMercadoValores;
            empresaModel.bActivo = entity.bActivo;
            empresaModel.dtFechaModificacion = entity.dtFechaModificacion;
            empresaModel.sUsuarioModificacion = entity.sUsuarioModificacion;

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

            _dbContext.Empresas.Remove(empresaModel);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemsPaginatorEntity<EMP_EmpresaModel>> GetAllAsyncPagination(int pageIndex, int pageSize, string? nameEnterprise)
        {
            var query = _dbContext.Empresas.AsQueryable();

            if (!string.IsNullOrEmpty(nameEnterprise))
            {
                
                query = query.Where(e => e.sRazonSocial.Contains(nameEnterprise));
            }

            var totalRows = await query.CountAsync();
            var lstItem = await query
                .OrderBy(e => e.nIdEmpresa)
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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

        public Task<EMP_EmpresaModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
