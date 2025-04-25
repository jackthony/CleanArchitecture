using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;

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

            app.MapPut("EMP_Empresa/Update", async (EMP_EmpresaUpdateDTO request, EditEmpEmpresaUseCase<EMP_EmpresaUpdateDTO, ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("EMP_Empresa")
            .WithName("UpdateEmpEmpresa")
            .WithOpenApi();

            app.MapDelete("EMP_Empresa/Delete/{idEmpresa}", async (int idEmpresa, DeleteEmpEmpresaUseCase<ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(idEmpresa);
            })
            .WithTags("EMP_Empresa")
            .WithName("DeleteEmpEmpresa")
            .WithOpenApi();
        }
    }
}
