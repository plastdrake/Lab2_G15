using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services and ViewModels for DI
            builder.Services.AddSingleton<IApiService, ApiService>(); // Register ApiService
            builder.Services.AddSingleton<ConcertsViewModel>(); // Register ConcertsViewModel with DI
            builder.Services.AddSingleton<MainPageViewModel>(); // Register MainPageViewModel with DI

            // Register HttpClient for ApiService
            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5263");  // Use the correct API address
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

#if DEBUG
            builder.Logging.AddDebug();  // Add debug logging for debugging purposes
#endif

            return builder.Build(); // Build and return the MauiApp
        }
    }
}
