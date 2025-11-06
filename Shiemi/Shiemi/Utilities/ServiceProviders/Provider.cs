namespace Shiemi.Utilities.ServiceProviders;

public static class Provider
{
    private static IServiceProvider? _serviceProvider { get; set; }

    public static void SetProvider(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public static T? GetService<T>()
        => _serviceProvider!.GetService<T>();
}
