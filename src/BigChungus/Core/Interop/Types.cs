using BigChungus.Common;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BigChungus.Core.Interop;

public delegate nint WNDPROC(nint hWnd, WM msg, nint wParam, nint lParam);

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

[StructLayout(LayoutKind.Sequential)]
public struct MSG {
    public nint hwnd;
    public WM message;
    public nint wParam;
    public nint lParam;
    public uint time;
    public Point pt;
    public int lPrivate;
}

public struct INITCOMMONCONTROLSEX {
    public uint dwSize;
    public INITCOMMONCONTROLSEX_ICC dwICC;
}

public unsafe struct ACTCTXW {
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

[Flags]
public enum INITCOMMONCONTROLSEX_ICC : uint {
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

public enum WINDOW_LONG_PTR_INDEX {
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

public struct RECT {
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