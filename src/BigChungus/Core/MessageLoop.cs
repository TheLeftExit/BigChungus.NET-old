using BigChungus.Core.Interop;

namespace BigChungus.Core;

public static class MessageLoop {
    public static void Run()
    {
        MSG message;
        while (PInvoke.GetMessage(out message, 0, 0, 0) != 0)
        {
            PInvoke.TranslateMessage(message);
            PInvoke.DispatchMessage(message);
        }
    }

    public static void PostQuit() => PInvoke.PostQuitMessage(0);
}