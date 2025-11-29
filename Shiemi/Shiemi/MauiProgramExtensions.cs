using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Shiemi.PageModels;
using Shiemi.PageModels.Chat;
using Shiemi.PageModels.Market;
using Shiemi.PageModels.Project;
using Shiemi.PageModels.Start;
using Shiemi.PageModels.User;
using Shiemi.Services;
using Shiemi.Services.ChatServices;
using Shiemi.Storage;
using Shiemi.Utilities;
using Shiemi.Utilities.HubClients;

namespace Shiemi;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit(options =>
            {
                options.SetShouldEnableSnackbarOnWindows(true);
            })
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


        // Add Rest Services
        // singleton
        builder.Services.AddSingleton<RestClient>();
        // transient
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<ProjectService>();


        // Add Chat Services
        // singleton
        builder.Services.AddSingleton<ChatService>();


        // Add Utilites
        // Hub Clients
        builder.Services.AddSingleton<ChannelClient>();
        builder.Services.AddSingleton<RoomClient>();
        builder.Services.AddSingleton<ProjectClient>();


        // Add Page Models
        builder.Services.AddSingleton<BasePageModel>();
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
        // chat
        builder.Services.AddTransient<RoomsPageModel>();
        builder.Services.AddTransient<ChannelsPageModel>();


        return builder;
    }
}
