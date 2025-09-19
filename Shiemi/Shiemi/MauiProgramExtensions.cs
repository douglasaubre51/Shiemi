using Microsoft.Extensions.Logging;
using Shiemi.PageModels;
using Shiemi.Services;

namespace Shiemi
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
        {
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

            // Add Services.

            builder.Services.AddTransient<AuthService>();

            // Environment variables storage service
            builder.Services.AddTransient<EnvironmentService>();

            // Add Page Models.

            builder.Services.AddSingleton<IndexPageModel>();

            return builder;
        }
    }
}
