using CA_ApplicationLayer.Dir_Director;
using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.Dir_Director;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{

    public static class Dir_DirectorioEndpoints
    {
        public static void MapDirDirectorioEndpoints(this WebApplication app)
        {
            app.MapGet("Dir_Directorio/GetByPagination", async (int pageIndex, int pageSize, int nIdEmpresa, GetAllDirDirectorUseCase<LstItemResponse<Dir_DirectorModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(pageIndex, pageSize, nIdEmpresa);
            })
            .WithTags("Dir_Directorio")
            .WithName("GetDirDirectorio")
            .WithOpenApi();

            app.MapPost("Dir_Directorio/Insert", async (Dir_DirectorCreateDTO request, AddDirDirectorUseCase<Dir_DirectorCreateDTO, ItemResponse<int>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("Dir_Directorio")
            .WithName("InsertDirDirectorio")
            .WithOpenApi();

            app.MapPost("Dir_Directorio/Update", async (Dir_DirectorUpdateDTO request, EditDirDirectorUseCase<Dir_DirectorUpdateDTO, ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("Dir_Directorio")
            .WithName("UpdateDirDirectorio")
            .WithOpenApi();

            app.MapPost("Dir_Directorio/Delete/{nIdRegistro}", async (int nIdRegistro, DeleteDirDirectorUseCase<ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(nIdRegistro);
            })
            .WithTags("Dir_Directorio")
            .WithName("DeleteDirDirectorio")
            .WithOpenApi();
        }
    }
}
