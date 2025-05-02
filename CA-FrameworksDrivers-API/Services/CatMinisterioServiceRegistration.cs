using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.CAT_Ministerio;

namespace CA_FrameworksDrivers_API.Services
{
    public static class CatMinisterioServiceRegistration
    {
        public static void AddCatMinisterioServices(this IServiceCollection services)
        {
            services.AddScoped<ICat_MinisterioRepository<CatMinisterioModel>, Cat_MinisterioRepository>();
            services.AddScoped<GetAllCat_MinisterioUseCase<CatMinisterioModel, LstItemResponse<CatMinisterioModel>>>();
        }
    }
}
