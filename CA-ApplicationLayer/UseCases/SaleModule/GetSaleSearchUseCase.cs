using CA_ApplicationLayer.Common.IRepositoriesFactory;
using CA_EntrerpriseLayer.venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.UseCases.SalesUseCases
{
    public class GetSaleSearchUseCase<TModel>
    {
        private readonly IRepositorySearch<TModel, Sale> _repository;
        
        public GetSaleSearchUseCase(IRepositorySearch<TModel, Sale> repository)
            => _repository = repository;

        public async Task<IEnumerable<Sale>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
            => await _repository.GetAsync(predicate);
    }
}
