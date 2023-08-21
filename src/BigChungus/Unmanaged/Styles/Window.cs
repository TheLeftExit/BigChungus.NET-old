namespace BigChungus.Unmanaged.WindowStyles;

public static class WS
{
    public const uint OVERLAPPED = 0x00000000;
    public const uint POPUP = 0x80000000;
    public const uint CHILD = 0x40000000;
    public const uint MINIMIZE = 0x20000000;
    public const uint VISIBLE = 0x10000000;
    public const uint DISABLED = 0x08000000;
    public const uint CLIPSIBLINGS = 0x04000000;
    public const uint CLIPCHILDREN = 0x02000000;
    public const uint MAXIMIZE = 0x01000000;
    public const uint CAPTION = BORDER | DLGFRAME;
    public const uint BORDER = 0x00800000;
    public const uint DLGFRAME = 0x00400000;
    public const uint VSCROLL = 0x00200000;
    public const uint HSCROLL = 0x00100000;
    public const uint SYSMENU = 0x00080000;
    public const uint THICKFRAME = 0x00040000;
    public const uint GROUP = 0x00020000;
    public const uint TABSTOP = 0x00010000;

    public const uint MINIMIZEBOX = 0x00020000;
    public const uint MAXIMIZEBOX = 0x00010000;

    public const uint OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX;
    public const uint POPUPWINDOW = POPUP | BORDER | SYSMENU;
}

public static class CCS
{
    public const uint TOP = 0x00000001;
    public const uint NOMOVEY = 0x00000002;
    public const uint BOTTOM = 0x00000003;
    public const uint NORESIZE = 0x00000004;
    public const uint NOPARENTALIGN = 0x00000008;
    public const uint ADJUSTABLE = 0x00000020;
    public const uint NODIVIDER = 0x00000040;
    public const uint VERT = 0x00000080;
    public const uint LEFT = VERT | TOP;
    public const uint RIGHT = VERT | BOTTOM;
    public const uint NOMOVEX = VERT | NOMOVEY;
}
