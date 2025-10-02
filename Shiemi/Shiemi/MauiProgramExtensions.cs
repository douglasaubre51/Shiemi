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
        builder.Services.AddTransient<EnvironmentStorage>();
        builder.Services.AddSingleton<UserStorage>();


        // Add Services.
        builder.Services.AddSingleton<RestClient>();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<AuthService>();


        // Add Page Models.
        builder.Services.AddTransient<IndexPageModel>();
        builder.Services.AddTransient<ProfilePageModel>();


        return builder;
    }
}
