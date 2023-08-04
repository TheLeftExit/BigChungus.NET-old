using BigChungus.Interop;
using System.Runtime.InteropServices;

namespace BigChungus.Utils;

public static class WindowProcedure
{
    public static nint Default(WindowProcedureArgs args)
    {
        return PInvoke.DefWindowProc(args.Handle, args.Message, args.WParam, args.LParam);
    }

    public static nint Call(nint handle, WindowProcedureArgs args)
    {
        return PInvoke.CallWindowProc(handle, args.Handle, args.Message, args.WParam, args.LParam);
    }

    public static nint Get(nint handle)
    {
        return PInvoke.GetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWLP_WNDPROC);
    }

    public static void Set(nint handle, nint functionPtr)
    {
        PInvoke.SetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWLP_WNDPROC, functionPtr);
    }

    public static IDisposable Subclass(nint handle, Func<WindowProcedureArgs, WindowProcedureFunction, nint> callback)
    {
        nint baseWndProcPtr = Get(handle);
        WNDPROC newWndProc = (nint handle, WM message, nint wParam, nint lParam) => callback(new(handle, message, wParam, lParam), newArgs => Call(baseWndProcPtr, newArgs));
        nint newWndProcPtr = MarshaledDelegateStorage.Current.Add(newWndProc);
        Set(handle, newWndProcPtr);
        return new SubclassContext(handle, baseWndProcPtr, newWndProcPtr);
    }
}

public delegate nint WindowProcedureFunction(WindowProcedureArgs args);

public struct WindowProcedureArgs
{
    public nint Handle { get; set; }
    public WM Message { get; set; }
    public nint WParam { get; set; }
    public nint LParam { get; set; }
    public WindowProcedureArgs(nint handle, WM message, nint wParam, nint lParam)
    {
        Handle = handle;
        Message = message;
        WParam = wParam;
        LParam = lParam;
    }
}

internal class SubclassContext(nint handle, nint baseWndProcPtr, nint newWndProcPtr) : IDisposable
{
    void IDisposable.Dispose()
    {
        WindowProcedure.Set(handle, baseWndProcPtr);
        MarshaledDelegateStorage.Current.Remove(newWndProcPtr);
    }
}