using CA_ApplicationLayer.Common.IPresenterFactory;
using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_EntrerpriseLayer;

namespace CA_ApplicationLayer.UseCases.BeersUseCases
{
    public class GetBeerUseCase<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _beerRepository;
        private readonly IPresenterGetAll<TEntity, TOutput> _presenter;
        public GetBeerUseCase(IRepository<TEntity> beerRepository,
            IPresenterGetAll<TEntity,TOutput> presenter)
        {
            _beerRepository = beerRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var beers = await _beerRepository.GetAllAsync();
            return _presenter.PresentGetAll(beers);
        }
    }
}
