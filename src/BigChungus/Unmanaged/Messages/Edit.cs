namespace BigChungus.Unmanaged;

public static class EM
{
    public const uint GETSEL = 0x00B0;
    public const uint SETSEL = 0x00B1;
    public const uint GETRECT = 0x00B2;
    public const uint SETRECT = 0x00B3;
    public const uint SETRECTNP = 0x00B4;
    public const uint SCROLL = 0x00B5;
    public const uint LINESCROLL = 0x00B6;
    public const uint SCROLLCARET = 0x00B7;
    public const uint GETMODIFY = 0x00B8;
    public const uint SETMODIFY = 0x00B9;
    public const uint GETLINECOUNT = 0x00BA;
    public const uint LINEINDEX = 0x00BB;
    public const uint SETHANDLE = 0x00BC;
    public const uint GETHANDLE = 0x00BD;
    public const uint GETTHUMB = 0x00BE;
    public const uint LINELENGTH = 0x00C1;
    public const uint REPLACESEL = 0x00C2;
    public const uint GETLINE = 0x00C4;
    public const uint LIMITTEXT = 0x00C5;
    public const uint CANUNDO = 0x00C6;
    public const uint UNDO = 0x00C7;
    public const uint FMTLINES = 0x00C8;
    public const uint LINEFROMCHAR = 0x00C9;
    public const uint SETTABSTOPS = 0x00CB;
    public const uint SETPASSWORDCHAR = 0x00CC;
    public const uint EMPTYUNDOBUFFER = 0x00CD;
    public const uint GETFIRSTVISIBLELINE = 0x00CE;
    public const uint SETREADONLY = 0x00CF;
    public const uint SETWORDBREAKPROC = 0x00D0;
    public const uint GETWORDBREAKPROC = 0x00D1;
    public const uint GETPASSWORDCHAR = 0x00D2;
    public const uint SETMARGINS = 0x00D3;
    public const uint GETMARGINS = 0x00D4;
    public const uint SETLIMITTEXT = LIMITTEXT;
    public const uint GETLIMITTEXT = 0x00D5;
    public const uint POSFROMCHAR = 0x00D6;
    public const uint CHARFROMPOS = 0x00D7;
    public const uint SETIMESTATUS = 0x00D8;
    public const uint GETIMESTATUS = 0x00D9;

    public const uint SETCUEBANNER = ECM.FIRST + 1;
    public const uint GETCUEBANNER = ECM.FIRST + 2;
    public const uint SHOWBALLOONTIP = ECM.FIRST + 3;
    public const uint HIDEBALLOONTIP = ECM.FIRST + 4;
}

public static class ECM
{
    public const uint FIRST = 0x1500;
}