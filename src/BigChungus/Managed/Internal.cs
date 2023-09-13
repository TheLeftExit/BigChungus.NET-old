using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public static class Internal
{
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

    public static nint SendMessage(this nint hWnd, uint msg, nint wParam, nint lParam)
    {
        return User32.SendMessage(hWnd, msg, wParam, lParam);
    }

    public static string ToNullTerminatedString(this Span<char> span)
    {
        var nullTerminatorIndex = span.IndexOf('\0');
        var slicedSpan = span.Slice(0, nullTerminatorIndex);
        return new(slicedSpan);
    }

    public static unsafe nint Create(nint atom, uint style, uint exStyle, nint parentHandle)
    {
        var returnValue = User32.CreateWindowEx(
            exStyle,
            (char*)atom,
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

    public static unsafe nint Create(ReadOnlySpan<char> className, uint style, uint exStyle, nint parentHandle)
    {
        fixed (char* classNamePtr = className)
        {
            return Create((nint)classNamePtr, style, exStyle, parentHandle);
        }
    }

    public static unsafe ushort Register(ReadOnlySpan<char> className, WNDPROC windowProcedure)
    {
        fixed (char* classNamePtr = className)
        {
            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = CS.HREDRAW | CS.VREDRAW,
                hbrBackground = User32.GetSysColorBrush(COLOR.BTNFACE),
                lpszClassName = classNamePtr,
                lpfnWndProc = MarshaledDelegateStorage.Add(windowProcedure),
                hInstance = ApplicationMethods.GetApplicationHandle()
            };

            return User32.RegisterClassEx(wndClassEx).ThrowIf<ushort>(0);
        }
    }

    public static unsafe void Unregister(ReadOnlySpan<char> className)
    {
        fixed (char* classNamePtr = className)
        {
            User32.GetClassInfoEx(ApplicationMethods.GetApplicationHandle(), classNamePtr, out var info);
            User32.UnregisterClass(classNamePtr, ApplicationMethods.GetApplicationHandle()).ThrowIfFalse();
            MarshaledDelegateStorage.Remove(info.lpfnWndProc);
        }
    }

    public static unsafe void Unregister(ushort atom)
    {
        User32.GetClassInfoEx(ApplicationMethods.GetApplicationHandle(), (char*)atom, out var info);
        User32.UnregisterClass((char*)atom, ApplicationMethods.GetApplicationHandle()).ThrowIfFalse();
        MarshaledDelegateStorage.Remove(info.lpfnWndProc);
    }
}