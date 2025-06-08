using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SolarMauiApp.Services;

namespace SolarMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<ISolarAzureFunctionsService, SolarAzureFunctionsService>();
        builder.Services.AddHttpClient();


        return builder.Build();
	}
}
