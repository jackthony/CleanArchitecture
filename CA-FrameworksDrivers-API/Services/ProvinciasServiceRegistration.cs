using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using CA_ApplicationLayer.Departamentos;
using CA_ApplicationLayer.Provincias;

namespace CA_FrameworksDrivers_API.Services
{
    public static class ProvinciasServiceRegistration
    {
        public static void AddProvinciasServices(this IServiceCollection services)
        {
            services.AddScoped<IProvinciasRepository<ProvinciasModel>, ProvinciasRepository>();
            services.AddScoped<GetAllProvinciasUseCase<ProvinciasModel, LstItemResponse<ProvinciasModel>>>();
        }
    }
}
