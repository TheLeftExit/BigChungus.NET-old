namespace BigChungus.Unmanaged;

public static class BN
{
    public const uint CLICKED = 0;
    public const uint PAINT = 1;
    public const uint HILITE = 2;
    public const uint UNHILITE = 3;
    public const uint DISABLE = 4;
    public const uint DOUBLECLICKED = 5;
    public const uint SETFOCUS = 6;
    public const uint KILLFOCUS = 7;
}

public static class BCN
{
    public const uint FIRST = unchecked(0U - 1250U);
    public const uint LAST = unchecked(0U - 1350U);
    public const uint HOTITEMCHANGE = FIRST + 0x0001;
    public const uint DROPDOWN = FIRST + 0x0002;
}

public static class EN
{
    public const uint SETFOCUS = 0x0100;
    public const uint KILLFOCUS = 0x0200;
    public const uint CHANGE = 0x0300;
    public const uint UPDATE = 0x0400;
    public const uint ERRSPACE = 0x0500;
    public const uint MAXTEXT = 0x0501;
    public const uint HSCROLL = 0x0601;
    public const uint VSCROLL = 0x0602;
    public const uint ALIGN_LTR_EC = 0x0700;
    public const uint ALIGN_RTL_EC = 0x0701;
}