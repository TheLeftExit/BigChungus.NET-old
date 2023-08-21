namespace BigChungus.Unmanaged.Notifications;

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