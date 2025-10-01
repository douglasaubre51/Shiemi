using Microsoft.Extensions.Logging;
using Shiemi.PageModels;
using Shiemi.Services;
using Shiemi.Storage;

namespace Shiemi;

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

        // Add Storage

        builder.Services.AddSingleton<UserStorage>();
        builder.Services.AddTransient<EnvironmentStorage>();

        // Add Services.

        builder.Services.AddTransient<AuthService>();

        // Add Page Models.

        builder.Services.AddTransient<IndexPageModel>();

        return builder;
    }
}
