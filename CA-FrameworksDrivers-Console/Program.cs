using CA_ApplicationLayer.Common.IPresenterFactory;
using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_ApplicationLayer.UseCases.BeersUseCases;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapter_Repository.BeerModule;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Data.Contexts.EfCore;
using CA_InterfaceAdapters_Presenters.BeerModule.Presenters;
using CA_InterfaceAdapters_Presenters.BeerModule.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = builder.Build();

var container = new ServiceCollection()
    .AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
    .AddScoped<IRepositoryGetAllAsync<BeerEntity>, BeerRepository>()
    .AddScoped<GetBeerUseCase<BeerEntity, BeerViewModel>>()
    .AddScoped<IPresenterGetAll<BeerEntity, BeerViewModel>, BeerPresenter>()
    .BuildServiceProvider();

var getBeerUseCase = container.GetService<GetBeerUseCase<BeerEntity, BeerDetailViewModel>>();

var beers = await getBeerUseCase.ExecuteAsync();

foreach (var beer in beers)
{
    Console.WriteLine($"Cerveza {beer.Name} con {beer.Alcohol} alcohol");
}

