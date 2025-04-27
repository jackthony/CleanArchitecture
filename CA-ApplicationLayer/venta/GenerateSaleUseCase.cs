using CA_ApplicationLayer.Exceptions;
using CA_EntrerpriseLayer.venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.venta
{
    public class GenerateSaleUseCase<TDTO>
    {
        private IRepository<Sale> _repository;
        private readonly IMapper<TDTO, Sale> _mapper;

        public GenerateSaleUseCase(IRepository<Sale> repository, IMapper<TDTO, Sale> mapper)
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
