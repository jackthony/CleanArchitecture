using CA_ApplicationLayer.Dir_Director;
using CA_ApplicationLayer.EMP_Empresa;
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
                        page.DefaultTextStyle(x => x.FontSize(9));
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

        static IContainer CellStyle(IContainer container)
        {
            return container
                .Padding(2)
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .AlignMiddle();
        }

    }
}
