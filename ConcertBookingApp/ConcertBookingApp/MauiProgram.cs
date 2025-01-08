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
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<ConcertsViewModel>(); // Register ConcertsViewModel with DI

            // Register HttpClient for ApiService
            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5263");  // Use the correct API address
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}