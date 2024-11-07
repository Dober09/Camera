using Microsoft.Extensions.Logging;
using Camera.MAUI;
using WorldMap.Service;
using WorldMap.ViewModel;
using WorldMap.View;
using WorldMap.Converter;

namespace WorldMap
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddSingleton<IBarcodeService, BarcodeService>();
            builder.Services.AddTransient<BarcodeViewModel>();
            builder.Services.AddTransient<LandingPage>();

            builder.Services.AddSingleton<NotNullConverter>();
            builder
                .UseMauiApp<App>()
                .UseMauiCameraView()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
