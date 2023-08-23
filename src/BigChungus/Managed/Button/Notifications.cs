using BigChungus.Unmanaged.Notifications;

namespace BigChungus.Managed;

public static class ButtonNotifications
{
    public static bool TryClicked(nint hwnd, uint msg, nint wParam, nint lParam, out CommandArgs args)
    {
        if(WindowNotifications.TryDecode(hwnd, msg, wParam, lParam, out var wmArgs) && wmArgs.NotificationCode == BN.CLICKED)
        {
            args = new(wmArgs.Handle);
            return true;
        }
        args = default;
        return false;
    }
}