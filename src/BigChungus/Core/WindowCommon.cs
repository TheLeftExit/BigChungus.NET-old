using BigChungus.Common;
using BigChungus.Core.Interop;
using System.Drawing;

namespace BigChungus.Core;

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
        ReturnValueException.ThrowIf(nameof(PInvoke.GetWindowRect), returnValue is false);
        return result.ToRectangle();
    }

    public static void SetBounds(nint handle, Rectangle newBounds, bool repaint = true)
    {
        var returnValue = PInvoke.MoveWindow(handle, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height, repaint);
        ReturnValueException.ThrowIf(nameof(PInvoke.MoveWindow), returnValue is false);
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
            var returnValue = PInvoke.SetWindowText(handle, ptr);
            ReturnValueException.ThrowIf(nameof(PInvoke.SetWindowText), returnValue is false);
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
        var returnValue = PInvoke.UpdateWindow(handle);
        ReturnValueException.ThrowIf(nameof(PInvoke.UpdateWindow), returnValue is false);
    }

    public static void Destroy(nint handle)
    {
        var returnValue = PInvoke.DestroyWindow(handle);
        ReturnValueException.ThrowIf(nameof(PInvoke.DestroyWindow), returnValue is false);
    }

    public static unsafe nint Create(ReadOnlySpan<char> className, WINDOW_EX_STYLE exStyle = default, WINDOW_STYLE style = default, ReadOnlySpan<char> windowName = default, int x = PInvoke.CW_USEDEFAULT, int y = PInvoke.CW_USEDEFAULT, int width = PInvoke.CW_USEDEFAULT, int height = PInvoke.CW_USEDEFAULT, nint parentHandle = default)
    {
        fixed (char* classNamePtr = className)
        {
            fixed (char* windowNamePtr = windowName)
            {
                var returnValue = PInvoke.CreateWindowEx(
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
                    ApplicationCommon.Handle,
                    default
                );
                ReturnValueException.ThrowIf(nameof(PInvoke.CreateWindowEx), returnValue is 0);
                return returnValue;
            }
        }
    }
}
