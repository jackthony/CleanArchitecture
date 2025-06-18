using CA_ApplicationLayer.Dir_Director;
using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Exportaciones
{
    public class ExportarEmpresasDirectoresUseCase
    {
        private readonly IDir_DirectorRepository _dirDirectorRepository;
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        public ExportarEmpresasDirectoresUseCase(IDir_DirectorRepository dirDirectorRepository, IEmp_EmpresaRepository empEmpresaRepository)
        {
            _dirDirectorRepository = dirDirectorRepository;
            _empEmpresaRepository = empEmpresaRepository;
        }

        public async Task<IResult> GenerarExcelAsync()
        {
            try
            {
                var empresas = await _empEmpresaRepository.GetAllEmpresasExportarAsync();
                var listaEmpresas = empresas.ToList();
                var directores = await _dirDirectorRepository.GetAllDirectoresExportarAsync();
                var listaDirectores = directores.ToList();

                using var workbook = new XLWorkbook();

                var hojaEmpresas = workbook.Worksheets.Add("Empresas");
                hojaEmpresas.Cell(1, 1).Value = "RUC";
                hojaEmpresas.Cell(1, 2).Value = "Razón Social";
                hojaEmpresas.Cell(1, 3).Value = "Departamento";
                hojaEmpresas.Cell(1, 4).Value = "Provincia";
                hojaEmpresas.Cell(1, 5).Value = "Distrito";
                hojaEmpresas.Cell(1, 6).Value = "Dirección";
                hojaEmpresas.Cell(1, 7).Value = "Rubro";
                hojaEmpresas.Cell(1, 8).Value = "Proponente";
                hojaEmpresas.Cell(1, 9).Value = "Ingreso de la empresa en el último año";
                hojaEmpresas.Cell(1, 10).Value = "Utilidad de la empresa en el último año";
                hojaEmpresas.Cell(1, 11).Value = "Capital Social";
                hojaEmpresas.Cell(1, 12).Value = "Número de miembros";
                hojaEmpresas.Cell(1, 13).Value = "Registro en mercado de valores";
                hojaEmpresas.Cell(1, 14).Value = "Activo";
                hojaEmpresas.Cell(1, 15).Value = "Comentario";
                for (int i = 0; i < listaEmpresas.Count(); i++)
                {
                    hojaEmpresas.Cell(i + 2, 1).Value = listaEmpresas[i].Ruc;
                    hojaEmpresas.Cell(i + 2, 2).Value = listaEmpresas[i].RazonSocial;
                    hojaEmpresas.Cell(i + 2, 3).Value = listaEmpresas[i].Departamento;
                    hojaEmpresas.Cell(i + 2, 4).Value = listaEmpresas[i].Provincia;
                    hojaEmpresas.Cell(i + 2, 5).Value = listaEmpresas[i].Distrito;
                    hojaEmpresas.Cell(i + 2, 6).Value = listaEmpresas[i].Direccion;
                    hojaEmpresas.Cell(i + 2, 7).Value = listaEmpresas[i].Rubro;
                    hojaEmpresas.Cell(i + 2, 8).Value = listaEmpresas[i].Ministerio;
                    hojaEmpresas.Cell(i + 2, 9).Value = listaEmpresas[i].Ingresos;
                    hojaEmpresas.Cell(i + 2, 10).Value = listaEmpresas[i].Utilidades;
                    hojaEmpresas.Cell(i + 2, 11).Value = listaEmpresas[i].CapitalSocial;
                    hojaEmpresas.Cell(i + 2, 12).Value = listaEmpresas[i].CantidadMiembros;
                    hojaEmpresas.Cell(i + 2, 13).Value = listaEmpresas[i].RegistroEnMercado;
                    hojaEmpresas.Cell(i + 2, 14).Value = listaEmpresas[i].Activo;
                    hojaEmpresas.Cell(i + 2, 15).Value = listaEmpresas[i].Comentario;
                }

                var hojaDirectores = workbook.Worksheets.Add("Directores");
                hojaDirectores.Cell(1, 1).Value = "RUC";
                hojaDirectores.Cell(1, 2).Value = "Empresa";
                hojaDirectores.Cell(1, 3).Value = "Tipo de documento";
                hojaDirectores.Cell(1, 4).Value = "Nro. Documento";
                hojaDirectores.Cell(1, 5).Value = "Departamento";
                hojaDirectores.Cell(1, 6).Value = "Provincia";
                hojaDirectores.Cell(1, 7).Value = "Distrito";
                hojaDirectores.Cell(1, 8).Value = "Dirección";
                hojaDirectores.Cell(1, 9).Value = "Nombres";    
                hojaDirectores.Cell(1, 10).Value = "Apellidos";
                hojaDirectores.Cell(1, 11).Value = "Fecha Nacimiento";
                hojaDirectores.Cell(1, 12).Value = "Género";
                hojaDirectores.Cell(1, 13).Value = "Telefono";
                hojaDirectores.Cell(1, 14).Value = "Correo";
                hojaDirectores.Cell(1, 15).Value = "Cargo";
                hojaDirectores.Cell(1, 16).Value = "Tipo de director";
                hojaDirectores.Cell(1, 17).Value = "Sector";
                hojaDirectores.Cell(1, 18).Value = "Profesión";
                hojaDirectores.Cell(1, 19).Value = "Dieta";
                hojaDirectores.Cell(1, 20).Value = "Especialidad";
                hojaDirectores.Cell(1, 21).Value = "Fec.Nombramiento";
                hojaDirectores.Cell(1, 22).Value = "Fec. de designación";
                hojaDirectores.Cell(1, 23).Value = "Fec. de renuncia";
                hojaDirectores.Cell(1, 24).Value = "Comentarios";

                int filaActual = 2;

                foreach (var empresa in listaEmpresas)
                {
                    var directoresDeEmpresa = listaDirectores
                        .Where(d => d.IdEmpresa == empresa.Id)
                        .ToList();

                    int inicio = filaActual;

                    foreach (var director in directoresDeEmpresa)
                    {
                        hojaDirectores.Cell(filaActual, 3).Value = director.TipoDocumento;
                        hojaDirectores.Cell(filaActual, 4).Value = director.Documento;
                        hojaDirectores.Cell(filaActual, 5).Value = director.Departamento;
                        hojaDirectores.Cell(filaActual, 6).Value = director.Provincia;
                        hojaDirectores.Cell(filaActual, 7).Value = director.Distrito;
                        hojaDirectores.Cell(filaActual, 8).Value = director.Direccion;
                        hojaDirectores.Cell(filaActual, 9).Value = director.Nombres;
                        hojaDirectores.Cell(filaActual, 10).Value = director.Apellidos;
                        hojaDirectores.Cell(filaActual, 11).Value = director.FechaNacimiento;
                        hojaDirectores.Cell(filaActual, 12).Value = director.Genero;
                        hojaDirectores.Cell(filaActual, 13).Value = director.Telefono;
                        hojaDirectores.Cell(filaActual, 14).Value = director.Correo;
                        hojaDirectores.Cell(filaActual, 15).Value = director.Cargo;
                        hojaDirectores.Cell(filaActual, 16).Value = director.TipoDirector;
                        hojaDirectores.Cell(filaActual, 17).Value = director.Sector;
                        hojaDirectores.Cell(filaActual, 18).Value = director.Profesion;
                        hojaDirectores.Cell(filaActual, 19).Value = director.Dieta;
                        hojaDirectores.Cell(filaActual, 20).Value = director.Especialidad;
                        hojaDirectores.Cell(filaActual, 21).Value = director.FechaNombramiento;
                        hojaDirectores.Cell(filaActual, 22).Value = director.FechaDesignacion;
                        hojaDirectores.Cell(filaActual, 23).Value = director.FechaRenuncia;
                        hojaDirectores.Cell(filaActual, 24).Value = director.Comentarios;

                        filaActual++;
                    }

                    int fin = filaActual - 1;

                    if (directoresDeEmpresa.Count > 0)
                    {
                        hojaDirectores.Range(inicio, 1, fin, 1).Merge();
                        hojaDirectores.Cell(inicio, 1).Value = empresa.Ruc;
                        hojaDirectores.Range(inicio, 1, fin, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        hojaDirectores.Range(inicio, 1, fin, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        hojaDirectores.Range(inicio, 2, fin, 2).Merge();
                        hojaDirectores.Cell(inicio, 2).Value = empresa.RazonSocial;
                        hojaDirectores.Range(inicio, 2, fin, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        hojaDirectores.Range(inicio, 2, fin, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }
                    else
                    {
                        hojaDirectores.Cell(filaActual, 1).Value = empresa.Ruc;
                        hojaDirectores.Cell(filaActual, 2).Value = empresa.RazonSocial;
                        hojaDirectores.Cell(filaActual, 3).Value = "(sin directores)";
                        filaActual++;
                    }
                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return Results.File(
                    fileContents: stream.ToArray(),
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: "Empresas"
                );
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    IsSuccess = false,
                    Errors = new List<string> { ex.Message }
                };
                return Results.BadRequest(errorResponse);
            }
        }
    }
}
