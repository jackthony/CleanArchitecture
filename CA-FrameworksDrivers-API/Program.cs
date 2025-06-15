using CA_FrameworksDrivers_API.Middlewares;
using CA_InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using CA_EntrerpriseLayer.venta;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;
using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_ApplicationLayer.Common.IMappersFactory;
using CA_ApplicationLayer.Common.IExternalServices;
using CA_ApplicationLayer.Common.IPresenterFactory;
using CA_ApplicationLayer.UseCases.BeersUseCases;
using CA_ApplicationLayer.UseCases.SalesUseCases;
using CA_ApplicationLayer.UseCases.PostsUseCases;
using CA_EntrerpriseLayer.BeerModule;
using CA_EntrerpriseLayer.PostModule;
using CA_InterfaceAdapters_Mappers.Mappers.BeerModule;
using CA_InterfaceAdapters_Mappers.Mappers.SaleModule;
using CA_InterfaceAdapters_Mappers.Dtos.Requests.BeerModule;
using CA_InterfaceAdapters_Mappers.Dtos.Requests.SaleModule;
using CA_InterfaceAdapters_Models.SaleModule;
using CA_InterfaceAdapters_Presenters.BeerModule.BeerDetail;
using CA_InterfaceAdapters_Presenters.BeerModule.Presenters;
using CA_InterfaceAdapters_Presenters.BeerModule.ViewModels;
using CA_InterfaceAdapters_Adapters.PostModule;
using CA_FrameworkDrivers_ExternalService.PostModule;
using CA_InterfaceAdapters_Adapters.PostModule.Dtos;
using CA_InterfaceAdapters_Data.Contexts.EfCore;
using CA_InterfaceAdapter_Repository.BeerModule;
using CA_InterfaceAdapter_Repository.SaleModule;
using CA_InterfaceAdapters_Models.BeerModule;
using CA_FrameworksDrivers_API.Validators.BeerModule.BeerValidators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


//validadores
builder.Services.AddValidatorsFromAssemblyContaining<BeerValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();



//dependencias
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);
//builder.Services.AddScoped<IRepositoryAddAsync<Beer>, BeerRepository>();
//builder.Services.AddScoped<IRepositoryDeleteAsync<Beer>, BeerRepository>();
//builder.Services.AddScoped<IRepositoryGetAllAsync<Beer>, BeerRepository>();
//builder.Services.AddScoped<IRepositoryGetByIdAsync<Beer>, BeerRepository>();
//builder.Services.AddScoped<IRepositoryGetByPagination<Beer>, BeerRepository>();
//builder.Services.AddScoped<IRepositoryUpdateAsync<Beer>, BeerRepository>();
//builder.Services.AddScoped<IRepositorySearch<BeerModel, Beer>, BeerRepository>();





builder.Services.AddScoped<IEServiceGetContentAsync<PostServiceDTO>, PostService>();
builder.Services.AddScoped<IESGetAllDataAsync<Post>, PostExternalServiceAdapter>();

builder.Services.AddScoped<GetPostUseCase>();
//Generate Sale Use Case
builder.Services.AddScoped<GenerateSaleUseCase<SaleRequestDTO>>();
builder.Services.AddScoped<IMapperDtoToOutput<SaleRequestDTO, Sale>, SaleMapper>();
builder.Services.AddScoped<IRepository<Sale>, SaleRepository>();

//Get Sale Use Case
builder.Services.AddScoped<GetSaleUseCase>();

//Get Sale Search Use Case
builder.Services.AddScoped<IRepositorySearch<SaleModel, Sale>, SaleRepository>();
builder.Services.AddScoped<GetSaleSearchUseCase<SaleModel>>();



builder.Services.AddHttpClient<IEServiceGetContentAsync<PostServiceDTO>,PostService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/beer", async (GetBeerUseCase<BeerEntity, BeerViewModel> beerUseCase) => 
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("beers")
.WithOpenApi();

app.MapPost("/beer", async (AddBeerRequestDTO beerRequest, AddBeerUseCase<AddBeerRequestDTO> beerUseCase,
    IValidator<AddBeerRequestDTO> validator) =>
{
    var result = await validator.ValidateAsync(beerRequest);
    
    //await beerUseCase.ExecuteAsync(beerRequest);

    if (!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }
    await beerUseCase.ExecuteAsync(beerRequest);
    return Results.Created();

})
.WithName("add-beer")
.WithOpenApi();

app.MapGet("/beerDetail", async (GetBeerUseCase<BeerEntity, BeerDetailViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
    .WithName("beerDetail")
    .WithOpenApi();



app.MapGet("/posts", async (GetPostUseCase getPostUseCase) =>
{
    return await getPostUseCase.ExecuteAsync();
})
    .WithName("posts")
    .WithOpenApi();

//Ednpoint venta de Generate Sale Use Case

app.MapPost("/sale", async (SaleRequestDTO saleRequest,
    GenerateSaleUseCase<SaleRequestDTO> saleUseCase) =>
{
    //var result = await validator.ValidateAsync(saleRequest);

    //if (!result.IsValid)
    //{
    //    return Results.ValidationProblem(result.ToDictionary());
    //}

    await saleUseCase.ExecuteAsync(saleRequest);
    return Results.Created();
})
    .WithName("generateSale")
    .WithOpenApi();


app.MapGet("/sale", async (GetSaleUseCase getSaleUseCase) =>
{
    return await getSaleUseCase.ExecuteAsync();
})
    .WithName("getSale")
    .WithOpenApi();


//Ednpoint venta de Get Sale Search Use Case
app.MapGet("/salesearch/{total}", async (GetSaleSearchUseCase<SaleModel> saleUseCase, decimal total) =>
{
    return await saleUseCase.ExecuteAsync(s => s.Total > total);
})
    .WithName("getSalesSearch")
    .WithOpenApi();



//app.MapGet("/salesearch/{total}",
//    async (
//        [FromServices] GetSaleSearchUseCase<SaleModel> saleUseCase,
//        [FromRoute] int total
//    ) =>
//    {
//        var resultado = await saleUseCase.ExecuteAsync(s => s.Total > total);
//        return Results.Ok(resultado);
//    }
//)
//.WithName("getSalesSearch")
//.WithOpenApi();

app.MapControllers();
app.Run();

