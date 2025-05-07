using CA_ApplicationLayer.EMP_Empresa;
using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using CA_ApplicationLayer.Dir_Director;
using CA_InterfaceAdapters_Mappers.Dir_Director;
using CA_InterfaceAdapters_Mappers.Dtos.Dir_Director;

namespace CA_FrameworksDrivers_API.Services
{
    public static class Dir_DirectorServiceRegistration
    {
        public static void AddDirDirectorServices(this IServiceCollection services)
        {
            services.AddScoped<IDir_DirectorRepository, Dir_DirectorRepository>();
            services.AddScoped<IMapper<Dir_DirectorCreateDTO, Dir_DirectorEntity>, Dir_DirectorCreateMapper>();
            services.AddScoped<IMapper<Dir_DirectorUpdateDTO, Dir_DirectorEntity>, Dir_DirectorUpdateMapper>();
            services.AddScoped<GetAllDirDirectorUseCase<LstItemResponse<Dir_DirectorModel>>>();
            services.AddScoped<AddDirDirectorUseCase<Dir_DirectorCreateDTO, ItemResponse<int>>>();
            services.AddScoped<EditDirDirectorUseCase<Dir_DirectorUpdateDTO, ItemResponse<bool>>>();
            services.AddScoped<DeleteDirDirectorUseCase<ItemResponse<bool>>>();
        }
    }
}
