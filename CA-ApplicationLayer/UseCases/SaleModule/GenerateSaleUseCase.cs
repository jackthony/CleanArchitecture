using CA_ApplicationLayer.Common.IMappersFactory;
using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_ApplicationLayer.Exceptions;
using CA_EntrerpriseLayer.venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.UseCases.SalesUseCases
{
    public class GenerateSaleUseCase<TDTO>
    {
        private IRepository<Sale> _repository;
        private readonly IMapperDtoToOutput<TDTO, Sale> _mapper;

        public GenerateSaleUseCase(IRepository<Sale> repository, IMapperDtoToOutput<TDTO, Sale> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO dto)
        {
            var sale = _mapper.ToEntity(dto);
            
            if(sale.Concepts.Count == 0)
            {
                throw new ValidationException("La venta no puede estar vacía");
            }
            if(sale.Total <= 0)
            {
                throw new ValidationException("La venta no puede ser menor o igual a cero");
            }
            await _repository.AddAsync(sale);
        }
    }
}
