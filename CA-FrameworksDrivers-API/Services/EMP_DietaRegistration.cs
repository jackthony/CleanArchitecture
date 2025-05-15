using CA_ApplicationLayer.CAT_Ministerio;
using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Services
{
    public static class EMP_DietaRegistration
    {
        public static void AddEmpDietaServices(this IServiceCollection services)
        {
            services.AddScoped<IEmp_DietaRepository, EMP_DietaRepository>();
            services.AddScoped<GetEmpDirDietaUseCase<ItemResponse<EMP_DietaModel>>>();
        }
    }
}
