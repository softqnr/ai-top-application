using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AiTopStudentStatus;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		//builder.Services.AddHttpClient("api", httpClient => httpClient.BaseAddress = new Uri("http://localhost:5170/WeatherForecast"));

		return builder.Build();
	}
}
