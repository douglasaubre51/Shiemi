using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Shiemi.PageModels;
using Shiemi.PageModels.Chat;
using Shiemi.PageModels.Dev;
using Shiemi.PageModels.Market;
using Shiemi.PageModels.Project;
using Shiemi.PageModels.Start;
using Shiemi.PageModels.User;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities;
using Shiemi.Utilities.HubClients;
using Shiemi.ViewModels;
using Shiemi.Views.DevViews;
using DetailsPageModel = Shiemi.PageModels.Project.DetailsPageModel;
using EditPageModel = Shiemi.PageModels.User.EditPageModel;

namespace Shiemi;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMarkup()
            .UseMauiCommunityToolkit(options => { options.SetShouldEnableSnackbarOnWindows(true); })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-Thin.ttf", "PoppinsThin");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                fonts.AddFont("Poppins-Italic.ttf", "PoppinsItalic");
                fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                fonts.AddFont("Poppins-Medium.ttf", "PoppinsMedium");
                fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Add Storage Services
        builder.Services.AddSingleton<EnvironmentStorage>();

        // Add Widgets
        builder.Services.AddTransient<ChatWidget>();


        // Add Rest Services
        builder.Services.AddSingleton<RestClient>();

        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<ProjectService>();
        builder.Services.AddSingleton<ChatService>();
        builder.Services.AddSingleton<ReviewService>();
        builder.Services.AddSingleton<DevService>();
        builder.Services.AddSingleton<ChannelService>();


        // Add HubClients
        builder.Services.AddSingleton<ChannelClient>();
        builder.Services.AddSingleton<RoomClient>();
        builder.Services.AddSingleton<ProjectClient>();


        // Add Page Models
        builder.Services.AddSingleton<BasePageModel>();
        builder.Services.AddSingleton<FlyoutFooterModel>();

        // start
        builder.Services.AddTransient<IndexPageModel>();

        // user
        builder.Services.AddTransient<ProfilePageModel>();
        builder.Services.AddTransient<EditPageModel>();
        builder.Services.AddTransient<HomePageModel>();
        builder.Services.AddTransient<ViewMoreProjectsPageModel>();
        builder.Services.AddTransient<DevHubPageModel>();

        // project
        builder.Services.AddTransient<ProjectsPageModel>();
        builder.Services.AddTransient<CreateProjectPageModel>();
        builder.Services.AddTransient<DetailsPageModel>();
        builder.Services.AddTransient<AddDevsPageModel>();

        // market
        builder.Services.AddTransient<ProjectShopPageModel>();
        builder.Services.AddTransient<ProjectDetailsPageModel>();
        builder.Services.AddTransient<PrivateRoomPageModel>();

        // chat
        builder.Services.AddSingleton<ChannelsPageModel>();
        builder.Services.AddTransient<RoomsPageModel>();

        // Dev
        builder.Services.AddTransient<PageModels.Dev.EditPageModel>();
        builder.Services.AddTransient<MarketpageModel>();
        builder.Services.AddTransient<PageModels.Dev.DetailsPageModel>();
        builder.Services.AddTransient<PageModels.Dev.ChatPageModel>();

        return builder;
    }
}