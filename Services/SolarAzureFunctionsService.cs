using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json; 

namespace SolarMauiApp.Services
{
    public class PacItem
    {
        public DateTime TimeStamp { get; set; }
        public double Pac { get; set; }
    }

    public interface ISolarAzureFunctionsService
    {
        Task<PacItem?> GetLastPacItem(int inverterId);
    }

    public class SolarAzureFunctionsService : ISolarAzureFunctionsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _functionUrl;
        private readonly string _functionCode;

        public SolarAzureFunctionsService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            _functionUrl = configuration!["AzureFunctions:BaseUrl"]!;
            _functionCode = configuration!["AzureFunctions:Code"]!;
        }

        public async Task<PacItem?> GetLastPacItem(int inverterId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"{_functionUrl}/api/GetLastSolarPacItem?code={_functionCode}&inverterId={inverterId}";
            Uri uri = new Uri(url);

            try
            {
                StringContent body = new StringContent("{}", Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PacItem>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }
    }
}