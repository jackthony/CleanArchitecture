using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer.Provincias;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class ConstanteEndpoints
    {
        public static void MapConstanteEndpoints(this WebApplication app)
        {
            app.MapGet("Constante/GetByPagination/{nConCodigo}", async (int nConCodigo, GetAllConstanteUseCase<ConstanteModel, LstItemResponse<ConstanteModel>> useCase) =>
            {
                return await useCase.ExecuteAsync(nConCodigo);
            })
            .WithTags("Constante")
            .WithName("GetConstante")
            .WithOpenApi();
        }
    }
}
