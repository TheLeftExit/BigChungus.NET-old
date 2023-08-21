namespace BigChungus.Unmanaged.Messages;

public static class BM
{
    public const uint GETCHECK = 0x00F0;
    public const uint SETCHECK = 0x00F1;
    public const uint GETSTATE = 0x00F2;
    public const uint SETSTATE = 0x00F3;
    public const uint SETSTYLE = 0x00F4;
    public const uint CLICK = 0x00F5;
    public const uint GETIMAGE = 0x00F6;
    public const uint SETIMAGE = 0x00F7;
}

public static class BCM
{
    public const uint FIRST = 0x1600;
    public const uint GETIDEALSIZE = FIRST + 0x0001;
    public const uint SETIMAGELIST = FIRST + 0x0002;
    public const uint GETIMAGELIST = FIRST + 0x0003;
    public const uint SETTEXTMARGIN = FIRST + 0x0004;
    public const uint GETTEXTMARGIN = FIRST + 0x0005;
    public const uint SETDROPDOWNSTATE = FIRST + 0x0006;
    public const uint SETSPLITINFO = FIRST + 0x0007;
    public const uint GETSPLITINFO = FIRST + 0x0008;
    public const uint SETNOTE = FIRST + 0x0009;
    public const uint GETNOTE = FIRST + 0x000A;
    public const uint GETNOTELENGTH = FIRST + 0x000B;
    public const uint SETSHIELD = FIRST + 0x000C;
}
