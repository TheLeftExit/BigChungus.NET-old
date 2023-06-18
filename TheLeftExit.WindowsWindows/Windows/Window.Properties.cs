using System.Drawing;

public partial class Window
{
    public WINDOW_STYLE Style
    {
        get => (WINDOW_STYLE)User32.GetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        set => User32.SetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_STYLE, (long)value);
    }

    public WINDOW_EX_STYLE ExStyle
    {
        get => (WINDOW_EX_STYLE)User32.GetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        set => User32.SetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, (long)value);
    }

    public unsafe string Text
    {
        get
        {
            var length = User32.GetWindowTextLength(Handle);
            var buffer = stackalloc char[length];
            User32.GetWindowText(Handle, buffer, length);
            return new string(buffer);
        }
        set
        {
            fixed (char* ptr = value)
            {
                User32.SetWindowText(Handle, ptr);
            }
        }
    }

    public Rectangle Bounds
    {
        get
        {
            User32.GetWindowRect(Handle, out var result);
            return result.ToRectangle();
        }
        set
        {
            User32.MoveWindow(Handle, value.X, value.Y, value.Width, value.Height, true);
        }
    }

    public Window Parent
    {
        get
        {
            var parentHandle = User32.GetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWLP_HWNDPARENT);
            return windows.GetValueOrDefault(parentHandle);
        }
        set
        {
            var result = User32.SetParent(Handle, value.Handle);
        }
    }
}