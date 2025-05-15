using CA_ApplicationLayer.Usuarios;
using CA_ApplicationLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;

namespace CA_FrameworksDrivers_API.Services
{
    public static class RolPermisosServiceRegistration
    {
        public static void AddRolPermisosService(this IServiceCollection services)
        {
            services.AddScoped<IRolPermisoRepository, RolPermisoRepository>();
        }
    }
}
