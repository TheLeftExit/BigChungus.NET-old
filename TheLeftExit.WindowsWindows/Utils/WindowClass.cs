using System.Runtime.InteropServices;

public class WindowClass {
    public unsafe Class Register(ReadOnlySpan<char> className, WindowProcedureFunction windowProcedure, WNDCLASS_STYLES style = default, bool enableBackgroundBrush = true)
    {
        fixed (char* classNamePtr = className)
        {
            WNDPROC wndProc = (hWnd, msg, wParam, lParam) => windowProcedure(new(hWnd, msg, wParam, lParam));

            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = style,
                hbrBackground = enableBackgroundBrush ? PInvoke.GetSysColorBrush(SYS_COLOR_INDEX.COLOR_BTNFACE) : default,
                lpszClassName = classNamePtr,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(wndProc),
                hInstance = Application.Handle
            };
            PInvoke.RegisterClassEx(wndClassEx);

            return new(wndProc);
        }
    }
}

public record struct Class(object ProcedureReference);