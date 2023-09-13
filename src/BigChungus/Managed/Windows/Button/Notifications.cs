using BigChungus.Unmanaged;
using System.Diagnostics;

namespace BigChungus.Managed;

public partial record struct Button
{
    public static bool TryDropDown(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == BCN.DROPDOWN;
    }

    public static ref NMBCDROPDOWN DecodeDropDown(Message message)
    {
        return ref message.DereferenceLParam<NMBCDROPDOWN>();
    }

    public static bool TryHotItemChange(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == BCN.HOTITEMCHANGE;
    }

    public static ref NMBCHOTITEM DecodeHotItemChange(Message message)
    {
        return ref message.DereferenceLParam<NMBCHOTITEM>();
    }

    public static bool TryClicked(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == BN.CLICKED;
    }

    public static bool TryDoubleClicked(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == BN.DOUBLECLICKED;
    }

    public static bool TryKillFocus(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == BN.KILLFOCUS;
    }

    public static bool TrySetFocus(Message message, out NMHDR header)
    {
        return message.TryParseNotification(out header) && header.Code == BN.SETFOCUS;
    }
}