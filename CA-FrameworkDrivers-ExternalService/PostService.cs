using CA_ApplicationLayer;
using CA_InterfaceAdapters_Adapters;
using CA_InterfaceAdapters_Adapters.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CA_FrameworkDrivers_ExternalService
{
    public class PostService : IExternalService<PostServiceDTO>
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        public PostService()
        {
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<IEnumerable<PostServiceDTO>> GetContentAsync()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PostServiceDTO>>
                (responseData, _options) 
                ?? Enumerable.Empty<PostServiceDTO>();

        }
    }
}
