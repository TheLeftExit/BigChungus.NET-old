using BigChungus.Unmanaged;
using BigChungus.Unmanaged.Libraries;
using BigChungus.Unmanaged.Messages;
using System.Drawing;

namespace BigChungus.Managed;

public static class WindowExtensions
{
    public static Window AsWindow<T>(this T control) where T : IWindow
    {
        return new Window(control.Handle);
    }
}

public interface IWindow
{
    nint Handle { get; }
}

public readonly record struct Window(nint Handle)
{
    public nint GetFont()
    {
        return User32.SendMessage(Handle, WM.GETFONT, 0, 0);
    }

    public void SetFont(nint fontHandle)
    {
        User32.SendMessage(Handle, WM.SETFONT, fontHandle, 1);
    }

    public unsafe string GetText()
    {
        var length = User32.GetWindowTextLength(Handle);
        Span<char> buffer = stackalloc char[length];
        fixed (char* text = buffer) User32.GetWindowText(Handle, text, length);
        return new string(buffer);
    }

    public unsafe void SetText(ReadOnlySpan<char> text)
    {
        fixed (char* ptr = text)
        {
            var returnValue = User32.SetWindowText(Handle, ptr);
            ReturnValueException.ThrowIf(nameof(User32.SetWindowText), returnValue is false);
        }
    }

    public Rectangle GetBounds()
    {
        var returnValue = User32.GetWindowRect(Handle, out var result);
        ReturnValueException.ThrowIf(nameof(User32.GetWindowRect), returnValue is false);
        return result.ToRectangle();
    }

    public void SetBounds(Rectangle newBounds, bool repaint = true)
    {
        var returnValue = User32.MoveWindow(Handle, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height, repaint);
        ReturnValueException.ThrowIf(nameof(User32.MoveWindow), returnValue is false);
    }

    public nint GetParent()
    {
        return User32.GetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWLP_HWNDPARENT);
    }

    public void SetParent(nint newParent)
    {
        User32.SetParent(Handle, newParent);
    }

    public void Update()
    {
        var returnValue = User32.UpdateWindow(Handle);
        ReturnValueException.ThrowIf(nameof(User32.UpdateWindow), returnValue is false);
    }

    public void Destroy()
    {
        var returnValue = User32.DestroyWindow(Handle);
        ReturnValueException.ThrowIf(nameof(User32.DestroyWindow), returnValue is false);
    }

    public void SetEnabled(bool enabled)
    {
        User32.EnableWindow(Handle, enabled);
    }

    public bool GetEnabled()
    {
        return User32.IsWindowEnabled(Handle);
    }
}
