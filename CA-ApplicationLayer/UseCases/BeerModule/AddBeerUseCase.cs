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
        private readonly IRepositoryAddAsync<BeerEntity> _beerRepository;
        private readonly IMapperDtoToOutput<TDTO, BeerEntity> _mapper;

        public AddBeerUseCase(IRepositoryAddAsync<BeerEntity> beerRepository, 
            IMapperDtoToOutput<TDTO, BeerEntity> mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> ExecuteAsync(TDTO beerDTO)
        {
            var beer = _mapper.ToEntity(beerDTO);

            var validationError = ValidateBeer(beer);
            if (validationError != null)
            {
                return Result.Failure<Unit>(validationError);
            }

            try
            {
                await _beerRepository.AddAsync(beer);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure<Unit>(Error.Create(ex.Message));
            }
        }


        private static Error? ValidateBeer(BeerEntity beer)
        {
            if (string.IsNullOrWhiteSpace(beer.Name))
                return Error.Create("El nombre de la cerveza es obligatorio.");

            if (string.IsNullOrWhiteSpace(beer.Style))
                return Error.Create("El estilo de la cerveza es obligatorio.");

            if (beer.Alcohol <= 0)
                return Error.Create("El porcentaje de alcohol debe ser mayor a 0.");
            return null;
        }
    }
}
