using BigChungus.Interop;
using System.Runtime.InteropServices;

namespace BigChungus.Utils;

public static class WindowClass {
    public static unsafe IDisposable Register(ReadOnlySpan<char> className, WindowProcedureFunction windowProcedure, WNDCLASS_STYLES style = WNDCLASS_STYLES.CS_DBLCLKS | WNDCLASS_STYLES.CS_HREDRAW | WNDCLASS_STYLES.CS_VREDRAW, bool enableBackgroundBrush = true)
    {
        fixed (char* classNamePtr = className)
        {
            WNDPROC wndProc = (hWnd, msg, wParam, lParam) => windowProcedure(new(hWnd, msg, wParam, lParam));
            var wndProcPtr = MarshaledDelegateStorage.Current.Add(wndProc);

            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = style,
                hbrBackground = enableBackgroundBrush ? PInvoke.GetSysColorBrush(SYS_COLOR_INDEX.COLOR_BTNFACE) : default,
                lpszClassName = classNamePtr,
                lpfnWndProc = wndProcPtr,
                hInstance = Application.Handle
            };

            var atom = PInvoke.RegisterClassEx(wndClassEx);

            return new ClassContext(atom, wndProcPtr);
        }
    }
}

internal class ClassContext(ushort atom, nint wndProcPtr) : IDisposable {
    unsafe void IDisposable.Dispose()
    {
        var result = PInvoke.UnregisterClass((char*)atom, Application.Handle);
        MarshaledDelegateStorage.Current.Remove(wndProcPtr);
    }
}