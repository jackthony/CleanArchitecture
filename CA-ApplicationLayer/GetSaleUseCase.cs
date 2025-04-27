using CA_EntrerpriseLayer.venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class GetSaleUseCase
    {
        private readonly IRepository<Sale> _repository;

        public GetSaleUseCase(IRepository<Sale> repository)
            => _repository = repository;

        public async Task<IEnumerable<Sale>> ExecuteAsync()
        => await _repository.GetAllAsync();
    }
}
