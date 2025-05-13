using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using CA_ApplicationLayer.Departamentos;

namespace CA_FrameworksDrivers_API.Services
{
    public static class DepartamentosServiceRegistration
    {
        public static void AddDepartamentosServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartamentosRepository<DepartmentosModel>, DepartamentosRepository>();
            services.AddScoped<GetAllDepartamentosUseCase<DepartmentosModel, LstItemResponse<DepartmentosModel>>>();
        }
    }
}
