using BigChungus.Common;
using BigChungus.Core.Interop;
namespace BigChungus.Core;

public static class MessageLoop {
    public static void Run(TranslateMessageFilter translateFilter = null, DispatchMessageFilter dispatchFilter = null)
    {
        MSG message = default;
        WindowMessage wrapper = new(ref message);
        while (PInvoke.GetMessage(out message, 0, 0, 0) != 0)
        {
            if (translateFilter?.Invoke(ref wrapper) ?? true)
            {
                PInvoke.TranslateMessage(message);
            }
            if (dispatchFilter?.Invoke(ref wrapper) ?? true)
            {
                PInvoke.DispatchMessage(message);
            }
        }
    }

    public static void PostQuit() => PInvoke.PostQuitMessage(0);
}

public delegate bool TranslateMessageFilter(ref WindowMessage msg);
public delegate bool DispatchMessageFilter(ref WindowMessage msg);

public ref struct WindowMessage {
    private ref MSG msg;
    public WindowMessage(ref MSG message)
    {
        msg = ref message;
    }
    public nint Handle { get => msg.hwnd; set => msg.hwnd = value; }
    public WM Message { get => msg.message; set => msg.message = value; }
    public nint WParam { get => msg.wParam; set => msg.wParam = value; }
    public nint LParam { get => msg.lParam; set => msg.lParam = value; }
}