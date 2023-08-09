using BigChungus.Common;
using BigChungus.Core.Interop;

namespace BigChungus.Core;

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

    public static IDisposable Subclass(nint handle, SubclassCallback callback)
    {
        nint baseWndProcPtr = Get(handle);
        WNDPROC newWndProc = (nint handle, WM message, nint wParam, nint lParam) => callback(new(handle, message, wParam, lParam), newArgs => Call(baseWndProcPtr, newArgs));
        nint newWndProcPtr = MarshaledDelegateStorage.Current.Add(newWndProc);
        Set(handle, newWndProcPtr);
        return new SubclassContext(handle, baseWndProcPtr, newWndProcPtr);
    }
}

internal class SubclassContext(nint handle, nint baseWndProcPtr, nint newWndProcPtr) : IDisposable
{
    public void Dispose()
    {
        WindowProcedure.Set(handle, baseWndProcPtr);
        MarshaledDelegateStorage.Current.Remove(newWndProcPtr);
    }
}