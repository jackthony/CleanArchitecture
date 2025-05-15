using CA_ApplicationLayer;
using CA_ApplicationLayer.Usuarios;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;

namespace CA_FrameworksDrivers_API.Services
{
    public static class PermisosServiceRegistration
    {
        public static void AddPermisosService(this IServiceCollection services)
        {
            services.AddScoped<IPermisoRepository, PermisoRepository>();
            services.AddScoped<GetAllPermisosUseCase<LstItemResponse<PermisoModel>>>();
            services.AddScoped<ILstPagPresenterResponse<PermisoModel, LstItemResponse<PermisoModel>>, LstItemPaginationResponsePresenter<PermisoModel, LstItemResponse<PermisoModel>>>();
        }
    }
}
