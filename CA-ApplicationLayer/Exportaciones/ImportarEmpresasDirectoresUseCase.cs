using CA_ApplicationLayer.Dir_Director;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.ExportarEmpresas;
using CA_InterfaceAdapters_Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Exportaciones
{
    public class ImportarEmpresasDirectoresUseCase<TRequest>
    {
        private readonly IDir_DirectorRepository _dirDirectorRepository;
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        private readonly IMapper<TRequest, ImportarEmpresasEntity> _mapper;

        public ImportarEmpresasDirectoresUseCase(IDir_DirectorRepository dirDirectorRepository, IEmp_EmpresaRepository empEmpresaRepository, IMapper<TRequest, ImportarEmpresasEntity> mapper)
        {
            _dirDirectorRepository = dirDirectorRepository;
            _empEmpresaRepository = empEmpresaRepository;
            _mapper = mapper;
        }

        public async Task<IResult> ImportarDesdeExcel(TRequest request)
        {
            try
            {
                var convert = _mapper.ToEntity(request);
                var archivo = convert.Archivo;
                if (archivo == null || archivo.Length == 0)
                {
                    var errorResponse = new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "No existe archivo" }
                    };
                    return Results.BadRequest(errorResponse);
                }

                using var stream = archivo.OpenReadStream();
                using var workbook = new XLWorkbook(stream);
                var worksheetEmpresas = workbook.Worksheet("Empresas");

                var empresas = new List<ImportarEmpresaCreate>();

                foreach (var row in worksheetEmpresas.RowsUsed().Skip(1))
                {
                    var empresa = new ImportarEmpresaCreate
                    {
                        Ruc = row.Cell(1).GetValue<string>().Trim(),
                        RazonSocial = row.Cell(2).GetValue<string>().Trim(),
                        Departamento = row.Cell(3).GetValue<string>().Trim(),
                        Provincia = row.Cell(4).GetValue<string>().Trim(),
                        Distrito = row.Cell(5).GetValue<string>().Trim(),
                        Direccion = row.Cell(6).GetValue<string>().Trim(),
                        Rubro = row.Cell(7).GetValue<string>().Trim(),
                        Proponente = row.Cell(8).GetValue<string>().Trim(),
                        IngresosUltimoAnio = row.Cell(9).Value.ToString().Trim(),
                        UtilidadUltimoAnio = row.Cell(10).Value.ToString().Trim(),
                        CapitalSocial = row.Cell(11).Value.ToString().Trim(),
                        NumeroMiembros = row.Cell(12).Value.ToString().Trim(),
                        RegistroEnMercadoValores = row.Cell(13).GetValue<string>().Trim(),
                        Activo = row.Cell(14).GetValue<string>().Trim(),
                        Comentario = row.Cell(15).GetValue<string>().Trim()
                    };

                    empresas.Add(empresa);
                }

                var worksheetDirectores = workbook.Worksheet("Directores");

                var directores = new List<ImportarDirectorCreate>();

                foreach (var row in worksheetDirectores.RowsUsed().Skip(1))
                {
                    var director = new ImportarDirectorCreate
                    {
                        Ruc = GetMergedValue(row.Cell(1)),
                        Empresa = GetMergedValue(row.Cell(2)),
                        TipoDocumento = row.Cell(3).GetValue<string>().Trim(),
                        NumeroDocumento = row.Cell(4).GetValue<string>().Trim(),
                        Departamento = row.Cell(5).GetValue<string>().Trim(),
                        Provincia = row.Cell(6).GetValue<string>().Trim(),
                        Distrito = row.Cell(7).GetValue<string>().Trim(),
                        Direccion = row.Cell(8).GetValue<string>().Trim(),
                        Nombres = row.Cell(9).GetValue<string>().Trim(),
                        Apellidos = row.Cell(10).GetValue<string>().Trim(),
                        FechaNacimiento = row.Cell(11).Value.ToString().Trim(),
                        Genero = row.Cell(12).GetValue<string>().Trim(),
                        Telefono = row.Cell(13).GetValue<string>().Trim(),
                        Correo = row.Cell(14).GetValue<string>().Trim(),
                        Cargo = row.Cell(15).GetValue<string>().Trim(),
                        TipoDirector = row.Cell(16).GetValue<string>().Trim(),
                        Sector = row.Cell(17).GetValue<string>().Trim(),
                        Profesion = row.Cell(18).GetValue<string>().Trim(),
                        Dieta = row.Cell(19).Value.ToString().Trim(),
                        Especialidad = row.Cell(20).GetValue<string>().Trim(),
                        FechaNombramiento = row.Cell(21).Value.ToString().Trim(),
                        FechaDesignacion = row.Cell(22).Value.ToString().Trim(),
                        FechaRenuncia = row.Cell(23).Value.ToString().Trim(),
                        Comentarios = row.Cell(24).GetValue<string>().Trim()
                    };

                    directores.Add(director);
                }

                await _empEmpresaRepository.UploadEmpresasAsync(empresas, directores, convert.UsuarioId);

                return Results.Ok("Importación exitosa");
            }
            catch (Exception)
            {
                throw;
            }
        }

        string GetMergedValue(IXLCell cell)
        {
            return cell.Worksheet.MergedRanges
                .FirstOrDefault(r => r.Contains(cell))?
                .FirstCell()
                .GetValue<string>()
                .Trim() ?? cell.GetValue<string>().Trim();
        }
    }
}
