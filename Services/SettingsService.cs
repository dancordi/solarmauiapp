using System.Text.Json;
using SolarMauiApp.Models;

namespace SolarMauiApp.Services;

public class SettingsService
{
    private const string SettingsFileName = "settings.json";
    private readonly string _filePath;

    public SettingsService()
    {
        _filePath = Path.Combine(FileSystem.AppDataDirectory, SettingsFileName);
    }

    public async Task<AppSettings> LoadSettingsAsync()
    {
        if (!File.Exists(_filePath))
            return new AppSettings();

        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
    }

    public async Task SaveSettingsAsync(AppSettings settings)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
    }
}
