using BigChungus.Common;

namespace BigChungus.Windows;

internal class MessageRouter
{
    public static nint HandleWindowMessage(WindowProcedureArgs args)
    {
        var targetWindow = WindowManager.Current.GetWindow(args.Handle) ?? WindowCreationScopeManager.Current.Top;
        var returnValue = targetWindow.WndProc(args);

        if(args.Message == WM.NCDESTROY)
        {
            WindowManager.Current.UnregisterWindow(targetWindow.Handle);
        }

        return returnValue;
    }
}