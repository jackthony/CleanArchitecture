using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Data.Contexts.EfCore;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Models.BeerModule;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository.BeerModule
{
    public class BeerRepository : IRepositoryAddAsync<Beer>, 
        IRepositoryGetAllAsync<Beer>, IRepositoryGetByIdAsync<Beer>, IRepositoryGetByPagination<Beer>, IRepositoryUpdateAsync<Beer>,
        IRepositoryDeleteAsync<Beer>
    {
        private readonly AppDbContext _dbContext;

        public BeerRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<int> AddAsync(Beer beer)
        {
            var existingBeer = await _dbContext.Beers
                .FirstOrDefaultAsync(b => b.sNombre == beer.Name && b.sEstilo == beer.Style);

            if (existingBeer != null)
            {
                // Si la cerveza ya existe, puedes actualizarla en lugar de agregarla.
                existingBeer.dAlcohol = beer.Alcohol; // Actualiza cualquier campo necesario
                await _dbContext.SaveChangesAsync();
                return existingBeer.nIdBeer; // Retorna el ID del elemento existente
            }
            else
            {
                var beerModel = new BeerModel
                {
                    sNombre = beer.Name,
                    sEstilo = beer.Style,
                    dAlcohol = beer.Alcohol,
                    dtFechaCrecion = DateTime.UtcNow, // Asigna la fecha de creación actual
                    sUsuarioCreacion = "System", // Asigna un valor por defecto para el usuario de creación
                    dtFechaActualizacion = null, // Inicialmente no hay actualización
                    sUsuarioActualizacion = null // Inicialmente no hay usuario de actualización
                };
                await _dbContext.Beers.AddAsync(beerModel);
                await _dbContext.SaveChangesAsync();
                return beerModel.nIdBeer; // Retorna el ID del nuevo elemento agregado
            }
        }

        public async Task DeleteAsync(Beer entity)
        {
            
        }

        public async Task<IEnumerable<Beer>> GetAllAsync()
        {
            return await _dbContext.Beers
                .Select(b => new Beer
                {
                    Id = b.nIdBeer,
                    Name = b.sNombre,
                    Style = b.sEstilo,
                    Alcohol = b.dAlcohol,
                    DateCreate = b.dtFechaCrecion,
                    userCreated = b.sUsuarioCreacion,
                    DateUpdate = b.dtFechaActualizacion ?? DateTime.UtcNow, // Default value if null
                    userUpdated = b.sUsuarioActualizacion ?? "System" // Default value if null
                })
                .ToListAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            var beerModel = await _dbContext.Beers.FindAsync(id);
            return new Beer
            {
                Id = beerModel.nIdBeer,
                Name = beerModel.sNombre,
                Style = beerModel.sEstilo,
                Alcohol = beerModel.dAlcohol,
                DateCreate = beerModel.dtFechaCrecion,
                userCreated = beerModel.sUsuarioCreacion,
                DateUpdate = beerModel.dtFechaActualizacion ?? DateTime.UtcNow,
                userUpdated = beerModel.sUsuarioActualizacion ?? "System" // Default value if null
            };
        }

        public IQueryable<Beer> GetByPagination(int pageNumber, int pageSize, out int totalCount, Func<IQueryable<Beer>, IQueryable<Beer>>? orderBy = null, Func<IQueryable<Beer>, IQueryable<Beer>>? filter = null)
        {
            // Obtenemos la consulta base desde el DbSet<BeerModel>
            IQueryable<BeerModel> query = _dbContext.Beers;  // DbSet<BeerModel>

            // Si se proporciona un filtro, lo aplicamos
            if (filter != null)
            {
                // Convertimos el filtro para que opere sobre BeerModel en lugar de Beer
                Func<IQueryable<BeerModel>, IQueryable<BeerModel>> adaptedFilter = q => filter(
                    q.Select(b => new Beer
                    {
                        Id = b.nIdBeer,
                        Name = b.sNombre,
                        Style = b.sEstilo,
                        Alcohol = b.dAlcohol,
                        DateCreate = b.dtFechaCrecion,
                        userCreated = b.sUsuarioCreacion,
                        DateUpdate = b.dtFechaActualizacion ?? DateTime.UtcNow,
                        userUpdated = b.sUsuarioActualizacion
                    })
                ).Select(b => new BeerModel
                {
                    nIdBeer = b.Id,
                    sNombre = b.Name,
                    sEstilo = b.Style,
                    dAlcohol = b.Alcohol,
                    dtFechaCrecion = b.DateCreate,
                    sUsuarioCreacion = b.userCreated,
                    dtFechaActualizacion = b.DateUpdate,
                    sUsuarioActualizacion = b.userUpdated
                });

                query = adaptedFilter(query);
            }

            // Si se proporciona un orden, lo aplicamos
            if (orderBy != null)
            {
                // Convertimos el orden para que opere sobre BeerModel en lugar de Beer
                Func<IQueryable<BeerModel>, IQueryable<BeerModel>> adaptedOrderBy = q => orderBy(
                    q.Select(b => new Beer
                    {
                        Id = b.nIdBeer,
                        Name = b.sNombre,
                        Style = b.sEstilo,
                        Alcohol = b.dAlcohol,
                        DateCreate = b.dtFechaCrecion,
                        userCreated = b.sUsuarioCreacion,
                        DateUpdate = b.dtFechaActualizacion ?? DateTime.UtcNow,
                        userUpdated = b.sUsuarioActualizacion
                    })
                ).Select(b => new BeerModel
                {
                    nIdBeer = b.Id,
                    sNombre = b.Name,
                    sEstilo = b.Style,
                    dAlcohol = b.Alcohol,
                    dtFechaCrecion = b.DateCreate,
                    sUsuarioCreacion = b.userCreated,
                    dtFechaActualizacion = b.DateUpdate,
                    sUsuarioActualizacion = b.userUpdated
                });

                query = adaptedOrderBy(query);
            }

            // Convertimos de BeerModel a Beer explícitamente usando Select
            IQueryable<Beer> beerQuery = query.Select(b => new Beer
            {
                Id = b.nIdBeer,  // Mapeo de campos de BeerModel a Beer
                Name = b.sNombre,
                Style = b.sEstilo,
                Alcohol = b.dAlcohol,
                DateCreate = b.dtFechaCrecion,
                userCreated = b.sUsuarioCreacion,
                DateUpdate = b.dtFechaActualizacion ?? DateTime.UtcNow,
                userUpdated = b.sUsuarioActualizacion
            });

            // Calculamos el total de elementos disponibles
            totalCount = beerQuery.Count();

            // Aplicamos la paginación
            beerQuery = beerQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Retornamos la consulta paginada
            return beerQuery;
        }

        public async Task UpdateAsync(Beer entity)
        {
            // Buscar la cerveza existente en la base de datos
            var existingBeer = await _dbContext.Beers.FirstOrDefaultAsync(b => b.nIdBeer == entity.Id);

            // Si la cerveza no existe, lanzar una excepción o manejarlo de la forma que se desee
            if (existingBeer == null)
            {
                throw new InvalidOperationException("Cerveza no encontrada.");
            }

            // Actualizar los campos de la cerveza existente con los nuevos valores
            existingBeer.sNombre = entity.Name;
            existingBeer.sEstilo = entity.Style;
            existingBeer.dAlcohol = entity.Alcohol;
            existingBeer.dtFechaActualizacion = DateTime.UtcNow; // Actualizamos la fecha de modificación
            existingBeer.sUsuarioActualizacion = entity.userUpdated; // Asumimos que esta información proviene de la entidad

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();
        }

    }
}
