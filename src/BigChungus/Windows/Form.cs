using BigChungus.Interop;
using BigChungus.Utils;
using System.Runtime.InteropServices;

namespace BigChungus.Windows;

public class Form : Window
{
    private const string className = "Form";

    private static unsafe void RegisterFormClass()
    {
        WindowClass.Register("Form", FormWndProc);
    }
    
    static unsafe Form()
    {
        RegisterFormClass();
    }

    private static nint FormWndProc(WindowProcedureArgs args)
    {
        var form = (Form)(windows.GetValueOrDefault(args.Handle) ?? createdWindows.Peek());
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