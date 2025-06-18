using CA_ApplicationLayer;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Mappers.Dtos.ExportarEmpresas;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<IEnumerable<EmpresaExportarModel>> GetAllEmpresasExportarAsync()
        {
            var resultado = await _dbContext.EmpresasExportar
                .FromSqlRaw("EXEC sp_ListarEmpresasExportar")
                .ToListAsync();
            return resultado;
        }

        public async Task<bool> UploadEmpresasAsync(List<ImportarEmpresaCreate> empresas, List<ImportarDirectorCreate> directores, int usuarioId)
        {
            var errores = new List<string> { };
            var departamentos = await _dbContext.Departmentos.ToListAsync();
            var provincias = await _dbContext.Provincias.ToListAsync();
            var distritos = await _dbContext.Distritos.ToListAsync();
            var rubros = await _dbContext.Constante
                .Where(c => c.nConCodigo == 1024 && c.nConValor != 0)
                .ToListAsync();
            var proponentes = await _dbContext.Ministerio.ToListAsync();
            var sectores = await _dbContext.Constante
                .Where(c => c.nConCodigo == 14 && c.nConValor != 0)
                .ToListAsync();
            var tiposDocumentos = await _dbContext.Constante
                .Where(c => c.nConCodigo == 14 && c.nConValor != 0)
                .ToListAsync();

            foreach (var empresa in empresas)
            {
                if (string.IsNullOrWhiteSpace(empresa.RazonSocial))
                {
                    errores.Add($"Razón social obligatoria para empresa con RUC: '{empresa.Ruc}'");
                    continue;
                }

                var existe = await _dbContext.Empresas.AnyAsync(e =>
                    e.sRuc == empresa.Ruc || e.sRazonSocial.ToLower() == empresa.RazonSocial.ToLower());

                if (existe)
                {
                    errores.Add($"Empresa ya registrada: '{empresa.RazonSocial}' (RUC: {empresa.Ruc})");
                    continue;
                }

                var duplicadosEnExcel = empresas
                    .Count(e => e.Ruc == empresa.Ruc || e.RazonSocial.Equals(empresa.RazonSocial, StringComparison.OrdinalIgnoreCase));

                if (duplicadosEnExcel > 1)
                {
                    errores.Add($"Empresa duplicada en archivo: '{empresa.RazonSocial}' (RUC: {empresa.Ruc})");
                    continue;
                }

                var nombreDepartamento = empresa.Departamento.Trim();
                var nombreProvincia = empresa.Provincia.Trim();
                var nombreDistrito = empresa.Distrito.Trim();

                var departamento = departamentos
                    .FirstOrDefault(d => d.sName.Equals(nombreDepartamento, StringComparison.OrdinalIgnoreCase));

                if (departamento == null)
                {
                    errores.Add($"Departamento no encontrado: '{nombreDepartamento}' para empresa '{empresa.RazonSocial}'");
                    continue;
                }

                var provincia = provincias
                    .FirstOrDefault(p =>
                        p.sName.Equals(nombreProvincia, StringComparison.OrdinalIgnoreCase) &&
                        p.sDepartmentCode == departamento.sCode);

                if (provincia == null)
                {
                    errores.Add($"Provincia no encontrada o no pertenece al departamento: '{nombreProvincia}' → '{nombreDepartamento}'");
                    continue;
                }

                var distrito = distritos
                    .FirstOrDefault(dist =>
                        dist.sName.Equals(nombreDistrito, StringComparison.OrdinalIgnoreCase) &&
                        dist.sProvinceCode == provincia.sCode);

                if (distrito == null)
                {
                    errores.Add($"Distrito no encontrado o no pertenece a la provincia: '{nombreDistrito}' → '{nombreProvincia}'");
                    continue;
                }

                var departamentoId = departamento.sCode;
                var provinciaId = provincia.sCode;
                var distritoId = distrito.sCode;

                var rubro = rubros.FirstOrDefault(r => r.sConDescripcion.Equals(empresa.Rubro, StringComparison.OrdinalIgnoreCase));
                if (rubro == null)
                {
                    errores.Add($"Rubro inválido: '{empresa.Rubro}' en empresa '{empresa.RazonSocial}'");
                    continue;
                }

                var proponente = proponentes.FirstOrDefault(p => p.sNombreMinisterio.Equals(empresa.Proponente, StringComparison.OrdinalIgnoreCase));
                if (proponente == null)
                {
                    errores.Add($"Proponente inválido: '{empresa.Proponente}' en empresa '{empresa.RazonSocial}'");
                    continue;
                }

                var registro = empresa.RegistroEnMercadoValores.Trim().ToLower();
                if (registro != "sí" && registro != "no" && registro != "si")
                {
                    errores.Add($"Valor inválido en 'Registro en mercado de valores': '{empresa.RegistroEnMercadoValores}'");
                    continue;
                }
                var registroBool = registro == "sí";

                var activo = empresa.Activo.Trim().ToLower();
                if (activo != "sí" && activo != "no" && activo != "si")
                {
                    errores.Add($"Valor inválido en 'Activo': '{empresa.Activo}'");
                    continue;
                }
                var activoBool = activo == "sí";
                decimal ingresos = string.IsNullOrEmpty(empresa.IngresosUltimoAnio)
                    ? 0
                    : decimal.TryParse(empresa.IngresosUltimoAnio, out var parsedValue1) ? parsedValue1 : 0;

                decimal utilidades = string.IsNullOrEmpty(empresa.IngresosUltimoAnio)
                    ? 0
                    : decimal.TryParse(empresa.UtilidadUltimoAnio, out var parsedValue2) ? parsedValue2 : 0;

                decimal capital = string.IsNullOrEmpty(empresa.IngresosUltimoAnio)
                    ? 0
                    : decimal.TryParse(empresa.UtilidadUltimoAnio, out var parsedValue3) ? parsedValue3 : 0;

                int miembros = string.IsNullOrEmpty(empresa.IngresosUltimoAnio)
                    ? 0
                    : int.TryParse(empresa.NumeroMiembros, out var parsedValue4) ? parsedValue4 : 0;

                var nuevaEmpresa = new EMP_EmpresaModel
                {
                    sRuc = empresa.Ruc,
                    sRazonSocial = empresa.RazonSocial,
                    nIdProponente = proponente.nIdMinisterio,
                    nIdRubroNegocio = rubro.nConValor,
                    sIdDepartamento = departamento.sCode,
                    sIdProvincia = provincia.sCode,
                    sIdDistrito = distrito.sCode,
                    sDireccion = empresa.Direccion,
                    sComentario = empresa.Comentario,
                    mIngresosUltimoAnio = ingresos,
                    mUtilidadUltimoAnio = utilidades,
                    mConformacionCapitalSocial = capital,
                    nNumeroMiembros = miembros,
                    bRegistradoMercadoValores = registroBool,
                    bActivo = activoBool,
                    dtFechaRegistro = DateTime.Now,
                    nUsuarioRegistro = usuarioId,
                };

                try
                {
                    _dbContext.Empresas.Add(nuevaEmpresa);
                    await _dbContext.SaveChangesAsync();

                    Console.WriteLine("Empresa guardada correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar la empresa: {ex.Message}");
                }
            }

            /*foreach (var director in directores)
            {
                var empresaExiste = await _dbContext.Empresas
                    .AnyAsync(e => e.sRuc == director.Ruc);

                if (!empresaExiste)
                {
                    errores.Add($"No se puede registrar al director '{director.Nombres} {director.Apellidos}' porque el RUC '{director.Ruc}' no está registrado.");
                    continue;
                }



            }*/
            return true;
        }
    }
}
