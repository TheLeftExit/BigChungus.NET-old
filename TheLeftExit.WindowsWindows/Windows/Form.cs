using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

    private static nint FormWndProc(nint hWnd, WM uMsg, nuint wParam, nint lParam)
    {
        var form = (Form)(windows.GetValueOrDefault(hWnd) ?? createdWindows.Peek());
        return form.WndProc(hWnd, uMsg, wParam, lParam);
    }

    protected virtual nint WndProc(nint hWnd, WM uMsg, nuint wParam, nint lParam)
    {
        return PInvoke.DefWindowProc(hWnd, uMsg, wParam, lParam);
    }

    protected override nint CreateHandle()
    {
        return CreateWindow(
            WINDOW_EX_STYLE.WS_EX_OVERLAPPEDWINDOW,
            className,
            default,
            WINDOW_STYLE.WS_OVERLAPPEDWINDOW,
            PInvoke.CW_USEDEFAULT,
            PInvoke.CW_USEDEFAULT,
            PInvoke.CW_USEDEFAULT,
            PInvoke.CW_USEDEFAULT,
            default
        );
    }
}