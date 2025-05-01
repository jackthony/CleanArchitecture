using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class DepartamentosEndpoints
    {
        public static void MapDepartamentosEndpoints(this WebApplication app)
        {
            app.MapGet("Departamentos/GetByPagination", async (GetAllDepartamentosUseCase<DepartmentosModel, LstItemResponse<DepartmentosModel>> useCase) =>
            {
                return await useCase.ExecuteAsync();
            })
            .WithTags("Departamentos")
            .WithName("GetDepartamentos")
            .WithOpenApi();
        }
    }
}
