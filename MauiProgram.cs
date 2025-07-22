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
            builder.Services.AddSingleton(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );

            // storage
            builder.Services.AddTransient<StorageService>();

            // REST
            builder.Services.AddTransient<UserService>();
            builder.Services.AddTransient<ProjectService>();


            // view models
            builder.Services.AddTransient<SignInVM>();
            builder.Services.AddTransient<SignUpVM>();
            builder.Services.AddTransient<UserProfileVM>();
            builder.Services.AddTransient<ProjectVM>();

            return builder.Build();
        }
    }
}
