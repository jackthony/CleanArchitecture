using CA_ApplicationLayer.Usuarios;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class PermisosEndpoints
    {
        public static void MapPermisosEndpoints(this WebApplication app)
        {
            app.MapGet("Permiso/GetByPagination", async (int pageIndex, int pageSize, string? search, GetAllPermisosUseCase<LstItemResponse<PermisoModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(pageIndex, pageSize, search);
            })
            .WithTags("Permiso")
            .WithName("GetAllPermisos")
            .WithOpenApi();
        }
    }
}
