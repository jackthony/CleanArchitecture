using CA_ApplicationLayer.Dir_Director;
using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Http;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Exportaciones
{
    public class ExportarEmpresasDirectoresPdfUseCase
    {
        private readonly IDir_DirectorRepository _dirDirectorRepository;
        private readonly IEmp_EmpresaRepository _empEmpresaRepository;
        public ExportarEmpresasDirectoresPdfUseCase(IDir_DirectorRepository dirDirectorRepository, IEmp_EmpresaRepository empEmpresaRepository)
        {
            _dirDirectorRepository = dirDirectorRepository;
            _empEmpresaRepository = empEmpresaRepository;
        }

        public async Task<IResult> GenerarPdfAsync()
        {
            try
            {
                var empresas = await _empEmpresaRepository.GetAllEmpresasExportarAsync();
                var listaEmpresas = empresas.ToList();
                var directores = await _dirDirectorRepository.GetAllDirectoresExportarAsync();
                var listaDirectores = directores.ToList();

                QuestPDF.Settings.License = LicenseType.Community;

                var documento = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(20);
                        page.DefaultTextStyle(x => x.FontSize(7));
                        page.Header().Text("Lista de Empresas").SemiBold().FontSize(14).AlignCenter();

                        page.Content().Table(table =>
                        {
                            // Definir columnas
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);  // RUC
                                columns.RelativeColumn(4);  // Razón Social
                                columns.RelativeColumn(1);  // Departamento
                                columns.RelativeColumn(1);  // Provincia
                                columns.RelativeColumn(1);  // Distrito
                                columns.RelativeColumn(3);  // Dirección
                                columns.RelativeColumn(2);  // Rubro
                                columns.RelativeColumn(2);  // Proponente
                                columns.RelativeColumn(2);  // Ingreso
                                columns.RelativeColumn(2);  // Utilidad
                                columns.RelativeColumn(1);  // Capital Social
                                columns.RelativeColumn(1);  // Número de miembros
                                columns.RelativeColumn(1);  // Registro mercado
                                columns.RelativeColumn(1);  // Activo
                                columns.RelativeColumn(2);  // Comentario
                            });

                            // Cabecera
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("RUC");
                                header.Cell().Element(CellStyle).Text("Razón Social");
                                header.Cell().Element(CellStyle).Text("Departamento");
                                header.Cell().Element(CellStyle).Text("Provincia");
                                header.Cell().Element(CellStyle).Text("Distrito");
                                header.Cell().Element(CellStyle).Text("Dirección");
                                header.Cell().Element(CellStyle).Text("Rubro");
                                header.Cell().Element(CellStyle).Text("Proponente");
                                //header.Cell().Element(CellStyle).Text(text => text.Span("Ingreso").LineBreak().Span("último año"));
                                header.Cell().Element(CellStyle).Text(text => text.Span("Ingreso"));
                                //header.Cell().Element(CellStyle).Text(text => text.Span("Utilidad").LineBreak().Span("último año"));
                                header.Cell().Element(CellStyle).Text(text => text.Span("Utilidad"));
                                header.Cell().Element(CellStyle).Text("Capital Social");
                                header.Cell().Element(CellStyle).Text("Miembros");
                                //header.Cell().Element(CellStyle).Text(text => text.Span("Registro en").LineBreak().Span("mercado"));
                                header.Cell().Element(CellStyle).Text(text => text.Span("En mercado"));
                                header.Cell().Element(CellStyle).Text("Activo");
                                header.Cell().Element(CellStyle).Text("Comentario");
                            });

                            // Cuerpo
                            foreach (var empresa in listaEmpresas)
                            {
                                table.Cell().Element(CellStyle).Text(empresa.Ruc);
                                table.Cell().Element(CellStyle).Text(empresa.RazonSocial);
                                table.Cell().Element(CellStyle).Text(empresa.Departamento);
                                table.Cell().Element(CellStyle).Text(empresa.Provincia);
                                table.Cell().Element(CellStyle).Text(empresa.Distrito);
                                table.Cell().Element(CellStyle).Text(empresa.Direccion);
                                table.Cell().Element(CellStyle).Text(empresa.Rubro);
                                table.Cell().Element(CellStyle).Text(empresa.Ministerio);
                                table.Cell().Element(CellStyle).Text(empresa.Ingresos.ToString("N2"));
                                table.Cell().Element(CellStyle).Text(empresa.Utilidades.ToString("N2"));
                                table.Cell().Element(CellStyle).Text(empresa.CapitalSocial.ToString("N2"));
                                table.Cell().Element(CellStyle).Text(empresa.CantidadMiembros.ToString());
                                table.Cell().Element(CellStyle).Text(empresa.RegistroEnMercado);
                                table.Cell().Element(CellStyle).Text(empresa.Activo);
                                table.Cell().Element(CellStyle).Text(empresa.Comentario);
                            }
                        });
                    });

                    // Salto de página para la tabla de directores
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(20);
                        page.DefaultTextStyle(x => x.FontSize(6));

                        page.Header().Text("Lista de Directores").SemiBold().FontSize(14).AlignCenter();

                        page.Content().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                for (int i = 0; i < 24; i++)
                                    columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                void H(string text) => header.Cell().Element(CellStyle).Text(text);

                                H("RUC");
                                H("Empresa");
                                H("Tipo doc");
                                H("Nro doc");
                                H("Departamento");
                                H("Provincia");
                                H("Distrito");
                                H("Dirección");
                                H("Nombres");
                                H("Apellidos");
                                H("F. Nacimiento");
                                H("Género");
                                H("Teléfono");
                                H("Correo");
                                H("Cargo");
                                H("Tipo director");
                                H("Sector");
                                H("Profesión");
                                H("Dieta");
                                H("Especialidad");
                                H("Fec. Nombramiento");
                                H("Fec. Designación");
                                H("Fec. Renuncia");
                                H("Comentarios");
                            });

                            foreach (var dir in listaDirectores)
                            {
                                void C(string text) => table.Cell().Element(CellStyle).Text(text ?? "");

                                C(ObtenerRucPorIdEmpresa(dir.IdEmpresa, listaEmpresas));
                                C(ObtenerRSPorIdEmpresa(dir.IdEmpresa, listaEmpresas));
                                C(dir.TipoDocumento);
                                C(dir.Documento);
                                C(dir.Departamento);
                                C(dir.Provincia);
                                C(dir.Distrito);
                                C(dir.Direccion);
                                C(dir.Nombres);
                                C(dir.Apellidos);
                                C(dir.FechaNacimiento.ToString() ?? "");
                                C(dir.Genero);
                                C(dir.Telefono);
                                C(dir.Correo);
                                C(dir.Cargo);
                                C(dir.TipoDirector);
                                C(dir.Sector);
                                C(dir.Profesion);
                                C(dir.Dieta.ToString() ?? "");
                                C(dir.Especialidad);
                                C(dir.FechaNombramiento.ToString() ?? "");
                                C(dir.FechaDesignacion.ToString() ?? "");
                                C(dir.FechaRenuncia);
                                C(dir.Comentarios);
                            }
                        });
                    });
                });

                using var stream = new MemoryStream();
                documento.GeneratePdf(stream);
                stream.Position = 0;

                var nombreArchivo = $"reporte_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf";

                return Results.File(
                    stream.ToArray(),
                    "application/pdf",
                    nombreArchivo
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

        static string ObtenerRucPorIdEmpresa(int idEmpresa, List<EmpresaExportarModel> empresas)
        {
            return empresas.FirstOrDefault(e => e.Id == idEmpresa)?.Ruc ?? "";
        }

        static string ObtenerRSPorIdEmpresa(int idEmpresa, List<EmpresaExportarModel> empresas)
        {
            return empresas.FirstOrDefault(e => e.Id == idEmpresa)?.RazonSocial ?? "";
        }


        static IContainer CellStyle(IContainer container)
        {
            return container
                .Padding(0)
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .AlignMiddle();
        }

    }
}
