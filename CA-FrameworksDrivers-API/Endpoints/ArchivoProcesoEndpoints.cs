using CA_ApplicationLayer.ArchivoProceso;
using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.ArchivoProceso;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class ArchivoProcesoEndpoints
    {
        public static void MapArchivoProcesoEndpoints(this WebApplication app)
        {
            //app.MapGet("EMP_Empresa/GetByPagination", async (int pageIndex, int pageSize, string? nameEnterprise, GetEmpEmpresaUseCase<LstItemResponse<EMP_EmpresaModel>> useCase) =>
            //{
            //    return await useCase.ExecuteAsync(pageIndex, pageSize, nameEnterprise);
            //})
            //.WithTags("EMP_Empresa")
            //.WithName("GetEmpEmpresas")
            //.WithOpenApi();

            app.MapPost("ArchivoProceso/Insert", async ([FromForm] ArchivoProcesoCreateDTO request, AddArchivoProcesoUseCase<ArchivoProcesoCreateDTO, ItemResponse<int>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .DisableAntiforgery()
            .WithTags("ArchivoProceso")
            .WithName("InsertArchivo")
            .WithOpenApi();

            app.MapGet("ArchivoProceso/OpenFolder/{*param}", (string param) =>
            {
                // Ruta por defecto si no se recibe parámetro
                string folderPath = @"C:\FonafeStorage\"+param;
                folderPath = folderPath.Replace('/', '\\');

                try
                {
                    // Verificar y crear carpeta si no existe
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Abrir carpeta con explorer.exe
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = folderPath,
                        UseShellExecute = true
                    });

                    return Results.Ok(new { message = $"Carpeta '{folderPath}' creada (si no existía) y abierta correctamente." });
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error al crear o abrir la carpeta: {ex.Message}");
                }
            })
            .WithTags("ArchivoProceso")
            .WithName("openFolder")
            .WithOpenApi();
        }
    }
}
