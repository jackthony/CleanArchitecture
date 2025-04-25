using CA_ApplicationLayer;
using CA_ApplicationLayer.EMP_Empresa;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA;
using CA_InterfaceAdapters_Mappers.Dtos.Requests;
using CA_InterfaceAdapters_Mappers.EMP_EMPRESA;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;

namespace CA_FrameworksDrivers_API.Services
{
    public static class EMP_EmpresaServiceRegistration
    {
        public static void AddEMP_EmpresaServices(this IServiceCollection services)
        {
            services.AddScoped<IEmp_EmpresaRepository, EMP_EmpresaRepository>();
            services.AddScoped<IMapper<EMP_EmpresaCreateDTO, EMP_EmpresaEntity>, EMP_EmpresaCreateMapper>();
            services.AddScoped<IMapper<EMP_EmpresaUpdateDTO, EMP_EmpresaEntity>, EMP_EmpresaUpdateMapper>();
            services.AddScoped<GetEmpEmpresaUseCase<LstItemResponse<EMP_EmpresaModel>>>();
            services.AddScoped<AddEmpEmpresaUseCase<EMP_EmpresaCreateDTO,ItemResponse<int>>>();
            services.AddScoped<EditEmpEmpresaUseCase<EMP_EmpresaUpdateDTO, ItemResponse<bool>>>();
            services.AddScoped<DeleteEmpEmpresaUseCase<ItemResponse<bool>>>();
        }
    }
}
