using BigChungus.Common;
using BigChungus.Core.Interop;

namespace BigChungus.Core;

public static class WindowClass {
    public static unsafe IDisposable Register(ReadOnlySpan<char> className, WindowCallback windowProcedure, WNDCLASS_STYLES style = WNDCLASS_STYLES.CS_DBLCLKS | WNDCLASS_STYLES.CS_HREDRAW | WNDCLASS_STYLES.CS_VREDRAW, bool enableBackgroundBrush = true)
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

            var returnValue = PInvoke.RegisterClassEx(wndClassEx);
            ReturnValueException.ThrowIf(nameof(PInvoke.RegisterClassEx), returnValue == 0);

            return new ClassContext(returnValue, wndProcPtr);
        }
    }
}

internal class ClassContext(ushort atom, nint wndProcPtr) : IDisposable {
    unsafe void IDisposable.Dispose()
    {
        var returnValue = PInvoke.UnregisterClass((char*)atom, Application.Handle);
        ReturnValueException.ThrowIf(nameof(PInvoke.UnregisterClass), returnValue is false);

        MarshaledDelegateStorage.Current.Remove(wndProcPtr);
    }
}