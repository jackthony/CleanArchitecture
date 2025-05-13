using CA_ApplicationLayer;
using CA_ApplicationLayer.Usuarios;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using CA_InterfaceAdapters_Mappers.USUARIOS;

namespace CA_FrameworksDrivers_API.Services
{
    public static class UsuariosServiceRegistration
    {
        public static void AddUsuariosServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMapper<UsuarioCreateDTO, UsuarioEntity>, UsuarioCreateMapper>();
            services.AddScoped<IMapper<UsuarioUpdateDTO, UsuarioEntity>, UsuarioUpdateMapper>();
            services.AddScoped<GetAllUsuariosUseCase<LstItemResponse<UsuarioModel>>>();
            services.AddScoped<AddUsuarioUseCase<UsuarioCreateDTO, ItemResponse<int>>>();
            services.AddScoped<EditUsuarioUseCase<UsuarioUpdateDTO, ItemResponse<bool>>>();
            services.AddScoped<DeleteUsuarioUseCase<ItemResponse<bool>>>();
            services.AddScoped<LoginUsuarioUseCase<LoginRequestDTO, ItemResponse<LoginResponseDTO>, LoginResponseDTO>>();
            services.AddScoped<IMapper<LoginRequestDTO, UsuarioEntity>, LoginRequestMapper>();
            services.AddScoped<IMapper<UsuarioModel, LoginResponseDTO>, LoginResponseMapper>();
            services.AddScoped<ChangePasswordByAdminUseCase<ChangePasswordRequestDTO, ItemResponse<bool>>>();
            services.AddScoped<IMapper<ChangePasswordRequestDTO, UsuarioEntity>, ChangePasswordMapper>();
        }
    }
}
