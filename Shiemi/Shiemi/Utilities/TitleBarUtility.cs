namespace Shiemi.Utilities;

public static class TitleBarUtility
{
    private static Window? _window;

    public static void InitTitleBar(Window window)
    {
        var titleBar = new TitleBar
        {
            Title = "Shiemi"
        };

        _window = window;
    }

    public static void ChangeTitle(string newTitle)
    {
        _window!.TitleBar = new TitleBar
        {
            Title = newTitle
        };
    }
}