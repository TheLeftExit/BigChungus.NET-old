using BigChungus.Interop;
using BigChungus.Utils;
using System.Drawing;

namespace BigChungus.Windows;

public abstract class Window
{
    [ThreadStatic]
    internal static Dictionary<nint, Window> windows;
    [ThreadStatic]
    protected static Stack<Window> createdWindows;

    private static unsafe void InitCommonControlSex()
    {
        PInvoke.InitCommonControlsEx(new INITCOMMONCONTROLSEX
        {
            dwSize = (uint)sizeof(INITCOMMONCONTROLSEX),
            dwICC = INITCOMMONCONTROLSEX_ICC.ICC_WIN95_CLASSES
        });
    }

    static unsafe Window()
    {
        InitCommonControlSex();
    }

    public Window()
    {
        windows ??= new();
        createdWindows ??= new();

        createdWindows.Push(this);

        Handle = CreateHandle();
        Application.OnWindowCreated(this);
        
        createdWindows.Pop();

        windows.Add(Handle, this);
    }

    protected abstract nint CreateHandle();

    public nint Handle { get; }

    internal static void Broadcast(WM uMsg, nuint wParam, nint lParam)
    {
        if (windows == null) return;
        foreach (var windowHandle in windows.Keys)
        {
            PInvoke.SendMessage(windowHandle, uMsg, wParam, lParam);
        }
    }

    public void Show(SHOW_WINDOW_CMD showMode = SHOW_WINDOW_CMD.SW_SHOW) => WindowCommon.Show(Handle, showMode);
    public void Update() => WindowCommon.Update(Handle);
    public void Destroy() => WindowCommon.Destroy(Handle);

    public WINDOW_STYLE Style
    {
        get => WindowCommon.GetStyle(Handle);
        set => WindowCommon.SetStyle(Handle, value);
    }

    public WINDOW_EX_STYLE ExStyle
    {
        get => WindowCommon.GetExStyle(Handle);
        set => WindowCommon.SetExStyle(Handle, value);
    }

    public Window Parent
    {
        get => windows[WindowCommon.GetParent(Handle)];
        set => WindowCommon.SetParent(Handle, value.Handle);
    }

    public unsafe string Text
    {
        get => WindowCommon.GetText(Handle);
        set => WindowCommon.SetText(Handle, value);
    }

    public Rectangle Bounds
    {
        get => WindowCommon.GetBounds(Handle);
        set => WindowCommon.SetBounds(Handle, value);
    }
}

