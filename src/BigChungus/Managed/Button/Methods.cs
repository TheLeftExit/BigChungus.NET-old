using BigChungus.Unmanaged;
using BigChungus.Unmanaged.Libraries;
using BigChungus.Unmanaged.Messages;
using System.Drawing;

namespace BigChungus.Managed;

public readonly record struct Button(nint Handle) : IWindow
{
    public Size GetIdealSize(int width = 0)
    {
        Size result = new Size(width, 0);
        var returnValue = Internal.SendMessage(Handle, BCM.GETIDEALSIZE, 0, ref result);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
        return result;
    }

    public string GetNote()
    {
        var length = (int)User32.SendMessage(Handle, BCM.GETNOTELENGTH, 0, 0);
        Span<char> buffer = stackalloc char[length];
        nint returnValue = Internal.SendMessage(Handle, BCM.GETNOTE, buffer.Length, buffer);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
        return new string(buffer);
    }

    public Rectangle GetTextMargin()
    {
        RECT result = default;
        var returnValue = Internal.SendMessage(Handle, BCM.SETTEXTMARGIN, 0, ref result);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
        return result.ToRectangle();
    }

    public void SetNote(ReadOnlySpan<char> newText)
    {
        var returnValue = Internal.SendMessage(Handle, BCM.SETNOTE, 0, newText);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
    }

    public void SetElevationRequiredState(bool state)
    {
        var returnValue = User32.SendMessage(Handle, BCM.SETSHIELD, 0, state ? 1 : 0);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is not 0);
    }

    public void SetTextMargin(Rectangle newMargin)
    {
        var rect = RECT.FromRectangle(newMargin);
        var returnValue = Internal.SendMessage(Handle, BCM.SETTEXTMARGIN, 0, ref rect);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
    }

    public void Click()
    {
        User32.SendMessage(Handle, BM.CLICK, 0, 0);
    }
}
