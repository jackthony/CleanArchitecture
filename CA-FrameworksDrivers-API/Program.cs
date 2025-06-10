using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_FrameworksDrivers_API.Middlewares;
using CA_FrameworksDrivers_API.Validators;
using CA_InterfaceAdapter_Repository;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Mappers;
using CA_InterfaceAdapters_Mappers.Dtos.Requests;
using CA_InterfaceAdapters_Presenters;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using CA_FrameworkDrivers_ExternalService;
using CA_InterfaceAdapters_Adapters.Dtos;
using CA_InterfaceAdapters_Adapters;
using Microsoft.Extensions.DependencyInjection;
using CA_ApplicationLayer.venta;
using CA_EntrerpriseLayer.venta;
using CA_InterfaceAdapters_Models;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;

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
builder.Services.AddScoped<IRepository<Beer>, Repository>();
builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
builder.Services.AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>();
builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();
builder.Services.AddScoped<IExternalService<PostServiceDTO>, PostService>();
builder.Services.AddScoped<IExternalServiceAdapter<Post>, PostExternalServiceAdapter>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<GetPostUseCase>();
//Generate Sale Use Case
builder.Services.AddScoped<GenerateSaleUseCase<SaleRequestDTO>>();
builder.Services.AddScoped<IMapper<SaleRequestDTO, Sale>, SaleMapper>();
builder.Services.AddScoped<IRepository<Sale>, SaleRepository>();

//Get Sale Use Case
builder.Services.AddScoped<GetSaleUseCase>();

//Get Sale Search Use Case
builder.Services.AddScoped<IRepositorySearch<SaleModel, Sale>, SaleRepository>();
builder.Services.AddScoped<GetSaleSearchUseCase<SaleModel>>();



builder.Services.AddHttpClient<IExternalService<PostServiceDTO>,PostService>(c =>
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

app.MapGet("/beer", async (GetBeerUseCase<Beer, BeerViewModel> beerUseCase) => 
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("beers")
.WithOpenApi();

app.MapPost("/beer", async (BeerRequestDTO beerRequest, AddBeerUseCase<BeerRequestDTO> beerUseCase,
    IValidator<BeerRequestDTO> validator) =>
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

app.MapGet("/beerDetail", async (GetBeerUseCase<Beer, BeerDetailViewModel> beerUseCase) =>
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

