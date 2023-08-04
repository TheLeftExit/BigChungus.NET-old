using BigChungus.Managed;
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
        var form = (OverlappedWindow)(WindowManager.Current.GetWindow(args.Handle) ?? WindowManager.Current.GetCreatedWindow());
        return form.WndProc(args);
    }

    protected virtual nint WndProc(WindowProcedureArgs args)
    {
        return WindowProcedure.Default(args);
    }

    protected override nint CreateHandle()
    {
        return WindowCommon.Create(className, WINDOW_EX_STYLE.WS_EX_OVERLAPPEDWINDOW, WINDOW_STYLE.WS_OVERLAPPEDWINDOW);
    }
}