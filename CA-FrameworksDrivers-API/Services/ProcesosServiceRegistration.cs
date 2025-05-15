using CA_ApplicationLayer;
using CA_ApplicationLayer.Usuarios;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;

namespace CA_FrameworksDrivers_API.Services
{
    public static class ProcesosServiceRegistration
    {
        public static void AddProcesosService(this IServiceCollection services)
        {
            services.AddScoped<IProcesoRepository, ProcesoRepository>();
            services.AddScoped<GetAllProcesosUseCase<LstItemResponse<ProcesoModel>>>();
            services.AddScoped<ILstPagPresenterResponse<ProcesoModel, LstItemResponse<ProcesoModel>>, LstItemPaginationResponsePresenter<ProcesoModel, LstItemResponse<ProcesoModel>>>();
        }
    }
}
