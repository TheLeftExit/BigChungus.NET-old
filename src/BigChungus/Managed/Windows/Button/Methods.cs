using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public readonly partial record struct Button(nint Handle) : ICommonControl<Button>
{
    public Window Attributes => new Window(Handle);
    static string ICommonControl<Button>.ClassName => "Button";

    public SIZE GetIdealSize(int width = 0)
    {
        SIZE result = new SIZE { cy = width };
        Handle.SendMessage_Ref(BCM.GETIDEALSIZE, 0, ref result).ThrowIf(0);
        return result;
    }

    public BUTTON_IMAGELIST GetImageList()
    {
        Handle.SendMessage_Out(BCM.GETIMAGELIST, 0, out BUTTON_IMAGELIST listInfo).ThrowIf(0);
        return listInfo;
    }

    public void GetNote(Span<char> buffer)
    {
        Handle.SendMessage_SpanChar(BCM.GETNOTE, buffer.Length, buffer).ThrowIf(0);
    }

    public int GetNoteLength()
    {
        return (int)Handle.SendMessage(BCM.GETNOTELENGTH, 0, 0);
    }

    public string GetNote()
    {
        Span<char> buffer = stackalloc char[GetNoteLength()];
        GetNote(buffer);
        return buffer.ToNullTerminatedString();
    }

    public BUTTON_SPLITINFO GetSplitInfo()
    {
        Handle.SendMessage_Out(BCM.GETSPLITINFO, 0, out BUTTON_SPLITINFO splitInfo).ThrowIf(0);
        return splitInfo;
    }

    public RECT GetTextMargin()
    {
        Handle.SendMessage_Out(BCM.GETTEXTMARGIN, 0, out RECT margin).ThrowIf(0);
        return margin;
    }

    public void SetDropDownState(bool state)
    {
        Handle.SendMessage(BCM.SETDROPDOWNSTATE, state ? 1 : 0, 0).ThrowIf(0);
    }

    public void SetImageList(BUTTON_IMAGELIST listInfo)
    {
        Handle.SendMessage_In(BCM.SETIMAGELIST, 0, in listInfo).ThrowIf(0);
    }

    public void SetNote(ReadOnlySpan<char> newText)
    {
        Handle.SendMessage_SpanChar(BCM.SETNOTE, 0, newText).ThrowIf(0);
    }

    public void SetElevationRequiredState(bool state)
    {
        Handle.SendMessage(BCM.SETSHIELD, 0, state ? 1 : 0).ThrowIfNot(0);
    }

    public void SetSplitInfo(BUTTON_SPLITINFO splitInfo)
    {
        Handle.SendMessage_In(BCM.SETSPLITINFO, 0, in splitInfo).ThrowIf(0);
    }

    public void SetTextMargin(RECT margin)
    {
        Handle.SendMessage_In(BCM.SETTEXTMARGIN, 0, in margin).ThrowIf(0);
    }

    public void Click()
    {
        Handle.SendMessage(BM.CLICK, 0, 0);
    }

    public BST GetCheck()
    {
        return (BST)Handle.SendMessage(BM.GETCHECK, 0, 0);
    }

    public nint GetImage(IMAGE imageType)
    {
        return Handle.SendMessage(BM.GETIMAGE, (nint)imageType, 0);
    }

    public BST GetState()
    {
        return (BST)Handle.SendMessage(BM.GETSTATE, 0, 0);
    }

    public void SetCheck(BST state)
    {
        Handle.SendMessage(BM.SETCHECK, (nint)state, 0);
    }

    public void SetDontClick(bool dontClick)
    {
        Handle.SendMessage(BM.SETDONTCLICK, dontClick ? 1 : 0, 0);
    }

    public nint SetImage(nint image, IMAGE imageType)
    {
        return Handle.SendMessage(BM.SETIMAGE, (nint)imageType, image);
    }

    public void SetState(bool highlight)
    {
        Handle.SendMessage(BM.SETSTATE, highlight ? 1 : 0, 0);
    }

    public void SetStyle(uint style, bool redraw)
    {
        Handle.SendMessage(BM.SETSTYLE, (nint)style, redraw ? 1 : 0);
    }
}
