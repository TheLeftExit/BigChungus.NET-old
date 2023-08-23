using BigChungus.Unmanaged.WindowStyles;

namespace BigChungus.Managed;

[Flags]
public enum ButtonKind : uint
{
    Push = BS.PUSHBUTTON,
    Check = BS.AUTOCHECKBOX,
    ThreeState = BS.AUTO3STATE,
    Radio = BS.RADIOBUTTON,
    PushLike = BS.PUSHLIKE,
    CommandLink = BS.COMMANDLINK
}

public enum ButtonGlyphAlignment : uint
{
    BeforeText = 0,
    AfterText = BS.LEFTTEXT
}

public struct ButtonArgs
{
    public ButtonArgs(ButtonKind kind = default, ButtonGlyphAlignment glyphAlignment = default, bool multiline = false)
    {
        Kind = kind;
        GlyphAlignment = glyphAlignment;
        Multiline = multiline;
    }
    public ButtonKind Kind { get; set; }
    public ButtonGlyphAlignment GlyphAlignment { get; set; }
    public bool Multiline { get; set; }

    public readonly nint Create(nint parent)
    {
        var style = Internal.BaseStyle | BS.NOTIFY | (uint)Kind | (uint)GlyphAlignment;
        if (Multiline) style |= BS.MULTILINE;
        return Internal.Create("Button", style, default, parent);
    }
}