using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.Distritos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer.Provincias;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class DistritosEndpoints
    {
        public static void MapDistritosEndpoints(this WebApplication app)
        {
            app.MapGet("Distritos/GetByPagination/{departmentCode}", async (string departmentCode, GetAllDistritosUseCase<DistritosModel, LstItemResponse<DistritosModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(departmentCode);
            })
            .WithTags("Distritos")
            .WithName("GetDistritos")
            .WithOpenApi();
        }
    }
}
