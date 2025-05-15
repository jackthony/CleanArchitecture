using CA_ApplicationLayer.Usuarios;
using CA_ApplicationLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;

namespace CA_FrameworksDrivers_API.Services
{
    public static class RolesServiceRegistration
    {
        public static void AddRolesService(this IServiceCollection services)
        {
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<GetAllRolesUseCase<LstItemResponse<RolModel>>>();
            services.AddScoped<ILstPagPresenterResponse<RolModel, LstItemResponse<RolModel>>, LstItemPaginationResponsePresenter<RolModel, LstItemResponse<RolModel>>>();
        }
    }
}
