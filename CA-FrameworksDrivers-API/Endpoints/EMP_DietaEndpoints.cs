using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class EMP_DietaEndpoints
    {
        public static void EmpDietaEndpoints(this WebApplication app)
        {
            app.MapGet("EMP_Dieta/GetByRuc/{ruc}/{cargo}", async (string ruc, int cargo, GetEmpDirDietaUseCase<ItemResponse<EMP_DietaModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(ruc, cargo);
            })
            .WithTags("EMP_Dieta")
            .WithName("GetByRucDieta")
            .WithOpenApi();
        }
    }
}
