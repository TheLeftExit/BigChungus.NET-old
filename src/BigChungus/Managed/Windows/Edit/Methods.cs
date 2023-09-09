using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public readonly partial record struct Edit(nint Handle) : IWindow
{
    public Window Attributes => new Window(Handle);
    public Edit(nint parent, uint style) : this(Internal.CreateEx("Edit", parent, style)) { }

    public bool CanUndo(Span<char> buffer)
    {
        return Handle.SendMessage(EM.CANUNDO, 0, 0) > 0;
    }

    public (ushort CharIndex, ushort LineIndex) CharFromPos(ushort x, ushort y)
    {
        return (DWord)Handle.SendMessage(EM.CHARFROMPOS, 0, new DWord(x, y));
    }

    public void EmptyUndoBuffer()
    {
        Handle.SendMessage(EM.EMPTYUNDOBUFFER, 0, 0);
    }

    public void FmtLines(bool insertSoftLineBreaks)
    {
        Handle.SendMessage(EM.FMTLINES, insertSoftLineBreaks ? 1 : 0, 0);
    }

    public void GetCueBanner(Span<char> buffer)
    {
        Handle.SendMessage_SpanChar(EM.GETCUEBANNER, buffer, buffer.Length).ThrowIf(0);
    }

    public int GetFirstVisibleLine()
    {
        return (int)Handle.SendMessage(EM.GETFIRSTVISIBLELINE, 0, 0);
    }

    public nint GetHandle()
    {
        return Handle.SendMessage(EM.GETHANDLE, 0, 0);
    }

    public EIMES GetImeStatus()
    {
        return (EIMES)Handle.SendMessage(EM.GETIMESTATUS, (nint)EMSIS.COMPOSITIONSTRING, 0);
    }

    public int GetLimitText()
    {
        return (int)Handle.SendMessage(EM.GETLIMITTEXT, 0, 0);
    }

    public int GetLine(int lineIndex, Span<char> buffer)
    {
        buffer[0] = (char)buffer.Length;
        return (int)Handle.SendMessage_SpanChar(EM.GETLINE, lineIndex, buffer);
    }

    public int GetLineCount()
    {
        return (int)Handle.SendMessage(EM.GETLINECOUNT, 0, 0);
    }

    public (ushort LeftMargin, ushort RightMargin) GetMargins()
    {
        return (DWord)Handle.SendMessage(EM.GETMARGINS, 0, 0);
    }

    public bool GetModify()
    {
        return Handle.SendMessage(EM.GETMODIFY, 0, 0) != 0;
    }

    public char GetPasswordChar()
    {
        return (char)Handle.SendMessage(EM.GETPASSWORDCHAR, 0, 0);
    }

    public RECT GetRect()
    {
        Handle.SendMessage_Out(EM.GETRECT, 0, out RECT rect);
        return rect;
    }

    public (int StartPos, int EndPos) GetSel()
    {
        Handle.SendMessage_Out(EM.GETSEL, out nint start, out nint end);
        return ((int)start, (int)end);
    }

    public int GetThumb()
    {
        return (int)Handle.SendMessage(EM.GETTHUMB, 0, 0);
    }

    public nint GetWordBreakProc()
    {
        return Handle.SendMessage(EM.GETWORDBREAKPROC, 0, 0);
    }

    public void HideBalloonTip()
    {
        Handle.SendMessage(EM.HIDEBALLOONTIP, 0, 0).ThrowIf(0);
    }

    public void SetLimitText(int count)
    {
        Handle.SendMessage(EM.SETLIMITTEXT, count, 0);
    }

    public int LineFromChar(int charIndex = -1)
    {
        return (int)Handle.SendMessage(EM.LINEFROMCHAR, charIndex, 0);
    }

    public int LineIndex(int lineIndex = -1)
    {
        return (int)Handle.SendMessage(EM.LINEINDEX, lineIndex, 0);
    }

    public int LineLength(int charIndex = -1)
    {
        return (int)Handle.SendMessage(EM.LINELENGTH, charIndex, 0);
    }

    public void LineScroll(int horzCharOffset, int vertLineOffset)
    {
        Handle.SendMessage(EM.LINESCROLL, horzCharOffset, vertLineOffset);
    }

    public (ushort X, ushort Y) PosFromChar(int charIndex)
    {
        return (DWord)Handle.SendMessage(EM.POSFROMCHAR, charIndex, 0);
    }

    public void ReplaceSel(bool canUndo, ReadOnlySpan<char> newText)
    {
        Handle.SendMessage_SpanChar(EM.REPLACESEL, canUndo ? 1 : 0, newText);
    }

    public int Scroll(SB action)
    {
        var result = (DWord)Handle.SendMessage(EM.SCROLL, (nint)action, 0);
        result.High.ThrowIf((ushort)0);
        return result.Low;
    }

    public void ScrollCaret()
    {
        Handle.SendMessage(EM.SCROLLCARET, 0, 0);
    }

    public void SetCueBanner(bool showIfFocused, ReadOnlySpan<char> text)
    {
        Handle.SendMessage_SpanChar(EM.SETCUEBANNER, showIfFocused ? 1 : 0, text);
    }

    public void SetHandle(nint newHandle)
    {
        Handle.SendMessage(EM.SETHANDLE, newHandle, 0);
    }

    public EIMES SetImeStatus(EIMES newStatus)
    {
        return (EIMES)Handle.SendMessage(EM.SETIMESTATUS, (nint)EMSIS.COMPOSITIONSTRING, (nint)newStatus);
    }

    public void SetMargins(EC marginType, ushort? leftMargin, ushort? rightMargin)
    {
        var lParam = new DWord(leftMargin ?? (ushort)EC.USEFONTINFO, rightMargin ?? (ushort)EC.USEFONTINFO);
        Handle.SendMessage(EM.SETMARGINS, (nint)marginType, lParam);
    }

    public void SetModify(bool newModified)
    {
        Handle.SendMessage(EM.SETMODIFY, newModified ? 1 : 0, 0);
    }

    public void SetPasswordChar(char? newChar)
    {
        Handle.SendMessage(EM.SETPASSWORDCHAR, newChar ?? 0, 0);
    }

    public void SetReadOnly(bool readOnly)
    {
        Handle.SendMessage(EM.SETREADONLY, readOnly ? 1 : 0, 0).ThrowIf(0);
    }

    public void SetRect(RECT? newRect)
    {
        if (newRect is RECT rect)
        {
            Handle.SendMessage_In(EM.SETRECT, 0, in rect);
        } else
        {
            Handle.SendMessage(EM.SETRECT, 0, 0);
        }
    }

    public void SetRectNp(RECT? newRect)
    {
        if (newRect is RECT rect)
        {
            Handle.SendMessage_In(EM.SETRECTNP, 0, in rect);
        } else
        {
            Handle.SendMessage(EM.SETRECTNP, 0, 0);
        }
    }

    public void SetSel(int start, int end)
    {
        Handle.SendMessage(EM.SETSEL, start, end);
    }

    public void SetTabStops()
    {
        throw new NotImplementedException("Wtf is this?");
    }

    public void SetWordBreakProc(nint funcPtr)
    {
        Handle.SendMessage(EM.SETWORDBREAKPROC, 0, funcPtr);
    }

    public void ShowBalloonTip(EDITBALLOONTIP tipInfo)
    {
        Handle.SendMessage_In(EM.SHOWBALLOONTIP, 0, in tipInfo).ThrowIf(0);
    }

    public void Undo()
    {
        Handle.SendMessage(EM.UNDO, 0, 0);
    }
}