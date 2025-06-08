using SolarMauiApp.Models;
using SolarMauiApp.Services;

namespace SolarMauiApp.Views;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsService _settingsService;

    public SettingsPage(SettingsService settingsService)
    {
        InitializeComponent();
        _settingsService = settingsService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var settings = await _settingsService.LoadSettingsAsync();
        BaseUrlEntry.Text = settings.BaseUrl;
        ApiKeyEntry.Text = settings.ApiKey;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var settings = new AppSettings
        {
            BaseUrl = BaseUrlEntry.Text,
            ApiKey = ApiKeyEntry.Text
        };

        await _settingsService.SaveSettingsAsync(settings);

        StatusLabel.Text = "💾 Impostazioni salvate";
    }
}