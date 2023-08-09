using BigChungus.Core;
using System.Drawing;
using BigChungus.Common;
using BigChungus.Drawing;

namespace BigChungus.Windows;

public abstract class Window : IDisposable {
    public Window()
    {
        using (WindowCreationScopeManager.Current.CreateScope(this))
        {
            Handle = CreateHandle();
        }
        WindowManager.Current.RegisterWindow(this);
        SetDefaultFont(WindowManager.Current.DefaultFont);
    }

    protected internal virtual nint WndProc(WindowProcedureArgs args)
    {
        return WindowProcedure.Default(args);
    }

    protected abstract nint CreateHandle();

    public nint Handle { get; set; }

    public void Show(SHOW_WINDOW_CMD showMode = SHOW_WINDOW_CMD.SW_SHOW) => WindowCommon.Show(Handle, showMode);
    public void Update() => WindowCommon.Update(Handle);
    public void Dispose() => WindowCommon.Destroy(Handle);

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
        set => WindowCommon.SetParent(Handle, value?.Handle ?? default);
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

    private Font controlFont;
    internal void SetDefaultFont(Font newFont)
    {
        if (controlFont == null) WindowCommon.SetFont(Handle, newFont?.Handle ?? default);
    }
    public Font Font {
        get => controlFont;
        set {
            controlFont = value;
            WindowCommon.SetFont(Handle, value?.Handle ?? WindowManager.Current.DefaultFont?.Handle ?? default);
        }
    }

    public Font RealFont {
        get => DrawingObjectManager.Current.GetObject<Font>(WindowCommon.GetFont(Handle));
    }

    public IDisposable Subclass(SubclassCallback callback)
    {
        return WindowProcedure.Subclass(Handle, callback);
    }

    public static Window FromHandle(nint handle)
    {
        return WindowManager.Current.GetWindow(handle);
    }
}

