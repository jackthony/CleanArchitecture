using CA_ApplicationLayer.Exceptions;
using CA_EntrerpriseLayer;
using System.ComponentModel.DataAnnotations;
using ROP;

namespace CA_ApplicationLayer
{
    public class AddBeerUseCase<TDTO>
    {
        private readonly IRepository<Beer> _beerRepository;
        private readonly IMapper<TDTO, Beer> _mapper;

        public AddBeerUseCase(IRepository<Beer> beerRepository, 
            IMapper<TDTO, Beer> mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> ExecuteAsync(TDTO beerDTO)
        {
            if (beerDTO == null)
                return Result.Failure<Unit>(Error.Create("Beer data is required"));

            var beer = _mapper.ToEntity(beerDTO);

            if (string.IsNullOrWhiteSpace(beer.Name))
                return Result.Failure<Unit>(Error.Create("El nombre de la cerveza es obligatorio."));
            await _beerRepository.AddAsync(beer);

            return Result.Success();
        }
        //public async Task ExecuteAsync(TDTO beerDTO)
        //{
        //    var beer = _mapper.ToEntity(beerDTO);

        //    if(string.IsNullOrEmpty(beer.Name))
        //        throw new Exceptions.ValidationException("El nombre de la cerveza es obligatorio.");

        //    await _beerRepository.AddAsync(beer);
        //}
    }
}
