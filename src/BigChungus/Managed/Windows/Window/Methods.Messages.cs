using BigChungus.Unmanaged;
using System.ComponentModel;
using System.Drawing;

namespace BigChungus.Managed;

public readonly partial record struct Window(nint Handle) : IWindow
{
    [Obsolete, EditorBrowsable(EditorBrowsableState.Never)]
    public Window Attributes => this;

    public nint GetFont()
    {
        return Handle.SendMessage(WM.GETFONT, 0, 0);
    }

    public int GetText(Span<char> buffer)
    {
        return (int)Handle.SendMessage_SpanChar(WM.GETTEXT, buffer.Length, buffer);
    }

    public int GetTextLength()
    {
        return (int)Handle.SendMessage(WM.GETTEXTLENGTH, 0, 0);
    }

    public string GetText()
    {
        Span<char> buffer = stackalloc char[GetTextLength()];
        GetText(buffer);
        return buffer.ToNullTerminatedString();
    }

    public void SetFont(nint fontHandle)
    {
        Handle.SendMessage(WM.SETFONT, fontHandle, 1);
    }

    public void SetText(ReadOnlySpan<char> buffer)
    {
        Handle.SendMessage_SpanChar(WM.SETTEXT, 0, buffer).ThrowIf(0);
    }
}
