using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public partial record struct Edit
{
    public static bool TryAlignLtrEc(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.ALIGN_LTR_EC;
    }

    public static bool TryAlignRtlEc(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.ALIGN_RTL_EC;
    }

    public static bool TryChange(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.CHANGE;
    }

    public static bool TryErrSpace(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.ERRSPACE;
    }

    public static bool TryHScroll(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.HSCROLL;
    }

    public static bool TryKillFocus(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.KILLFOCUS;
    }

    public static bool TryMaxText(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.MAXTEXT;
    }

    public static bool TrySetFocus(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.SETFOCUS;
    }

    public static bool TryUpdate(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.UPDATE;
    }

    public static bool TryVScroll(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == EN.VSCROLL;
    }
}