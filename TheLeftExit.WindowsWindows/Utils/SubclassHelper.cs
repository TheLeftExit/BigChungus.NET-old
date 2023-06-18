using System.Runtime.InteropServices;

public static class SubclassHelper
{
    public static IDisposable Subclass(Window window, CustomWndProc wndProc) => new SubclassHandle(window.Handle, wndProc);
}

public class SubclassHandle : IDisposable
{
    [ThreadStatic]
    private static HashSet<SubclassHandle> handles = new();
    
    private nint handle;
    private CustomWndProc customWndProc;
    private nint defaultWndProcPtr;

    public SubclassHandle(nint handle, CustomWndProc wndProc)
    {
        this.handle = handle;
        defaultWndProcPtr = User32.GetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWLP_WNDPROC);
        customWndProc = wndProc;
        var subclassedWndProcPtr = Marshal.GetFunctionPointerForDelegate<WNDPROC>(WndProc);
        User32.SetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWLP_WNDPROC, subclassedWndProcPtr);
        handles.Add(this);
    }

    private nint WndProc(nint hWnd, WM uMsg, nuint wParam, nint lParam)
    {
        var args = new WndProcArgs(CallWindowProc)
        {
            Handle = hWnd,
            Message = uMsg,
            WParam = wParam,
            LParam = lParam
        };
        
        var result = customWndProc(args);
        if (uMsg == WM.NCDESTROY) Dispose();
        return result;
    }

    private nint CallWindowProc(nint hWnd, WM uMsg, nuint wParam, nint lParam) => User32.CallWindowProc(defaultWndProcPtr, hWnd, uMsg, wParam, lParam);

    public void Dispose()
    {
        User32.SetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWLP_WNDPROC, defaultWndProcPtr);
        handles.Remove(this);
    }
}

public struct WndProcArgs(WNDPROC defaultWndProc)
{
    public nint Handle { get; init; }
    public WM Message { get; init; }
    public nuint WParam { get; init; }
    public nint LParam { get; init; }
    public nint DefaultWndProc(nint hWnd, WM uMsg, nuint wParam, nint lParam) => defaultWndProc(hWnd, uMsg, wParam, lParam);
}

public delegate nint CustomWndProc(WndProcArgs args);