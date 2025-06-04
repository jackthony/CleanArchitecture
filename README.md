# CleanArchitecture

This repository contains a sample .NET 8 solution organized around the principles of Clean Architecture. Each layer lives in its own project so dependencies flow in a single direction.

## Project structure

- **CA-EntrerpriseLayer** – Domain entities and business rules.
- **CA-ApplicationLayer** – Use cases and interfaces that define application logic.
- **CA-InterfaceAdapters-Models** – Data transfer models used by the infrastructure and presentation layers.
- **CA-InterfaceAdapters-Data** – Entity Framework `DbContext` and configuration.
- **CA-InterfaceAdapter-Repository** – Repository implementations that access the database.
- **CA-InterfaceAdapters-Mappers** – Mapping logic between DTOs and domain models.
- **CA-InterfaceAdapters-Presenters** – Presenters used to shape output data.
- **CA-FrameworkDrivers-ExternalService** – Adapter for calling external services.
- **CA-FrameworksDrivers-API** – ASP.NET Core minimal API project.
- **CA-FrameworksDrivers-Console** – Console application that consumes application services.

All projects are referenced by the solution file `CleanArchitecture.sln`.

## Building the solution

Restore dependencies and build all projects:

```bash
dotnet build CleanArchitecture.sln
```

## Running the applications

### API

Run the web API from the root of the repository:

```bash
dotnet run --project CA-FrameworksDrivers-API
```

Swagger is enabled in development so the API documentation is available at `/swagger` when running locally.

### Console

Execute the console application:

```bash
dotnet run --project CA-FrameworksDrivers-Console
```

The console app queries beers from the database and writes them to the terminal.

## Database configuration

Both the API and console projects read their connection string from `appsettings.json` under `ConnectionStrings:DefaultConnection`. Update these files with the values required for your SQL Server instance.

Connection strings can also be supplied via environment variables using the standard .NET configuration syntax. Set an environment variable named `ConnectionStrings__DefaultConnection` before running the application to override the value in the configuration file.

Example:

```bash
export ConnectionStrings__DefaultConnection="Server=localhost;Database=Brewery;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
```

The API respects the `ASPNETCORE_ENVIRONMENT` variable to load environment specific configuration files such as `appsettings.Development.json`.

You can override other settings in the same way. For example, set `BaseUrlPost` to change the URL used for the external post service.
