using CA_ApplicationLayer;
using CA_InterfaceAdapters_Adapters.PostModule;
using CA_InterfaceAdapters_Adapters.PostModule.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CA_FrameworkDrivers_ExternalService.PostModule
{
    public class PostService : IEServiceGetContentAsync<PostServiceDTO>
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<IEnumerable<PostServiceDTO>> GetContentAsync()
        {   
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PostServiceDTO>>
                (responseData, _options) 
                ?? Enumerable.Empty<PostServiceDTO>();

        }
    }
}
