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
using CA_ApplicationLayer.EMP_Empresa;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_FrameworksDrivers_API.Endpoints;
using CA_FrameworksDrivers_API.Services;
using CA_FrameworksDrivers_API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//validadores
builder.Services.AddValidatorsFromAssemblyContaining<BeerValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        // Aquí se define qué orígenes son permitidos.
        policy.WithOrigins("http://localhost:4200")  // Define el origen permitido
              .AllowAnyHeader()   // Permite cualquier encabezado en la solicitud
              .AllowAnyMethod();  // Permite cualquier método (GET, POST, PUT, DELETE, etc.)
    });
});



//dependencias
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
    }
);

builder.Services.Configure<TimeZoneSettings>(builder.Configuration.GetSection(TimeZoneSettings.SectionName));
builder.Services.AddSingleton<ITimeZoneInfoProvider, TimeZoneInfoProvider>();

builder.Services.AddScoped(typeof(ILstPresenterResponse<,>), typeof(LstItemResponsePresenter<,>));
builder.Services.AddScoped(typeof(IPresenterResponse<,>), typeof(ItemResponsePresenter<,>));
builder.Services.AddScoped(typeof(ILstPagPresenterResponse<,>), typeof(LstItemPaginationResponsePresenter<,>));

builder.Services.AddEMP_EmpresaServices();
builder.Services.AddDepartamentosServices();
builder.Services.AddProvinciasServices();
builder.Services.AddDistritosServices();
builder.Services.AddConstanteServices();
builder.Services.AddCatMinisterioServices();
builder.Services.AddDirDirectorServices();


//builder.Services.AddScoped<IRepository<Beer>, Repository>();
builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
builder.Services.AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>();
builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();
builder.Services.AddScoped<IExternalService<PostServiceDTO>, PostService>();
builder.Services.AddScoped<IExternalServiceAdapter<Post>, PostExternalServiceAdapter>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<GetPostUseCase>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");  // Aplica la política que definimos anteriormente

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();



app.MapEmpEmpresasEndpoints();
app.MapDepartamentosEndpoints();
app.MapProvinciasEndpoints();
app.MapDistritosEndpoints();
app.MapConstanteEndpoints();
app.MapCatMinisterioEndpoints();
app.MapDirDirectorioEndpoints();

app.Run();

