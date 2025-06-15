using System.Linq.Expressions;
using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Data.Contexts.EfCore;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Models.BeerModule;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository.BeerModule
{
    public class BeerRepository : IRepositoryAddAsync<BeerEntity>,
        IRepositoryGetAllAsync<BeerEntity>, IRepositoryGetByIdAsync<BeerEntity>, IRepositoryGetByPagination<BeerEntity>, IRepositoryUpdateAsync<BeerEntity>,
        IRepositoryDeleteAsync<BeerEntity>, IRepositorySearch<BeerModel, BeerEntity>
    {


        private readonly AppDbContext _dbContext;

        public BeerRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<int> AddAsync(BeerEntity beer)
        {
            if (beer == null)
                throw new ArgumentNullException(nameof(beer));

            var exists = await _dbContext.Beers
                .AnyAsync(b => b.sNombre == beer.Name && b.sEstilo == beer.Style);

            if (exists)
                throw new InvalidOperationException("Beer already exists.");

            var beerModel = new BeerModel
            {
                sNombre = beer.Name,
                sEstilo = beer.Style,
                dAlcohol = beer.Alcohol,
                dtFechaCreacion = DateTime.UtcNow,
                sUsuarioCreacion = beer.userCreated,
            };

            await _dbContext.Beers.AddAsync(beerModel);
            await _dbContext.SaveChangesAsync();

            return beerModel.nIdBeer;
        }

        public async Task DeleteAsync(BeerEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingBeer = await _dbContext.Beers.FindAsync(entity.Id);

            if (existingBeer == null)
                throw new InvalidOperationException("Beer not found.");

            _dbContext.Beers.Remove(existingBeer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BeerEntity>> GetAllAsync()
        {
            return await _dbContext.Beers
                .AsNoTracking()
                .Select(b => new BeerEntity
                {
                    Id = b.nIdBeer,
                    Name = b.sNombre,
                    Style = b.sEstilo,
                    Alcohol = b.dAlcohol,
                    DateCreate = b.dtFechaCreacion ?? DateTime.UtcNow,
                    userCreated = b.sUsuarioCreacion,
                    DateUpdate = b.dtFechaActualizacion ?? DateTime.UtcNow, // Default value if null
                    userUpdated = b.sUsuarioActualizacion ?? "System" // Default value if null
                })
                .ToListAsync();
        }

        public async Task<BeerEntity> GetByIdAsync(int id)
        {
            var beerModel = await _dbContext.Beers
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.nIdBeer == id);

            if (beerModel == null)
                throw new InvalidOperationException("Beer not found.");

            return new BeerEntity
            {
                Id = beerModel.nIdBeer,
                Name = beerModel.sNombre,
                Style = beerModel.sEstilo,
                Alcohol = beerModel.dAlcohol,
                DateCreate = beerModel.dtFechaCreacion ?? DateTime.UtcNow,
                userCreated = beerModel.sUsuarioCreacion,
                DateUpdate = beerModel.dtFechaActualizacion ?? DateTime.UtcNow,
                userUpdated = beerModel.sUsuarioActualizacion ?? ""
            };
        }

        public IQueryable<BeerEntity> GetByPagination(int pageNumber, int pageSize, out int totalCount, Func<IQueryable<BeerEntity>, IQueryable<BeerEntity>>? orderBy = null, Func<IQueryable<BeerEntity>, IQueryable<BeerEntity>>? filter = null)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            // Obtenemos la consulta base desde el DbSet<BeerModel>
            IQueryable<BeerModel> query = _dbContext.Beers.AsNoTracking();

            // Si se proporciona un filtro, lo aplicamos
            if (filter != null)
            {
                // Convertimos el filtro para que opere sobre BeerModel en lugar de Beer
                Func<IQueryable<BeerModel>, IQueryable<BeerModel>> adaptedFilter = q => filter(
                    q.Select(b => new BeerEntity
                    {
                        Id = b.nIdBeer,
                        Name = b.sNombre,
                        Style = b.sEstilo,
                        Alcohol = b.dAlcohol,
                        DateCreate = b.dtFechaCreacion ?? DateTime.UtcNow,
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
                    dtFechaCreacion = b.DateCreate,
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
                    q.Select(b => new BeerEntity
                    {
                        Id = b.nIdBeer,
                        Name = b.sNombre,
                        Style = b.sEstilo,
                        Alcohol = b.dAlcohol,
                        DateCreate = b.dtFechaCreacion ?? DateTime.UtcNow,
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
                    dtFechaCreacion = b.DateCreate,
                    sUsuarioCreacion = b.userCreated,
                    dtFechaActualizacion = b.DateUpdate,
                    sUsuarioActualizacion = b.userUpdated
                });

                query = adaptedOrderBy(query);
            }

            // Convertimos de BeerModel a Beer explícitamente usando Select
            IQueryable<BeerEntity> beerQuery = query.Select(b => new BeerEntity
            {
                Id = b.nIdBeer,  // Mapeo de campos de BeerModel a Beer
                Name = b.sNombre,
                Style = b.sEstilo,
                Alcohol = b.dAlcohol,
                DateCreate = b.dtFechaCreacion ?? DateTime.UtcNow,
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

        public async Task UpdateAsync(BeerEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingBeer = await _dbContext.Beers
                .FirstOrDefaultAsync(b => b.nIdBeer == entity.Id);

            if (existingBeer == null)
                throw new InvalidOperationException("Beer not found.");

            var duplicated = await _dbContext.Beers
                .AnyAsync(b => b.nIdBeer != entity.Id &&
                               b.sNombre == entity.Name &&
                               b.sEstilo == entity.Style);

            if (duplicated)
                throw new InvalidOperationException("Another beer with same name and style already exists.");

            existingBeer.sNombre = entity.Name;
            existingBeer.sEstilo = entity.Style;
            existingBeer.dAlcohol = entity.Alcohol;
            existingBeer.dtFechaActualizacion = DateTime.UtcNow;
            existingBeer.sUsuarioActualizacion = entity.userUpdated;

            await _dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<BeerEntity>> GetAsync(Expression<Func<BeerModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

    }
}
