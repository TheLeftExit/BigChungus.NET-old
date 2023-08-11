using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Controls;

internal class MessageRouter
{
    private static int LOWORD(nint value) => (ushort)(value & 0xFFFF);
    private static int HIWORD(nint value) => (ushort)((value >> 16) & 0xFFFF);

    public static nint HandleWindowMessage(WindowProcedureArgs args)
    {
        var targetWindow = WindowManager.Current.GetWindow(args.Handle) ?? WindowCreationScopeManager.Current.Top;
        var returnValue = targetWindow.WndProc(args);

        if(args.Message == WM.COMMAND)
        {
            var sourceHandle = args.LParam;
            var sourceWindow = WindowManager.Current.GetWindow(sourceHandle);
            if(sourceWindow is Button button)
            {
                if(HIWORD(args.WParam) == ButtonConstants.BN_CLICKED)
                {
                    button.RaiseClicked();
                }
            }
        }

        switch (args.Message)
        {
            case WM.CREATE:
                WindowManager.Current.RegisterWindow(targetWindow, args.Handle);
                WindowCommon.SetFont(args.Handle, WindowManager.Current.DefaultFont.Handle);
                break;
            case WM.NCDESTROY:
                WindowManager.Current.UnregisterWindow(args.Handle);
                break;
        }

        return returnValue;
    }
}