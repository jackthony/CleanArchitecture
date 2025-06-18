using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_ApplicationLayer.Nodos;
using CA_ApplicationLayer.Exportaciones;
using CA_InterfaceAdapters_Mappers.Dtos.EmpresaImportar;
using Microsoft.AspNetCore.Mvc;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class EMP_EmpresaEndpoints
    {
        public static void MapEmpEmpresasEndpoints(this WebApplication app)
        {
            app.MapGet("EMP_Empresa/GetByPagination", async (int pageIndex, int pageSize, string? nameEnterprise ,GetEmpEmpresaUseCase<LstItemResponse<EMP_EmpresaModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(pageIndex, pageSize, nameEnterprise);
            })
            .WithTags("EMP_Empresa")
            .WithName("GetEmpEmpresas")
            .WithOpenApi();

            app.MapPost("EMP_Empresa/Insert", async (EMP_EmpresaCreateDTO request ,AddEmpEmpresaUseCase<EMP_EmpresaCreateDTO, ItemResponse<int>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("EMP_Empresa")
            .WithName("InsertEmpEmpresa")
            .WithOpenApi();

            app.MapPost("EMP_Empresa/Update", async (EMP_EmpresaUpdateDTO request, EditEmpEmpresaUseCase<EMP_EmpresaUpdateDTO, ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("EMP_Empresa")
            .WithName("UpdateEmpEmpresa")
            .WithOpenApi();

            app.MapPost("EMP_Empresa/Delete/{idEmpresa}", async (int idEmpresa, DeleteEmpEmpresaUseCase<ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(idEmpresa);
            })
            .WithTags("EMP_Empresa")
            .WithName("DeleteEmpEmpresa")
            .WithOpenApi();

            app.MapGet("EMP_Empresa/GetById/{nIdEmpresa}", async (int nIdEmpresa, GetByIdEmpEmpresaUseCase<ItemResponse<EMP_EmpresaModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(nIdEmpresa);
            })
            .WithTags("EMP_Empresa")
            .WithName("GetByIdEmpEmpresas")
            .WithOpenApi();

            app.MapGet("EMP_Empresa/ExportarExcel", async (ExportarEmpresasDirectoresUseCase useCase) =>
            {
                return await useCase.GenerarExcelAsync();
            })
            .WithTags("EMP_Empresa")
            .WithName("ExportarExcel")
            .WithOpenApi();

            app.MapGet("EMP_Empresa/ExportarPdf", async (ExportarEmpresasDirectoresPdfUseCase useCase) =>
            {
                return await useCase.GenerarPdfAsync();
            })
            .WithTags("EMP_Empresa")
            .WithName("ExportarPdf")
            .WithOpenApi();

            app.MapPost("EMP_Empresa/ImportarExcel", async ([FromForm] ImportarEmpresaRequest request, ImportarEmpresasDirectoresUseCase<ImportarEmpresaRequest> useCase) =>
            {
                return await useCase.ImportarDesdeExcel(request);
            })
            .WithTags("EMP_Empresa")
            .WithName("ImportarExcel")
            .DisableAntiforgery()
            .WithOpenApi();
        }
    }
}
