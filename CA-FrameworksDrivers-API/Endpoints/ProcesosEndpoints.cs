using CA_ApplicationLayer.Usuarios;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class ProcesosEndpoints
    {
        public static void MapProcesosEndpoints(this WebApplication app)
        {
            app.MapGet("Proceso/GetByPagination", async (int pageIndex, int pageSize, string? search, GetAllProcesosUseCase<LstItemResponse<ProcesoModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(pageIndex, pageSize, search);
            })
            .WithTags("Proceso")
            .WithName("GetAllProcesos")
            .WithOpenApi();
        }
    }
}
