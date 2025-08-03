namespace Shiemi.Services.Storage
{
    public static class SettingsStorage
    {
        public static void StoreDeveloperMode(bool value)
        {
            Preferences.Default.Set("DeveloperMode", value);
        }
        public static bool FetchDeveloperMode()
        {
            return Preferences.Default.Get("DeveloperMode", false);
        }
    }
}
