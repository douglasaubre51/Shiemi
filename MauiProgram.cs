using System.Text.Json;
using Microsoft.Extensions.Logging;
using Shiemi.Helpers;
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
            // helpers
            builder.Services.AddTransient<SignInValidator>();

            // services
            builder.Services.AddSingleton(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            // REST
            builder.Services.AddTransient<SendUserDetailsService>();
            builder.Services.AddTransient<UserService>();

            // view models
            builder.Services.AddTransient<SignInVM>();
            builder.Services.AddTransient<SignUpVM>();

            return builder.Build();
        }
    }
}
