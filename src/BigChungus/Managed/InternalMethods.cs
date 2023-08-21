using BigChungus.Unmanaged;
using BigChungus.Unmanaged.Libraries;

namespace BigChungus.Managed;

public static class InternalMethods
{
    public static unsafe nint SendMessage<T>(nint hWnd, uint msg, nint wParam, ref T lParam) where T : unmanaged
    {
        fixed(T* ptr = &lParam) return User32.SendMessage(hWnd, msg, wParam, (nint)ptr);
    }

    public static unsafe nint SendMessage(nint hWnd, uint msg, nint wParam, ReadOnlySpan<char> lParam)
    {
        fixed (char* text = lParam) return User32.SendMessage(hWnd, msg, wParam, (nint)text);
    }

    public static unsafe nint Create(ReadOnlySpan<char> className, uint style, uint exStyle, nint parentHandle)
    {
        fixed (char* classNamePtr = className)
        {
            var returnValue = User32.CreateWindowEx(
                exStyle,
                classNamePtr,
                default,
                style,
                CW.USEDEFAULT,
                CW.USEDEFAULT,
                CW.USEDEFAULT,
                CW.USEDEFAULT,
                parentHandle,
                default,
                ApplicationMethods.GetApplicationHandle(),
                default
            );
            ReturnValueException.ThrowIf(nameof(User32.CreateWindowEx), returnValue is 0);
            return returnValue;
        }
    }

    public static unsafe void Register(ReadOnlySpan<char> className, WNDPROC windowProcedure)
    {
        fixed (char* classNamePtr = className)
        {
            var wndProcPtr = MarshaledDelegateStorage.Current.Add(windowProcedure);

            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = WNDCLASS_STYLES.CS_HREDRAW | WNDCLASS_STYLES.CS_VREDRAW,
                hbrBackground = User32.GetSysColorBrush(SYS_COLOR_INDEX.COLOR_BTNFACE),
                lpszClassName = classNamePtr,
                lpfnWndProc = wndProcPtr,
                hInstance = ApplicationMethods.GetApplicationHandle()
            };

            var returnValue = User32.RegisterClassEx(wndClassEx);
            ReturnValueException.ThrowIf(nameof(User32.RegisterClassEx), returnValue is 0);
        }
    }
}