using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

public delegate nint WNDPROC(nint hWnd, WM msg, nuint wParam, nint lParam);

public static unsafe partial class PInvoke
{
    [LibraryImport("user32.dll", EntryPoint = "DefWindowProcW")]
    public static partial nint DefWindowProc(nint hWnd, WM uMsg, nuint wParam, nint lParam);

    [LibraryImport("user32.dll", EntryPoint = "GetMessageW")]
    public static partial int GetMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool TranslateMessage(in MSG lpMsg);

    [LibraryImport("user32.dll", EntryPoint = "DispatchMessageW")]
    public static partial nint DispatchMessage(in MSG lpMsg);

    [LibraryImport("user32.dll")]
    public static partial void PostQuitMessage(int nExitCode);

    [LibraryImport("user32.dll")]
    public static partial nint GetSysColorBrush(SYS_COLOR_INDEX nIndex);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ShowWindow(nint hWnd, SHOW_WINDOW_CMD nCmdShow);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UpdateWindow(nint hWnd);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DestroyWindow(nint hWnd);

    [LibraryImport("user32.dll", EntryPoint = "RegisterClassExW")]
    public static partial ushort RegisterClassEx(in WNDCLASSEXW wndClassEx);

    [LibraryImport("user32.dll", EntryPoint = "CreateWindowExW")]
    public static partial nint CreateWindowEx(WINDOW_EX_STYLE dwExStyle, char* lpClassName, char* lpWindowName, WINDOW_STYLE dwStyle, int X, int Y, int nWidth, int nHeight, nint hWndParent, nint hMenu, nint hInstance, void* lpParam);

    public const int CW_USEDEFAULT = unchecked((int)0x80000000);

    [LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    public static partial nint GetWindowLongPtr(nint hWnd, WINDOW_LONG_PTR_INDEX nIndex);
    
    [LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    public static partial nint SetWindowLongPtr(nint hWnd, WINDOW_LONG_PTR_INDEX nIndex, long dwNewLong);

    [LibraryImport("user32.dll", EntryPoint = "SetWindowTextW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetWindowText(nint hWnd, char* lpString);

    [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW")]
    public static partial int GetWindowText(nint hWnd, char* lpString, int nMaxCount);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetWindowRect(nint hWnd, out RECT lpRect);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

    [LibraryImport("user.dll", EntryPoint = "GetWindowTextLengthW")]
    public static partial int GetWindowTextLength(nint hWnd);

    [LibraryImport("user32.dll", EntryPoint = "CallWindowProcW")]
    public static partial nint CallWindowProc(nint lpPrevWndFunc, nint hWnd, WM Msg, nuint wParam, nint lParam);

    [LibraryImport("user32.dll")]
    public static partial nint SetParent(nint hWndChild, nint hWndNewParent);

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleExW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool GetModuleHandleEx(uint dwFlags, nuint lpModuleName, out nint phModule);

    [LibraryImport("comctl32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool InitCommonControlsEx(in INITCOMMONCONTROLSEX picce);

    [LibraryImport("user32.dll", EntryPoint = "SendMessageW")]
    public static unsafe partial nint SendMessage(nint hWnd, WM Msg, nuint wParam, nint lParam);

    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW")]
    public static unsafe partial nint CreateFont(int cHeight, int cWidth, int cEscapement, int cOrientation, int cWeight, uint bItalic, uint bUnderline, uint bStrikeOut, uint iCharSet, uint iOutPrecision, uint iClipPrecision, uint iQuality, uint iPitchAndFamily, char* pszFaceName);

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool DeleteObject(nint hObject);

    [LibraryImport("kernel32.dll", EntryPoint = "CreateActCtxW")]
    public static unsafe partial nint CreateActCtx(in ACTCTXW pActCtx);

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool ActivateActCtx(nint hActCtx, out nuint lpCookie);
}

[Flags]
public enum WINDOW_EX_STYLE : uint
{
    WS_EX_DLGMODALFRAME = 0x00000001,
    WS_EX_NOPARENTNOTIFY = 0x00000004,
    WS_EX_TOPMOST = 0x00000008,
    WS_EX_ACCEPTFILES = 0x00000010,
    WS_EX_TRANSPARENT = 0x00000020,
    WS_EX_MDICHILD = 0x00000040,
    WS_EX_TOOLWINDOW = 0x00000080,
    WS_EX_WINDOWEDGE = 0x00000100,
    WS_EX_CLIENTEDGE = 0x00000200,
    WS_EX_CONTEXTHELP = 0x00000400,
    WS_EX_RIGHT = 0x00001000,
    WS_EX_LEFT = 0x00000000,
    WS_EX_RTLREADING = 0x00002000,
    WS_EX_LTRREADING = 0x00000000,
    WS_EX_LEFTSCROLLBAR = 0x00004000,
    WS_EX_RIGHTSCROLLBAR = 0x00000000,
    WS_EX_CONTROLPARENT = 0x00010000,
    WS_EX_STATICEDGE = 0x00020000,
    WS_EX_APPWINDOW = 0x00040000,
    WS_EX_OVERLAPPEDWINDOW = 0x00000300,
    WS_EX_PALETTEWINDOW = 0x00000188,
    WS_EX_LAYERED = 0x00080000,
    WS_EX_NOINHERITLAYOUT = 0x00100000,
    WS_EX_NOREDIRECTIONBITMAP = 0x00200000,
    WS_EX_LAYOUTRTL = 0x00400000,
    WS_EX_COMPOSITED = 0x02000000,
    WS_EX_NOACTIVATE = 0x08000000,
}

[Flags]
public enum WINDOW_STYLE : uint
{
    WS_OVERLAPPED = 0x00000000,
    WS_POPUP = 0x80000000,
    WS_CHILD = 0x40000000,
    WS_MINIMIZE = 0x20000000,
    WS_VISIBLE = 0x10000000,
    WS_DISABLED = 0x08000000,
    WS_CLIPSIBLINGS = 0x04000000,
    WS_CLIPCHILDREN = 0x02000000,
    WS_MAXIMIZE = 0x01000000,
    WS_CAPTION = 0x00C00000,
    WS_BORDER = 0x00800000,
    WS_DLGFRAME = 0x00400000,
    WS_VSCROLL = 0x00200000,
    WS_HSCROLL = 0x00100000,
    WS_SYSMENU = 0x00080000,
    WS_THICKFRAME = 0x00040000,
    WS_GROUP = 0x00020000,
    WS_TABSTOP = 0x00010000,
    WS_MINIMIZEBOX = 0x00020000,
    WS_MAXIMIZEBOX = 0x00010000,
    WS_TILED = 0x00000000,
    WS_ICONIC = 0x20000000,
    WS_SIZEBOX = 0x00040000,
    WS_TILEDWINDOW = 0x00CF0000,
    WS_OVERLAPPEDWINDOW = 0x00CF0000,
    WS_POPUPWINDOW = 0x80880000,
    WS_CHILDWINDOW = 0x40000000,
    WS_ACTIVECAPTION = 0x00000001,
}

[Flags]
public enum WNDCLASS_STYLES : uint
{
    CS_VREDRAW = 0x00000001,
    CS_HREDRAW = 0x00000002,
    CS_DBLCLKS = 0x00000008,
    CS_OWNDC = 0x00000020,
    CS_CLASSDC = 0x00000040,
    CS_PARENTDC = 0x00000080,
    CS_NOCLOSE = 0x00000200,
    CS_SAVEBITS = 0x00000800,
    CS_BYTEALIGNCLIENT = 0x00001000,
    CS_BYTEALIGNWINDOW = 0x00002000,
    CS_GLOBALCLASS = 0x00004000,
    CS_IME = 0x00010000,
    CS_DROPSHADOW = 0x00020000,
}

public unsafe struct WNDCLASSEXW
{
    public uint cbSize;
    public WNDCLASS_STYLES style;
    public nint lpfnWndProc;
    public int cbClsExtra;
    public int cbWndExtra;
    public nint hInstance;
    public nint hIcon;
    public nint hCursor;
    public nint hbrBackground;
    public char* lpszMenuName;
    public char* lpszClassName;
    public nint hIconSm;
}

public enum SHOW_WINDOW_CMD : uint
{
    SW_HIDE = 0U,
    SW_SHOWNORMAL = 1U,
    SW_NORMAL = 1U,
    SW_SHOWMINIMIZED = 2U,
    SW_SHOWMAXIMIZED = 3U,
    SW_MAXIMIZE = 3U,
    SW_SHOWNOACTIVATE = 4U,
    SW_SHOW = 5U,
    SW_MINIMIZE = 6U,
    SW_SHOWMINNOACTIVE = 7U,
    SW_SHOWNA = 8U,
    SW_RESTORE = 9U,
    SW_SHOWDEFAULT = 10U,
    SW_FORCEMINIMIZE = 11U,
    SW_MAX = 11U,
}

public enum SYS_COLOR_INDEX
{
    COLOR_SCROLLBAR = 0,
    COLOR_BACKGROUND = 1,
    COLOR_ACTIVECAPTION = 2,
    COLOR_INACTIVECAPTION = 3,
    COLOR_MENU = 4,
    COLOR_WINDOW = 5,
    COLOR_WINDOWFRAME = 6,
    COLOR_MENUTEXT = 7,
    COLOR_WINDOWTEXT = 8,
    COLOR_CAPTIONTEXT = 9,
    COLOR_ACTIVEBORDER = 10,
    COLOR_INACTIVEBORDER = 11,
    COLOR_APPWORKSPACE = 12,
    COLOR_HIGHLIGHT = 13,
    COLOR_HIGHLIGHTTEXT = 14,
    COLOR_BTNFACE = 15,
    COLOR_BTNSHADOW = 16,
    COLOR_GRAYTEXT = 17,
    COLOR_BTNTEXT = 18,
    COLOR_INACTIVECAPTIONTEXT = 19,
    COLOR_BTNHIGHLIGHT = 20,
    COLOR_3DDKSHADOW = 21,
    COLOR_3DLIGHT = 22,
    COLOR_INFOTEXT = 23,
    COLOR_INFOBK = 24,
    COLOR_HOTLIGHT = 26,
    COLOR_GRADIENTACTIVECAPTION = 27,
    COLOR_GRADIENTINACTIVECAPTION = 28,
    COLOR_MENUHILIGHT = 29,
    COLOR_MENUBAR = 30,
    COLOR_DESKTOP = 1,
    COLOR_3DFACE = 15,
    COLOR_3DSHADOW = 16,
    COLOR_3DHIGHLIGHT = 20,
    COLOR_3DHILIGHT = 20,
    COLOR_BTNHILIGHT = 20,
}

[StructLayout(LayoutKind.Sequential)]
public struct MSG
{
    public nint hwnd;
    public WM message;
    public nuint wParam;
    public nint lParam;
    public uint time;
    public Point pt;
}

public enum WINDOW_LONG_PTR_INDEX
{
    GWL_EXSTYLE = -20,
    GWLP_HINSTANCE = -6,
    GWLP_HWNDPARENT = -8,
    GWLP_ID = -12,
    GWL_STYLE = -16,
    GWLP_USERDATA = -21,
    GWLP_WNDPROC = -4,
    GWL_HINSTANCE = -6,
    GWL_ID = -12,
    GWL_USERDATA = -21,
    GWL_WNDPROC = -4,
    GWL_HWNDPARENT = -8,
}

public struct RECT
{
    private int left;
    private int top;
    private int right;
    private int bottom;

    public static RECT FromRectangle(Rectangle rectangle)
    {
        return new RECT
        {
            left = rectangle.Left,
            top = rectangle.Top,
            right = rectangle.Right + 1,
            bottom = rectangle.Bottom + 1
        };
    }

    public Rectangle ToRectangle() => Rectangle.FromLTRB(left, top, right - 1, bottom - 1);
}

// thanks dotnet/winforms
public enum WM : uint
{
    NULL = 0x0000,
    CREATE = 0x0001,
    DESTROY = 0x0002,
    MOVE = 0x0003,
    SIZE = 0x0005,
    ACTIVATE = 0x0006,
    SETFOCUS = 0x0007,
    KILLFOCUS = 0x0008,
    ENABLE = 0x000A,
    SETREDRAW = 0x000B,
    SETTEXT = 0x000C,
    GETTEXT = 0x000D,
    GETTEXTLENGTH = 0x000E,
    PAINT = 0x000F,
    CLOSE = 0x0010,
    QUERYENDSESSION = 0x0011,
    QUERYOPEN = 0x0013,
    ENDSESSION = 0x0016,
    QUIT = 0x0012,
    ERASEBKGND = 0x0014,
    SYSCOLORCHANGE = 0x0015,
    SHOWWINDOW = 0x0018,
    CTLCOLOR = 0x0019,
    SETTINGCHANGE = 0x001A,
    WININICHANGE = 0x001A,
    DEVMODECHANGE = 0x001B,
    ACTIVATEAPP = 0x001C,
    FONTCHANGE = 0x001D,
    TIMECHANGE = 0x001E,
    CANCELMODE = 0x001F,
    SETCURSOR = 0x0020,
    MOUSEACTIVATE = 0x0021,
    CHILDACTIVATE = 0x0022,
    QUEUESYNC = 0x0023,
    GETMINMAXINFO = 0x0024,
    PAINTICON = 0x0026,
    ICONERASEBKGND = 0x0027,
    NEXTDLGCTL = 0x0028,
    SPOOLERSTATUS = 0x002A,
    DRAWITEM = 0x002B,
    MEASUREITEM = 0x002C,
    DELETEITEM = 0x002D,
    VKEYTOITEM = 0x002E,
    CHARTOITEM = 0x002F,
    SETFONT = 0x0030,
    GETFONT = 0x0031,
    SETHOTKEY = 0x0032,
    GETHOTKEY = 0x0033,
    QUERYDRAGICON = 0x0037,
    COMPAREITEM = 0x0039,
    GETOBJECT = 0x003D,
    COMPACTING = 0x0041,
    COMMNOTIFY = 0x0044,
    WINDOWPOSCHANGING = 0x0046,
    WINDOWPOSCHANGED = 0x0047,
    POWER = 0x0048,
    COPYDATA = 0x004A,
    CANCELJOURNAL = 0x004B,
    NOTIFY = 0x004E,
    INPUTLANGCHANGEREQUEST = 0x0050,
    INPUTLANGCHANGE = 0x0051,
    TCARD = 0x0052,
    HELP = 0x0053,
    USERCHANGED = 0x0054,
    NOTIFYFORMAT = 0x0055,
    CONTEXTMENU = 0x007B,
    STYLECHANGING = 0x007C,
    STYLECHANGED = 0x007D,
    DISPLAYCHANGE = 0x007E,
    GETICON = 0x007F,
    SETICON = 0x0080,
    NCCREATE = 0x0081,
    NCDESTROY = 0x0082,
    NCCALCSIZE = 0x0083,
    NCHITTEST = 0x0084,
    NCPAINT = 0x0085,
    NCACTIVATE = 0x0086,
    GETDLGCODE = 0x0087,
    SYNCPAINT = 0x0088,
    NCMOUSEMOVE = 0x00A0,
    NCLBUTTONDOWN = 0x00A1,
    NCLBUTTONUP = 0x00A2,
    NCLBUTTONDBLCLK = 0x00A3,
    NCRBUTTONDOWN = 0x00A4,
    NCRBUTTONUP = 0x00A5,
    NCRBUTTONDBLCLK = 0x00A6,
    NCMBUTTONDOWN = 0x00A7,
    NCMBUTTONUP = 0x00A8,
    NCMBUTTONDBLCLK = 0x00A9,
    NCXBUTTONDOWN = 0x00AB,
    NCXBUTTONUP = 0x00AC,
    NCXBUTTONDBLCLK = 0x00AD,
    INPUT_DEVICE_CHANGE = 0x00FE,
    INPUT = 0x00FF,
    KEYFIRST = 0x0100,
    KEYDOWN = 0x0100,
    KEYUP = 0x0101,
    CHAR = 0x0102,
    DEADCHAR = 0x0103,
    SYSKEYDOWN = 0x0104,
    SYSKEYUP = 0x0105,
    SYSCHAR = 0x0106,
    SYSDEADCHAR = 0x0107,
    UNICHAR = 0x0109,
    KEYLAST = 0x0109,
    IME_STARTCOMPOSITION = 0x010D,
    IME_ENDCOMPOSITION = 0x010E,
    IME_COMPOSITION = 0x010F,
    IME_KEYLAST = 0x010F,
    INITDIALOG = 0x0110,
    COMMAND = 0x0111,
    SYSCOMMAND = 0x0112,
    TIMER = 0x0113,
    HSCROLL = 0x0114,
    VSCROLL = 0x0115,
    INITMENU = 0x0116,
    INITMENUPOPUP = 0x0117,
    GESTURE = 0x0119,
    GESTURENOTIFY = 0x011A,
    MENUSELECT = 0x011F,
    MENUCHAR = 0x0120,
    ENTERIDLE = 0x0121,
    MENURBUTTONUP = 0x0122,
    MENUDRAG = 0x0123,
    MENUGETOBJECT = 0x0124,
    UNINITMENUPOPUP = 0x0125,
    MENUCOMMAND = 0x0126,
    CHANGEUISTATE = 0x0127,
    UPDATEUISTATE = 0x0128,
    QUERYUISTATE = 0x0129,
    CTLCOLORMSGBOX = 0x0132,
    CTLCOLOREDIT = 0x0133,
    CTLCOLORLISTBOX = 0x0134,
    CTLCOLORBTN = 0x0135,
    CTLCOLORDLG = 0x0136,
    CTLCOLORSCROLLBAR = 0x0137,
    CTLCOLORSTATIC = 0x0138,
    MOUSEFIRST = 0x0200,
    MOUSEMOVE = 0x0200,
    LBUTTONDOWN = 0x0201,
    LBUTTONUP = 0x0202,
    LBUTTONDBLCLK = 0x0203,
    RBUTTONDOWN = 0x0204,
    RBUTTONUP = 0x0205,
    RBUTTONDBLCLK = 0x0206,
    MBUTTONDOWN = 0x0207,
    MBUTTONUP = 0x0208,
    MBUTTONDBLCLK = 0x0209,
    MOUSEWHEEL = 0x020A,
    XBUTTONDOWN = 0x020B,
    XBUTTONUP = 0x020C,
    XBUTTONDBLCLK = 0x020D,
    MOUSEHWHEEL = 0x020E,
    MOUSELAST = 0x020E,
    PARENTNOTIFY = 0x0210,
    ENTERMENULOOP = 0x0211,
    EXITMENULOOP = 0x0212,
    NEXTMENU = 0x0213,
    SIZING = 0x0214,
    CAPTURECHANGED = 0x0215,
    MOVING = 0x0216,
    POWERBROADCAST = 0x0218,
    DEVICECHANGE = 0x0219,
    MDICREATE = 0x0220,
    MDIDESTROY = 0x0221,
    MDIACTIVATE = 0x0222,
    MDIRESTORE = 0x0223,
    MDINEXT = 0x0224,
    MDIMAXIMIZE = 0x0225,
    MDITILE = 0x0226,
    MDICASCADE = 0x0227,
    MDIICONARRANGE = 0x0228,
    MDIGETACTIVE = 0x0229,
    MDISETMENU = 0x0230,
    ENTERSIZEMOVE = 0x0231,
    EXITSIZEMOVE = 0x0232,
    DROPFILES = 0x0233,
    MDIREFRESHMENU = 0x0234,
    POINTERDEVICECHANGE = 0x0238,
    POINTERDEVICEINRANGE = 0x0239,
    POINTERDEVICEOUTOFRANGE = 0x023A,
    TOUCH = 0x0240,
    NCPOINTERUPDATE = 0x0241,
    NCPOINTERDOWN = 0x0242,
    NCPOINTERUP = 0x0243,
    POINTERUPDATE = 0x0245,
    POINTERDOWN = 0x0246,
    POINTERUP = 0x0247,
    POINTERENTER = 0x0249,
    POINTERLEAVE = 0x024A,
    POINTERACTIVATE = 0x024B,
    POINTERCAPTURECHANGED = 0x024C,
    TOUCHHITTESTING = 0x024D,
    POINTERWHEEL = 0x024E,
    POINTERHWHEEL = 0x024F,
    POINTERROUTEDTO = 0x0251,
    POINTERROUTEDAWAY = 0x0252,
    POINTERROUTEDRELEASED = 0x0253,
    IME_SETCONTEXT = 0x0281,
    IME_NOTIFY = 0x0282,
    IME_CONTROL = 0x0283,
    IME_COMPOSITIONFULL = 0x0284,
    IME_SELECT = 0x0285,
    IME_CHAR = 0x0286,
    IME_REQUEST = 0x0288,
    IME_KEYDOWN = 0x0290,
    IME_KEYUP = 0x0291,
    MOUSEHOVER = 0x02A1,
    MOUSELEAVE = 0x02A3,
    NCMOUSEHOVER = 0x02A0,
    NCMOUSELEAVE = 0x02A2,
    WTSSESSION_CHANGE = 0x02B1,
    DPICHANGED = 0x02E0,
    DPICHANGED_BEFOREPARENT = 0x02E2,
    DPICHANGED_AFTERPARENT = 0x02E3,
    GETDPISCALEDSIZE = 0x02E4,
    CUT = 0x0300,
    COPY = 0x0301,
    PASTE = 0x0302,
    CLEAR = 0x0303,
    UNDO = 0x0304,
    RENDERFORMAT = 0x0305,
    RENDERALLFORMATS = 0x0306,
    DESTROYCLIPBOARD = 0x0307,
    DRAWCLIPBOARD = 0x0308,
    PAINTCLIPBOARD = 0x0309,
    VSCROLLCLIPBOARD = 0x030A,
    SIZECLIPBOARD = 0x030B,
    ASKCBFORMATNAME = 0x030C,
    CHANGECBCHAIN = 0x030D,
    HSCROLLCLIPBOARD = 0x030E,
    QUERYNEWPALETTE = 0x030F,
    PALETTEISCHANGING = 0x0310,
    PALETTECHANGED = 0x0311,
    HOTKEY = 0x0312,
    PRINT = 0x0317,
    PRINTCLIENT = 0x0318,
    APPCOMMAND = 0x0319,
    THEMECHANGED = 0x031A,
    CLIPBOARDUPDATE = 0x031D,
    DWMCOMPOSITIONCHANGED = 0x031E,
    DWMNCRENDERINGCHANGED = 0x031F,
    DWMCOLORIZATIONCOLORCHANGED = 0x0320,
    DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
    DWMSENDICONICTHUMBNAIL = 0x0323,
    DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,
    GETTITLEBARINFOEX = 0x033F,
    HANDHELDFIRST = 0x0358,
    HANDHELDLAST = 0x035F,
    AFXFIRST = 0x0360,
    AFXLAST = 0x037F,
    PENWINFIRST = 0x0380,
    PENWINLAST = 0x038F,
    USER = 0x0400,
    CHOOSEFONT_GETLOGFONT = USER + 1,
    APP = 0x8000,
    REFLECT = USER + 0x1C00,
    REFLECT_NOTIFY = REFLECT + NOTIFY,
    REFLECT_NOTIFYFORMAT = REFLECT + NOTIFYFORMAT,
    REFLECT_COMMAND = REFLECT + COMMAND,
    REFLECT_CHARTOITEM = REFLECT + CHARTOITEM,
    REFLECT_VKEYTOITEM = REFLECT + VKEYTOITEM,
    REFLECT_DRAWITEM = REFLECT + DRAWITEM,
    REFLECT_MEASUREITEM = REFLECT + MEASUREITEM,
    REFLECT_HSCROLL = REFLECT + HSCROLL,
    REFLECT_VSCROLL = REFLECT + VSCROLL,
    REFLECT_CTLCOLOR = REFLECT + CTLCOLOR,
    REFLECT_CTLCOLORBTN = REFLECT + CTLCOLORBTN,
    REFLECT_CTLCOLORDLG = REFLECT + CTLCOLORDLG,
    REFLECT_CTLCOLORMSGBOX = REFLECT + CTLCOLORMSGBOX,
    REFLECT_CTLCOLORSCROLLBAR = REFLECT + CTLCOLORSCROLLBAR,
    REFLECT_CTLCOLOREDIT = REFLECT + CTLCOLOREDIT,
    REFLECT_CTLCOLORLISTBOX = REFLECT + CTLCOLORLISTBOX,
    REFLECT_CTLCOLORSTATIC = REFLECT + CTLCOLORSTATIC
}

public struct INITCOMMONCONTROLSEX
{
    public uint dwSize;
    public INITCOMMONCONTROLSEX_ICC dwICC;
}

[Flags]
public enum INITCOMMONCONTROLSEX_ICC : uint
{
    ICC_ANIMATE_CLASS = 0x00000080,
    ICC_BAR_CLASSES = 0x00000004,
    ICC_COOL_CLASSES = 0x00000400,
    ICC_DATE_CLASSES = 0x00000100,
    ICC_HOTKEY_CLASS = 0x00000040,
    ICC_INTERNET_CLASSES = 0x00000800,
    ICC_LINK_CLASS = 0x00008000,
    ICC_LISTVIEW_CLASSES = 0x00000001,
    ICC_NATIVEFNTCTL_CLASS = 0x00002000,
    ICC_PAGESCROLLER_CLASS = 0x00001000,
    ICC_PROGRESS_CLASS = 0x00000020,
    ICC_STANDARD_CLASSES = 0x00004000,
    ICC_TAB_CLASSES = 0x00000008,
    ICC_TREEVIEW_CLASSES = 0x00000002,
    ICC_UPDOWN_CLASS = 0x00000010,
    ICC_USEREX_CLASSES = 0x00000200,
    ICC_WIN95_CLASSES = 0x000000FF,
}

public unsafe struct ACTCTXW
{
    internal uint cbSize;
    internal uint dwFlags;
    internal char* lpSource;
    internal ushort wProcessorArchitecture;
    internal ushort wLangId;
    internal char* lpAssemblyDirectory;
    internal char* lpResourceName;
    internal char* lpApplicationName;
    internal nint hModule;
}