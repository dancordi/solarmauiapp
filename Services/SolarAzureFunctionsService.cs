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
        private readonly SettingsService _settingsService;

        public SolarAzureFunctionsService(SettingsService settingsService, IHttpClientFactory httpClientFactory)
        {
            _settingsService = settingsService;
            _httpClientFactory = httpClientFactory;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<PacItem?> GetLastPacItem(int inverterId)
        {
            var settings = await _settingsService.LoadSettingsAsync();
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"{settings.BaseUrl}/api/GetLastSolarPacItem?code={settings.ApiKey}&inverterId={inverterId}";
            Uri uri = new Uri(url);

            StringContent body = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, body);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PacItem>(content, _serializerOptions);
            }

            return null;
        }
    }
}