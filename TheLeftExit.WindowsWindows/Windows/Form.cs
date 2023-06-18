using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Form : Window
{
    private const string className = "Form";
    
    static unsafe Form()
    {
        fixed (char* classNamePtr = className)
        {
            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = WNDCLASS_STYLES.CS_DBLCLKS | WNDCLASS_STYLES.CS_HREDRAW | WNDCLASS_STYLES.CS_VREDRAW,
                hbrBackground = User32.GetSysColorBrush(SYS_COLOR_INDEX.COLOR_BTNFACE),
                lpszClassName = classNamePtr,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate<WNDPROC>(FormWndProc),
                hInstance = InstanceHandleHelper.GetHandle()
            };
            if (User32.RegisterClassEx(wndClassEx) == 0) throw new ApplicationException();
        }
    }

    private static nint FormWndProc(nint hWnd, WM uMsg, nuint wParam, nint lParam)
    {
        return ((Form)(windows.GetValueOrDefault(hWnd) ?? createdWindows.Peek())).WndProc(hWnd, uMsg, wParam, lParam);
    }

    protected virtual nint WndProc(nint hWnd, WM uMsg, nuint wParam, nint lParam)
    {
        return User32.DefWindowProc(hWnd, uMsg, wParam, lParam);
    }

    public Form() : base(className) { }
}