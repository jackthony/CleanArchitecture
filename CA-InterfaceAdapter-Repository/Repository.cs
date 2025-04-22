using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Data;
using CA_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapter_Repository
{
    public class Repository : IRepository<Beer>
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Beer beer)
        {
            var beerModel = new BeerModel
            {
                Id = beer.Id,
                Name = beer.Name,
                Style = beer.Style
            };
            await _dbContext.Beers.AddAsync(beerModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Beer>> GetAllAsync()
        {
            return await _dbContext.Beers
                .Select(b => new Beer
                {
                    Id = b.Id,
                    Name = b.Name,
                    Style = b.Style
                })
                .ToListAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            var beerModel = await _dbContext.Beers.FindAsync(id);
            return new Beer
            {
                Id = beerModel.Id,
                Name = beerModel.Name,
                Style = beerModel.Style,
                Alcohol = beerModel.Alcohol
            };
        }
    }
}
