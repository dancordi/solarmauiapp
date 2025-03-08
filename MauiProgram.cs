using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SolarMauiApp.Services;

namespace SolarMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
							 .AddUserSecrets<MainPage>(optional: true, reloadOnChange: true);

        builder.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        //var configuration = builder.Configuration;
        //builder.Services.AddSingleton<IConfiguration>(configuration);

        builder.Services.AddSingleton<ISolarAzureFunctionsService, SolarAzureFunctionsService>();
        builder.Services.AddHttpClient();


        return builder.Build();
	}
}
