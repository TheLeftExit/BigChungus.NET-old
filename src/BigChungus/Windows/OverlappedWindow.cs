using BigChungus.Core;
using BigChungus.Common;

namespace BigChungus.Windows;

public class OverlappedWindow : Window
{
    private const string className = "Form";
    
    static unsafe OverlappedWindow()
    {
        WindowClass.Register(className, FormWndProc);
    }

    private static nint FormWndProc(WindowProcedureArgs args)
    {
        var form = (OverlappedWindow)WindowManager.Current.GetWindow(args.Handle);
        form ??= OverlappedWindowManager.Current.Top;
        return form.OnWindowMessage(args);
    }

    protected virtual nint OnWindowMessage(WindowProcedureArgs args)
    {
        return Core.WindowProcedure.Default(args);
    }

    protected override nint CreateHandle()
    {
        using (var scope = OverlappedWindowManager.Current.CreateScope(this))
        {
            return WindowCommon.Create(className, WINDOW_EX_STYLE.WS_EX_OVERLAPPEDWINDOW, WINDOW_STYLE.WS_OVERLAPPEDWINDOW);
        }
    }
}