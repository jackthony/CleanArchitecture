using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer.Provincias;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class ProvinciasEndpoints
    {
        public static void MapProvinciasEndpoints(this WebApplication app)
        {
            app.MapGet("Provincias/GetByPagination/{departmentCode}", async (string departmentCode, GetAllProvinciasUseCase<ProvinciasModel, LstItemResponse<ProvinciasModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(departmentCode);
            })
            .WithTags("Provincias")
            .WithName("GetProvincias")
            .WithOpenApi();
        }
    }
}
