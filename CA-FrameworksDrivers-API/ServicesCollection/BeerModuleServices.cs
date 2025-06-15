using CA_ApplicationLayer.Common.IMappersFactory;
using CA_ApplicationLayer.Common.IPresenterFactory;
using CA_ApplicationLayer.UseCases.BeersUseCases;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapters_Mappers.Dtos.Requests.BeerModule;
using CA_InterfaceAdapters_Mappers.Mappers.BeerModule;
using CA_InterfaceAdapters_Presenters.BeerModule.BeerDetail;
using CA_InterfaceAdapters_Presenters.BeerModule.Presenters;
using CA_InterfaceAdapters_Presenters.BeerModule.ViewModels;

namespace CA_FrameworksDrivers_API.ServicesCollection
{
    public static class BeerModuleServices
    {
        public static IServiceCollection ofBeerModule(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<CA_ApplicationLayer.Common.IRepositoriesFactory.IRepositoryAddAsync<CA_EntrerpriseLayer.BeerModule.BeerEntity>, CA_InterfaceAdapter_Repository.BeerModule.BeerRepository>();
            services.AddScoped<CA_ApplicationLayer.Common.IRepositoriesFactory.IRepositoryGetAllAsync<CA_EntrerpriseLayer.BeerModule.BeerEntity>, CA_InterfaceAdapter_Repository.BeerModule.BeerRepository>();
            services.AddScoped<CA_ApplicationLayer.Common.IRepositoriesFactory.IRepositoryGetByIdAsync<CA_EntrerpriseLayer.BeerModule.BeerEntity>, CA_InterfaceAdapter_Repository.BeerModule.BeerRepository>();
            services.AddScoped<CA_ApplicationLayer.Common.IRepositoriesFactory.IRepositoryGetByPagination<CA_EntrerpriseLayer.BeerModule.BeerEntity>, CA_InterfaceAdapter_Repository.BeerModule.BeerRepository>();
            services.AddScoped<CA_ApplicationLayer.Common.IRepositoriesFactory.IRepositoryUpdateAsync<CA_EntrerpriseLayer.BeerModule.BeerEntity>, CA_InterfaceAdapter_Repository.BeerModule.BeerRepository>();
            services.AddScoped<CA_ApplicationLayer.Common.IRepositoriesFactory.IRepositoryDeleteAsync<CA_EntrerpriseLayer.BeerModule.BeerEntity>, CA_InterfaceAdapter_Repository.BeerModule.BeerRepository>();
            services.AddScoped<CA_ApplicationLayer.Common.IRepositoriesFactory.IRepositorySearch<CA_InterfaceAdapters_Models.BeerModule.BeerModel, CA_EntrerpriseLayer.BeerModule.BeerEntity>, CA_InterfaceAdapter_Repository.BeerModule.BeerRepository>();
            // Mappers
            services.AddScoped<IMapperDtoToOutput<AddBeerRequestDTO, BeerEntity>, AddBeerMapper>();
            // Use Cases
            services.AddScoped<AddBeerUseCase<AddBeerRequestDTO>>();

            services.AddScoped<IPresenterGetAll<BeerEntity, BeerViewModel>, BeerPresenter>();
            services.AddScoped<IPresenterGetAll<BeerEntity, BeerDetailViewModel>, BeerDetailPresenter>();
            services.AddScoped<GetBeerUseCase<BeerEntity, BeerViewModel>>();
            services.AddScoped<GetBeerUseCase<BeerEntity, BeerDetailViewModel>>();

            services.AddScoped<IPresenterGetAll<BeerEntity, BeerViewModel>, BeerPresenter>();
            services.AddScoped<IPresenterGetAll<BeerEntity, BeerDetailViewModel>, BeerDetailPresenter>();
            services.AddScoped<IMapperDtoToOutput<AddBeerRequestDTO, BeerEntity>, AddBeerMapper>();

            services.AddScoped<GetBeerUseCase<BeerEntity, BeerViewModel>>();
            services.AddScoped<GetBeerUseCase<BeerEntity, BeerDetailViewModel>>();
            services.AddScoped<AddBeerUseCase<AddBeerRequestDTO>>();

            return services;
        }
    }
}
