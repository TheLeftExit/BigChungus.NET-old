using BigChungus.Interop;
using BigChungus.Utils;
using System.Runtime.InteropServices;

namespace BigChungus.Windows;

public class Form : Window
{
    private const string className = "Form";

    private static unsafe void RegisterFormClass()
    {
        fixed (char* classNamePtr = className)
        {
            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = WNDCLASS_STYLES.CS_DBLCLKS | WNDCLASS_STYLES.CS_HREDRAW | WNDCLASS_STYLES.CS_VREDRAW,
                hbrBackground = PInvoke.GetSysColorBrush(SYS_COLOR_INDEX.COLOR_BTNFACE),
                lpszClassName = classNamePtr,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate<WNDPROC>(FormWndProc),
                hInstance = Application.Handle
            };
            PInvoke.RegisterClassEx(wndClassEx);
        }
    }
    
    static unsafe Form()
    {
        RegisterFormClass();
    }

    private static nint FormWndProc(nint hWnd, WM msg, nint wParam, nint lParam)
    {
        var form = (Form)(windows.GetValueOrDefault(hWnd) ?? createdWindows.Peek());
        return form.WndProc(new(hWnd, msg, wParam, lParam));
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