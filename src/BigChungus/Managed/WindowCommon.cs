using BigChungus.Common;
using BigChungus.Unmanaged;
using System.Drawing;

namespace BigChungus.Managed;

public static class WindowCommon
{
    public static nint GetFont(nint handle)
    {
        return PInvoke.SendMessage(handle, WM.GETFONT, 0, 0);
    }

    public static void SetFont(nint handle, nint fontHandle)
    {
        PInvoke.SendMessage(handle, WM.SETFONT, fontHandle, 1);
    }
    
    public static Rectangle GetBounds(nint handle)
    {
        var returnValue = PInvoke.GetWindowRect(handle, out var result);
        return result.ToRectangle();
    }

    public static void SetBounds(nint handle, Rectangle newBounds)
    {
        PInvoke.MoveWindow(handle, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height, true);
    }

    public static int GetTextLength(nint handle)
    {
        return PInvoke.GetWindowTextLength(handle);
    }

    public static unsafe void GetText(nint handle, Span<char> buffer)
    {
        fixed (char* ptr = buffer)
        {
            PInvoke.GetWindowText(handle, ptr, buffer.Length);
        }
    }

    public static string GetText(nint handle)
    {
        var length = GetTextLength(handle);
        Span<char> buffer = stackalloc char[length];
        GetText(handle, buffer);
        return new string(buffer);
    }

    public static unsafe void SetText(nint handle, ReadOnlySpan<char> text)
    {
        fixed(char* ptr = text)
        {
            PInvoke.SetWindowText(handle, ptr);
        }
    }

    public static nint GetParent(nint handle)
    {
        return PInvoke.GetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWLP_HWNDPARENT);
    }

    public static void SetParent(nint handle, nint newParent)
    {
        PInvoke.SetParent(handle, newParent);
    }

    public static WINDOW_STYLE GetStyle(nint handle)
    {
        return (WINDOW_STYLE)PInvoke.GetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWL_STYLE); 
    }

    public static void SetStyle(nint handle, WINDOW_STYLE style)
    {
        PInvoke.SetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWL_STYLE, (long)style);
    }

    public static WINDOW_EX_STYLE GetExStyle(nint handle)
    {
        return (WINDOW_EX_STYLE)PInvoke.GetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
    }

    public static void SetExStyle(nint handle, WINDOW_EX_STYLE style)
    {
        PInvoke.SetWindowLongPtr(handle, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, (long)style);
    }

    public static void Show(nint handle, SHOW_WINDOW_CMD showMode)
    {
        PInvoke.ShowWindow(handle, showMode);
    }

    public static void Update(nint handle)
    {
        PInvoke.UpdateWindow(handle);
    }

    public static void Destroy(nint handle)
    {
        PInvoke.DestroyWindow(handle);
    }

    public static unsafe nint Create(ReadOnlySpan<char> className, WINDOW_EX_STYLE exStyle = default, WINDOW_STYLE style = default, ReadOnlySpan<char> windowName = default, int x = PInvoke.CW_USEDEFAULT, int y = PInvoke.CW_USEDEFAULT, int width = PInvoke.CW_USEDEFAULT, int height = PInvoke.CW_USEDEFAULT, nint parentHandle = default)
    {
        fixed (char* classNamePtr = className)
        {
            fixed (char* windowNamePtr = windowName)
            {
                var handle = PInvoke.CreateWindowEx(
                    exStyle,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    x,
                    y,
                    width,
                    height,
                    parentHandle,
                    default,
                    Application.Handle,
                    default
                );
                return handle;
            }
        }
    }
}
