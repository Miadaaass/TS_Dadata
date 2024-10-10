using TS_Dadata.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TS_Dadata.Services
{
    public class DadataService : IDadataService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DadataService> _logger;

        public DadataService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DadataService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AddressResponse> CleanAddressAsync(string address)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Token {_configuration["Dadata:ApiKey"]}");

            var requestBody = new { query = address };
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://dadata.ru/api/clean/address/", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AddressResponse>(jsonResponse);

                return result;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Ошибка при вызове Dadata API");
                throw new Exception("Ошибка при обращении к Dadata API");
            }
        }
    }
}
