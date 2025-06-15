using CA_ApplicationLayer.Exceptions;
using System.ComponentModel.DataAnnotations;
using ROP;
using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_ApplicationLayer.Common.IMappersFactory;
using CA_EntrerpriseLayer.BeerModule;

namespace CA_ApplicationLayer.UseCases.BeersUseCases
{
    public class AddBeerUseCase<TDTO>
    {
        private readonly IRepositoryAddAsync<Beer> _beerRepository;
        private readonly IMapperDtoToOutput<TDTO, Beer> _mapper;

        public AddBeerUseCase(IRepositoryAddAsync<Beer> beerRepository, 
            IMapperDtoToOutput<TDTO, Beer> mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> ExecuteAsync(TDTO beerDTO)
        {
            var beer = _mapper.ToEntity(beerDTO);

            if (string.IsNullOrWhiteSpace(beer.Name))
                return Result.Failure<Unit>(Error.Create("El nombre de la cerveza es obligatorio."));
            await _beerRepository.AddAsync(beer);

            return Result.Success();
        }
    }
}
