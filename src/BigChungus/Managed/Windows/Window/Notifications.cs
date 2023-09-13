using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public readonly partial record struct Window
{
    public static bool TryClose(Message message)
    {
        return message.Code == WM.CLOSE;
    }

    public static bool TryDestroy(Message message)
    {
        return message.Code == WM.DESTROY;
    }
}