using BigChungus.Core;
using BigChungus.Common;
using System.Drawing;
using BigChungus.Drawing;

namespace BigChungus.Controls;

public struct CreateParams
{
    public string ClassName { get; set; }
    public WINDOW_STYLE Style { get; set; }
    public WINDOW_EX_STYLE ExStyle { get; set; }
}

public class Control : IWin32Object
{
    // Core

    public Control(Control parent)
    {
        using (WindowCreationScopeManager.Current.CreateScope(this))
        {
            var args = CreateParams;
            if ((args.Style & WINDOW_STYLE.WS_CHILD) == WINDOW_STYLE.WS_CHILD) ArgumentNullException.ThrowIfNull(parent);
            Handle = WindowCommon.Create(args.ClassName, args.ExStyle, args.Style, parentHandle: parent?.Handle ?? 0);
        }
    }

    protected internal virtual nint WndProc(WindowProcedureArgs args)
    {
        return WindowProcedure.Default(args);
    }
    
    protected virtual CreateParams CreateParams
    {
        get
        {
            return new()
            {
                ClassName = WindowClassManager.GetCustomClass(),
                Style = WINDOW_STYLE.WS_VISIBLE | WINDOW_STYLE.WS_CHILD,
                ExStyle = default
            };
        }
    }

    public nint Handle { get; }
    public Control Parent { get; }

    public IDisposable Subclass(SubclassCallback callback)
    {
        return WindowProcedure.Subclass(Handle, callback);
    }

    public static Control FromHandle(nint handle)
    {
        return WindowManager.Current.GetWindow(handle);
    }

    // Common functions

    protected void ShowWindow(SHOW_WINDOW_CMD showMode) => WindowCommon.Show(Handle, showMode);

    public void Hide() => ShowWindow(SHOW_WINDOW_CMD.SW_HIDE);
    public void Show(bool alsoFocus = true) => ShowWindow(alsoFocus ? SHOW_WINDOW_CMD.SW_SHOW : SHOW_WINDOW_CMD.SW_SHOWNA);
    public void Update() => WindowCommon.Update(Handle);

    public bool IsDisposed { get; private set; } = false;

    public void Dispose()
    {
        if (IsDisposed) return;
        WindowCommon.Destroy(Handle);
        IsDisposed = true;
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

    // Font

    private Font controlFont;
    internal void SetDefaultFont(Font newFont)
    {
        if (controlFont == null) WindowCommon.SetFont(Handle, newFont?.Handle ?? default);
    }
    public Font Font
    {
        get => controlFont;
        set
        {
            controlFont = value;
            WindowCommon.SetFont(Handle, value?.Handle ?? WindowManager.Current.DefaultFont?.Handle ?? default);
        }
    }

    public Font RealFont
    {
        get => DrawingObjectManager.Current.GetObject<Font>(WindowCommon.GetFont(Handle));
    }
}

