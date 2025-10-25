using Microsoft.Extensions.Logging;
using Shiemi.PageModels;
using Shiemi.PageModels.Market;
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


        // Add Storage Services
        builder.Services.AddSingleton<EnvironmentStorage>();
        builder.Services.AddSingleton<UserStorage>();


        // Add Rest Services
        // singleton
        builder.Services.AddSingleton<RestClient>();

        // transient
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<ProjectService>();


        // Add Chat Services
        // singleton
        builder.Services.AddSingleton<MessageService>();
        builder.Services.AddSingleton<RoomService>();


        // Add Page Models
        // start
        builder.Services.AddTransient<IndexPageModel>();

        // user
        builder.Services.AddTransient<ProfilePageModel>();
        builder.Services.AddTransient<ProjectsPageModel>();
        builder.Services.AddTransient<CreateProjectPageModel>();

        // market
        builder.Services.AddTransient<ProjectShopPageModel>();
        builder.Services.AddTransient<ProjectDetailsPageModel>();
        builder.Services.AddTransient<PrivateRoomPageModel>();



        return builder;
    }
}
