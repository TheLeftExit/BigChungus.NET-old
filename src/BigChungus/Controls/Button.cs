using BigChungus.Common;

namespace BigChungus.Controls;

public class Button(Control parent) : Control(parent)
{
    protected override WindowArgs CreateWindowArgs()
    {
        return base.CreateWindowArgs() with { ClassName = "Button" };
    }
    internal void RaiseClicked()
    {
        Clicked?.Invoke(this);
    }
    public event Action<Button> Clicked;
}

internal static class ButtonConstants
{
    public const WINDOW_STYLE BS_PUSHBUTTON = 0;
    public const WINDOW_STYLE BS_DEFPUSHBUTTON = (WINDOW_STYLE)1;
    public const WINDOW_STYLE BS_CHECKBOX = (WINDOW_STYLE)2;
    public const WINDOW_STYLE BS_AUTOCHECKBOX = (WINDOW_STYLE)3;
    public const WINDOW_STYLE BS_RADIOBUTTON = (WINDOW_STYLE)4;
    public const WINDOW_STYLE BS_3STATE = (WINDOW_STYLE)5;
    public const WINDOW_STYLE BS_AUTO3STATE = (WINDOW_STYLE)6;
    public const WINDOW_STYLE BS_GROUPBOX = (WINDOW_STYLE)7;
    public const WINDOW_STYLE BS_USERBUTTON = (WINDOW_STYLE)8;
    public const WINDOW_STYLE BS_AUTORADIOBUTTON = (WINDOW_STYLE)9;
    public const WINDOW_STYLE BS_PUSHBOX = (WINDOW_STYLE)10;
    public const WINDOW_STYLE BS_OWNERDRAW = (WINDOW_STYLE)11;
    public const WINDOW_STYLE BS_TYPEMASK = (WINDOW_STYLE)15;
    public const WINDOW_STYLE BS_LEFTTEXT = (WINDOW_STYLE)32;
    public const WINDOW_STYLE BS_TEXT = 0;
    public const WINDOW_STYLE BS_ICON = (WINDOW_STYLE)64;
    public const WINDOW_STYLE BS_BITMAP = (WINDOW_STYLE)128;
    public const WINDOW_STYLE BS_LEFT = (WINDOW_STYLE)256;
    public const WINDOW_STYLE BS_RIGHT = (WINDOW_STYLE)512;
    public const WINDOW_STYLE BS_CENTER = (WINDOW_STYLE)768;
    public const WINDOW_STYLE BS_TOP = (WINDOW_STYLE)1024;
    public const WINDOW_STYLE BS_BOTTOM = (WINDOW_STYLE)2048;
    public const WINDOW_STYLE BS_VCENTER = (WINDOW_STYLE)3072;
    public const WINDOW_STYLE BS_PUSHLIKE = (WINDOW_STYLE)4096;
    public const WINDOW_STYLE BS_MULTILINE = (WINDOW_STYLE)8192;
    public const WINDOW_STYLE BS_NOTIFY = (WINDOW_STYLE)16384;
    public const WINDOW_STYLE BS_FLAT = (WINDOW_STYLE)32768;
    public const WINDOW_STYLE BS_RIGHTBUTTON = (WINDOW_STYLE)32;

    public const nint BN_CLICKED = (nint)0U;
    public const nint BN_PAINT = (nint)1U;
    public const nint BN_HILITE = (nint)2U;
    public const nint BN_UNHILITE = (nint)3U;
    public const nint BN_DISABLE = (nint)4U;
    public const nint BN_DOUBLECLICKED = (nint)5U;
    public const nint BN_PUSHED = (nint)2U;
    public const nint BN_UNPUSHED = (nint)3U;
    public const nint BN_DBLCLK = (nint)5U;
    public const nint BN_SETFOCUS = (nint)6U;
    public const nint BN_KILLFOCUS = (nint)7U;
    public const WM BM_GETCHECK = (WM)240U;
    public const WM BM_SETCHECK = (WM)241U;
    public const WM BM_GETSTATE = (WM)242U;
    public const WM BM_SETSTATE = (WM)243U;
    public const WM BM_SETSTYLE = (WM)244U;
    public const WM BM_CLICK = (WM)245U;
    public const WM BM_GETIMAGE = (WM)246U;
    public const WM BM_SETIMAGE = (WM)247U;
    public const WM BM_SETDONTCLICK = (WM)248U;
}