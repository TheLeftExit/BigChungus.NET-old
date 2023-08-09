using BigChungus.Core;
using BigChungus.Drawing;
using BigChungus.Windows;

namespace BigChungus.Common;

public static class Application
{
    static Application()
    {
        ApplicationCommon.EnableVisualStyles();
        ApplicationCommon.LoadCommonControls();
    }

    public static void Run()
    {
        MessageLoop.Run();
    }

    public static void Quit()
    {
        MessageLoop.PostQuit();
    }

    public static Font DefaultFont {
        get => WindowManager.Current.DefaultFont;
        set => WindowManager.Current.DefaultFont = value;
    }
}