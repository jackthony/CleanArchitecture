using CA_ApplicationLayer.Common.IExternalServices;
using CA_EntrerpriseLayer.PostModule;
using CA_InterfaceAdapters_Adapters.PostModule.Dtos;

namespace CA_InterfaceAdapters_Adapters.PostModule
{
    public class PostExternalServiceAdapter : IESGetAllDataAsync<Post>
    {
        private readonly IEServiceGetContentAsync<PostServiceDTO> _externalService;
        
        public PostExternalServiceAdapter(IEServiceGetContentAsync<PostServiceDTO> externalService)
        {
            _externalService = externalService;
        }

        public async Task<IEnumerable<Post>> GetDataAsync()
        {
            var postsES = await _externalService.GetContentAsync();
            var post = postsES.Select(p => new Post
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body
            });
            return post;
        }

    }
}
