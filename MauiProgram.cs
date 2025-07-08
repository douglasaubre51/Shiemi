using Microsoft.Extensions.Logging;
using Shiemi.Services;
using Shiemi.ViewModels;

namespace Shiemi
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

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // middlewares
            // services
            builder.Services.AddTransient<SendUserDetailsService>();

            // view models
            builder.Services.AddTransient<SignUpVM>();

            return builder.Build();
        }
    }
}
