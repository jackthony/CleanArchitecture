using CA_ApplicationLayer.ArchivoProceso;
using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.ArchivoProceso;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Mvc;

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
        }
    }
}
