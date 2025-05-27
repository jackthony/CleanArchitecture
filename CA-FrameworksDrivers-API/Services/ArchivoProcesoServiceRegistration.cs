using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using CA_ApplicationLayer.ArchivoProceso;
using CA_InterfaceAdapters_Mappers.Dtos.ArchivoProceso;
using CA_InterfaceAdapters_Mappers;
using CA_InterfaceAdapters_Presenters;

namespace CA_FrameworksDrivers_API.Services
{
    public static class ArchivoProcesoServiceRegistration
    {
    
            public static void AddArchivoProcesoServices(this IServiceCollection services)
            {
                    services.AddScoped<IArchivoProcesoRepository, ArchivoProcesoRepository>();
                    services.AddScoped<IMapper<ArchivoProcesoCreateDTO, ArchivoProcesoEntity>, ArchivoProcesoCreateMapper>();
                //services.AddScoped<IMapper<ArchivoProcesoCreateDTO, ArchivoProcesoEntity>, ArchivoProcesoUpdateMapper>();
                //services.AddScoped<GetEmpEmpresaUseCase<LstItemResponse<EMP_EmpresaModel>>>();
                services.AddScoped<AddArchivoProcesoUseCase<ArchivoProcesoCreateDTO, ItemResponse<int>>>();


            services.AddScoped<IArchivoProcesoRepository, ArchivoProcesoRepository>();
            //services.AddScoped<IMapper<ArchivoProcesoCreateDTO, ArchivoProcesoEntity>, ArchivoProcesoCreateMapper>();
            services.AddScoped<IArchivoStorage, FileSystemArchivoStorage>();
            // services.AddScoped<ICurrentUserService, CurrentUserServiceImplementation>(); // No se usa porque nUserId viene desde front
            services.AddScoped<IClock, SystemClockImplementation>();                      // Puedes implementar o usar DateTime.UtcNow directo
            services.AddScoped<AddArchivoProcesoUseCase<ArchivoProcesoCreateDTO, ItemResponse<int>>>();
            services.AddScoped<IPresenterResponse<int, ItemResponse<int>>, ItemResponsePresenter<int, ItemResponse<int>>>();


        }

    }
}
