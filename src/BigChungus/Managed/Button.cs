using BigChungus.Unmanaged;
using BigChungus.Unmanaged.Libraries;
using BigChungus.Unmanaged.Messages;
using System.Drawing;

namespace BigChungus.Managed;

public static class ButtonMethods
{
    public static Size GetIdealSize(nint handle, int width = 0)
    {
        Size result = new Size(width, 0);
        var returnValue = InternalMethods.SendMessage(handle, BCM.GETIDEALSIZE, 0, ref result);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
        return result;
    }

    public static string GetNote(nint handle)
    {
        var length = (int)User32.SendMessage(handle, BCM.GETNOTELENGTH, 0, 0);
        Span<char> buffer = stackalloc char[length];
        nint returnValue = InternalMethods.SendMessage(handle, BCM.GETNOTE, buffer.Length, buffer);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
        return new string(buffer);
    }

    public static Rectangle GetTextMargin(nint handle)
    {
        RECT result = default;
        var returnValue = InternalMethods.SendMessage(handle, BCM.SETTEXTMARGIN, 0, ref result);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
        return result.ToRectangle();
    }

    public static void SetNote(nint handle, ReadOnlySpan<char> newText)
    {
        var returnValue = InternalMethods.SendMessage(handle, BCM.SETNOTE, 0, newText);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
    }

    public static void SetElevationRequiredState(nint handle, bool state)
    {
        var returnValue = User32.SendMessage(handle, BCM.SETSHIELD, 0, state ? 1 : 0);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is not 0);
    }

    public static void SetTextMargin(nint handle, Rectangle newMargin)
    {
        var rect = RECT.FromRectangle(newMargin);
        var returnValue = InternalMethods.SendMessage(handle, BCM.SETTEXTMARGIN, 0, ref rect);
        ReturnValueException.ThrowIf(nameof(User32.SendMessage), returnValue is 0);
    }

    public static void Click(nint handle)
    {
        User32.SendMessage(handle, BM.CLICK, 0, 0);
    }
}

public static class ButtonConstructors
{

}