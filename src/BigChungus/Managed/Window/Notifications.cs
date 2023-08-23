using BigChungus.Unmanaged;
using BigChungus.Unmanaged.Messages;
using System.Runtime.CompilerServices;

namespace BigChungus.Managed;

public readonly record struct WindowNotificationArgs(nint Handle, uint NotificationCode);

public readonly struct CommandArgs
{
    public readonly nint Handle;
    public CommandArgs(nint handle)
    {
        Handle = handle;
    }
}

public static class WindowNotifications
{
    public static ushort LOWORD(this nint value) => (ushort)(value & 0xFFFF);
    public static ushort HIWORD(this nint value) => (ushort)(value >> 16 & 0xFFFF);

    public static unsafe bool TryDecode(nint hwnd, uint msg, nint wParam, nint lParam, out WindowNotificationArgs args)
    {
        if(msg == WM.COMMAND)
        {
            args = new(lParam, wParam.HIWORD());
            return true;
        }
        if(msg == WM.NOTIFY)
        {
            var infoBasePtr = (NMHDR*)lParam;
            args = new(infoBasePtr->hwndFrom, infoBasePtr->code);
        }
        args = default;
        return false;
    }

    public static unsafe ref T GetExtraInfo<T>(nint lParam) where T : unmanaged
    {
        return ref Unsafe.AsRef<T>((void*)lParam);
    }
}