using BigChungus.Core;
using System.Drawing;
using BigChungus.Common;
using BigChungus.Drawing;

namespace BigChungus.Windows;

public abstract class Window {
    public Window()
    {
        Handle = CreateHandle();
        WindowManager.Current.RegisterWindow(this);
    }

    protected abstract nint CreateHandle();

    public nint Handle { get; }

    public void Show(SHOW_WINDOW_CMD showMode = SHOW_WINDOW_CMD.SW_SHOW) => WindowCommon.Show(Handle, showMode);
    public void Update() => WindowCommon.Update(Handle);

    public void Dispose()
    {
        WindowCommon.Destroy(Handle);
        WindowManager.Current.UnregisterWindow(this);
    }

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
        get => WindowManager.Current.GetWindow(WindowCommon.GetParent(Handle));
        set => WindowCommon.SetParent(Handle, value.Handle);
    }

    public string Text
    {
        get => WindowCommon.GetText(Handle);
        set => WindowCommon.SetText(Handle, value);
    }

    public Rectangle Bounds
    {
        get => WindowCommon.GetBounds(Handle);
        set => WindowCommon.SetBounds(Handle, value);
    }

    public Font Font {
        get => DrawingObjectManager.Current.GetObject<Font>(WindowCommon.GetFont(Handle));
        set => WindowCommon.SetFont(Handle, value.Handle);
    }

    public IDisposable Subclass(SubclassCallback callback)
    {
        return WindowProcedure.Subclass(Handle, callback);
    }
}

