using CA_ApplicationLayer.Usuarios;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using CA_InterfaceAdapters_Mappers.USUARIOS;
using CA_InterfaceAdapters_Models;
using CA_ApplicationLayer.ExceptionLog;
using CA_InterfaceAdapters_Mappers.Dtos.Exeptions;
using CA_InterfaceAdapters_Mappers.EXEPTIONS;

namespace CA_FrameworksDrivers_API.Services
{
    public static class ExeptionsLogServiceRegistration
    {
        public static void AddExeptionLogServices(this IServiceCollection services)
        {
            services.AddScoped<IExeptionLogRepository, ExeptionLogRepository>();
            services.AddScoped<IMapper<ExeptionLogCreateDTO, ExeptionLogEntity>, ExeptionCreateMapper>();
            services.AddScoped<AddExeptionLogUseCase<ExeptionLogCreateDTO, ItemResponse<int>>>();
        }
    }
}
