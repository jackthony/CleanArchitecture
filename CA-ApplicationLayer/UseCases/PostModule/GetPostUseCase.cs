using CA_ApplicationLayer.Common.IExternalServices;
using CA_EntrerpriseLayer.PostModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.UseCases.PostsUseCases
{
    public class GetPostUseCase
    {
        private readonly IESGetAllDataAsync<Post> _adapter;

        public GetPostUseCase(IESGetAllDataAsync<Post> adapter)
            => _adapter = adapter;

        public async Task<IEnumerable<Post>> ExecuteAsync()
            => await _adapter.GetDataAsync();


    }
}
