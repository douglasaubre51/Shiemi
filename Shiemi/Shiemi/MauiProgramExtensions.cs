using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Shiemi.PageModels;
using Shiemi.PageModels.Chat;
using Shiemi.PageModels.Market;
using Shiemi.PageModels.Project;
using Shiemi.PageModels.Start;
using Shiemi.PageModels.User;
using Shiemi.Services;
using Shiemi.Storage;
using Shiemi.Utilities;
using Shiemi.Utilities.HubClients;
using Shiemi.Views.DevViews;

namespace Shiemi;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
			.UseMauiCommunityToolkitMarkup()
            .UseMauiCommunityToolkit(options =>
            {
                options.SetShouldEnableSnackbarOnWindows(true);
            })
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

        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<ProjectService>();
        builder.Services.AddTransient<ChatService>();
        builder.Services.AddTransient<ReviewService>();
        builder.Services.AddTransient<DevService>();


        // Add HubClients
        builder.Services.AddSingleton<ChannelClient>();
        builder.Services.AddSingleton<RoomClient>();
        builder.Services.AddSingleton<ProjectClient>();


        // Add Page Models
        builder.Services.AddSingleton<BasePageModel>();

        // start
        builder.Services.AddTransient<IndexPageModel>();

        // user
        builder.Services.AddTransient<ProfilePageModel>();
        builder.Services.AddTransient<PageModels.User.EditPageModel>();

        // project
        builder.Services.AddTransient<ProjectsPageModel>();
        builder.Services.AddTransient<CreateProjectPageModel>();
        builder.Services.AddTransient<PageModels.Project.DetailsPageModel>();

        // market
        builder.Services.AddTransient<ProjectShopPageModel>();
        builder.Services.AddTransient<ProjectDetailsPageModel>();
        builder.Services.AddTransient<PrivateRoomPageModel>();

        // chat
        builder.Services.AddTransient<RoomsPageModel>();
        builder.Services.AddTransient<ChannelsPageModel>();

        // Dev
        builder.Services.AddTransient<PageModels.Dev.EditPageModel>();
        builder.Services.AddTransient<PageModels.Dev.MarketpageModel>();
        builder.Services.AddTransient<PageModels.Dev.DetailsPageModel>();

        return builder;
    }
}
