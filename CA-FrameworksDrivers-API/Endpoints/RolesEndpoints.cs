using CA_ApplicationLayer.Usuarios;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class RolesEndpoints
    {
        public static void MapRolesEndpoints(this WebApplication app)
        {
            app.MapGet("Rol/GetByPagination", async (int pageIndex, int pageSize, string? search, GetAllRolesUseCase<LstItemResponse<RolModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(pageIndex, pageSize, search);
            })
            .WithTags("Rol")
            .WithName("GetAllRoles")
            .WithOpenApi();
        }
    }
}
