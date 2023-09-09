using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public static class Internal
{
    public const uint BaseStyle = WS.CHILD | WS.VISIBLE;

    public static unsafe nint SendMessage_Ref<T>(this nint hWnd, uint msg, nint wParam, ref T lParam) where T : unmanaged
    {
        fixed(T* ptr = &lParam) return User32.SendMessage(hWnd, msg, wParam, (nint)ptr);
    }

    public static unsafe nint SendMessage_Out<T>(this nint hWnd, uint msg, nint wParam, out T lParam) where T : unmanaged
    {
        fixed (T* ptr = &lParam) return User32.SendMessage(hWnd, msg, wParam, (nint)ptr);
    }

    public static unsafe nint SendMessage_Out<T1, T2>(this nint hWnd, uint msg, out T1 wParam, out T2 lParam) where T1 : unmanaged where T2 : unmanaged
    {
        fixed (T1* wParamPtr = &wParam) fixed (T2* lParamPtr = &lParam) return User32.SendMessage(hWnd, msg, (nint)wParamPtr, (nint)lParamPtr);
    }

    public static unsafe nint SendMessage_In<T>(this nint hWnd, uint msg, nint wParam, in T lParam) where T : unmanaged
    {
        fixed (T* ptr = &lParam) return User32.SendMessage(hWnd, msg, wParam, (nint)ptr);
    }

    public static unsafe nint SendMessage_SpanChar(this nint hWnd, uint msg, nint wParam, ReadOnlySpan<char> lParam)
    {
        fixed (char* text = lParam) return User32.SendMessage(hWnd, msg, wParam, (nint)text);
    }

    public static unsafe nint SendMessage_SpanChar(this nint hWnd, uint msg, ReadOnlySpan<char> wParam, nint lParam)
    {
        fixed (char* text = wParam) return User32.SendMessage(hWnd, msg, (nint)text, lParam);
    }

    public static nint SendMessage(this nint hWnd, uint msg, nint wParam, nint lParam) => User32.SendMessage(hWnd, msg, wParam, lParam);

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
            ).ThrowIf(0);
            return returnValue;
        }
    }

    public static nint CreateEx(ReadOnlySpan<char> className, nint parentHandle, uint style = default, uint exStyle = default)
    {
        return Create(className, BaseStyle | style, exStyle, parentHandle);
    }
    public static unsafe void Register(ReadOnlySpan<char> className, WNDPROC windowProcedure)
    {
        fixed (char* classNamePtr = className)
        {
            var wndProcPtr = MarshaledDelegateStorage.Current.Add(windowProcedure);

            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = CS.HREDRAW | CS.VREDRAW,
                hbrBackground = User32.GetSysColorBrush(COLOR.BTNFACE),
                lpszClassName = classNamePtr,
                lpfnWndProc = wndProcPtr,
                hInstance = ApplicationMethods.GetApplicationHandle()
            };

            User32.RegisterClassEx(wndClassEx).ThrowIf((ushort)0);
        }
    }
}