using System.Text.Json;

namespace Frameworks3Frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(json);
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<string?> GetStringAsync(string endpoint)
        {
            try
            {
                return await _httpClient.GetStringAsync(endpoint);
            }
            catch
            {
                return null;
            }
        }
    }
}
