using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class CatMinisterioEndpoints
    {
        public static void MapCatMinisterioEndpoints(this WebApplication app)
        {
            app.MapGet("Cat_Ministerio/GetByPagination", async (GetAllCat_MinisterioUseCase<CatMinisterioModel, LstItemResponse<CatMinisterioModel>> useCase) =>
            {
                return await useCase.ExecuteAsync();
            })
            .WithTags("Cat_Ministerio")
            .WithName("GetCat_Ministerio")
            .WithOpenApi();
        }
    }
}
