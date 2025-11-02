namespace Shiemi.Storage;

public static class DataStorage
{
    public static void Store(string key, string value)
        => Preferences.Default.Set(key, value);

    public static string Get(string key)
        => Preferences.Default.Get<string>(key, "");

    public static void Remove(string key)
        => Preferences.Default.Remove(key);
}
