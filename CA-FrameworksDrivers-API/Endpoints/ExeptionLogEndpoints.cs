using CA_ApplicationLayer.ExceptionLog;
using CA_ApplicationLayer.Usuarios;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.Exeptions;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class ExeptionLogEndpoints
    {
        public static void MapExeptionsLogEndpoints(this WebApplication app)
        {
            app.MapPost("ExeptionLog/Insert", async (ExeptionLogCreateDTO request, AddExeptionLogUseCase<ExeptionLogCreateDTO, ItemResponse<int>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("ExeptionLog")
            .WithName("InsertExeptionLog")
            .WithOpenApi();
        }
    }
}
