using Shiemi.Views;

namespace Shiemi.Utilities.ServiceProviders;

public static class Provider
{
    private static TitleBarWidget? _titleBarWidget { get; set; }
    private static IServiceProvider? _serviceProvider { get; set; }
    
    // titlebar service provider
    public static TitleBarWidget? GetTitleBarWidget()
        => _titleBarWidget;
    public static void SetTitleBarWidget(TitleBarWidget titleBarWidget)
        => _titleBarWidget = titleBarWidget;

    // service provider
    public static void SetProvider(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;
    public static T? GetService<T>()
        => _serviceProvider!.GetService<T>();
}
