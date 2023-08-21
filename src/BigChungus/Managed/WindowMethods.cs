using BigChungus.Unmanaged;
using BigChungus.Unmanaged.Libraries;
using BigChungus.Unmanaged.Messages;
using System.Drawing;

namespace BigChungus.Managed;

public static class WindowMethods
{
    public static nint GetFont(nint handle)
    {
        return User32.SendMessage(handle, WM.GETFONT, 0, 0);
    }

    public static void SetFont(nint handle, nint fontHandle)
    {
        User32.SendMessage(handle, WM.SETFONT, fontHandle, 1);
    }

    public unsafe static string GetText(nint handle)
    {
        var length = User32.GetWindowTextLength(handle);
        Span<char> buffer = stackalloc char[length];
        fixed (char* text = buffer) User32.GetWindowText(handle, text, length);
        return new string(buffer);
    }

    public static unsafe void SetText(nint handle, ReadOnlySpan<char> text)
    {
        fixed (char* ptr = text)
        {
            var returnValue = User32.SetWindowText(handle, ptr);
            ReturnValueException.ThrowIf(nameof(User32.SetWindowText), returnValue is false);
        }
    }

    public static Rectangle GetBounds(nint handle)
    {
        var returnValue = User32.GetWindowRect(handle, out var result);
        ReturnValueException.ThrowIf(nameof(User32.GetWindowRect), returnValue is false);
        return result.ToRectangle();
    }

    public static void SetBounds(nint handle, Rectangle newBounds, bool repaint = true)
    {
        var returnValue = User32.MoveWindow(handle, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height, repaint);
        ReturnValueException.ThrowIf(nameof(User32.MoveWindow), returnValue is false);
    }

    public static nint GetParent(nint handle)
    {
        return User32.GetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWLP_HWNDPARENT);
    }

    public static void SetParent(nint handle, nint newParent)
    {
        User32.SetParent(handle, newParent);
    }

    public static void Update(nint handle)
    {
        var returnValue = User32.UpdateWindow(handle);
        ReturnValueException.ThrowIf(nameof(User32.UpdateWindow), returnValue is false);
    }

    public static void Destroy(nint handle)
    {
        var returnValue = User32.DestroyWindow(handle);
        ReturnValueException.ThrowIf(nameof(User32.DestroyWindow), returnValue is false);
    }

    public static void SetEnabled(nint handle, bool enabled)
    {
        User32.EnableWindow(handle, enabled);
    }

    public static bool GetEnabled(nint handle)
    {
        return User32.IsWindowEnabled(handle);
    }
}
