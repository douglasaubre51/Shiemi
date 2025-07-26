using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Shiemi.Helpers;
using Shiemi.Services;
using Shiemi.ViewModels;
using System.Text.Json;

namespace Shiemi
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // helpers
            builder.Services.AddTransient<SignInValidator>();
            builder.Services.AddTransient<SignUpValidator>();

            // services
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );

            // REST
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<ProjectService>();


            // view models
            builder.Services.AddTransient<SignInVM>();
            builder.Services.AddTransient<SignUpVM>();
            builder.Services.AddTransient<UserProfileVM>();
            builder.Services.AddTransient<ProjectVM>();
            builder.Services.AddTransient<CreateProjectVM>();
            builder.Services.AddTransient<MarketPlaceVM>();
            builder.Services.AddTransient<ProjectInfoVM>();

            // widget models


            return builder.Build();
        }
    }
}
