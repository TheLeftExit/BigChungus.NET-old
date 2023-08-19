using BigChungus.Core;
using BigChungus.Common;
using System.Drawing;
using BigChungus.Drawing;

namespace BigChungus.Controls;

public abstract class Control(Control parent) : Win32Object
{
    public struct WindowArgs
    {
        public string ClassName { get; set; }
        public WINDOW_STYLE Style { get; set; }
        public WINDOW_EX_STYLE ExStyle { get; set; }
    }

    protected override nint CreateHandle()
    {
        var args = CreateWindowArgs();

        if (string.IsNullOrEmpty(args.ClassName))
        {
            throw new ArgumentNullException(nameof(args.ClassName), $"{nameof(Control)} descendants must override {nameof(CreateWindowArgs)} and specify {nameof(WindowArgs.ClassName)}.");
        }
        if(args.Style.HasFlag(WINDOW_STYLE.WS_CHILD) && parent == null)
        {
            throw new ArgumentNullException(nameof(parent));
        }

        var handle = WindowCommon.Create(args.ClassName, args.ExStyle, args.Style, parentHandle: parent?.Handle ?? 0);
        SetDefaultFont(WindowManager.Current.DefaultFont, handle);

        WindowManager.Current.RegisterWindow(this, handle);
        WindowProcedure.Subclass(handle, (args, defWndProc) =>
        {
            var returnValue = defWndProc(args);
            if (args.Message == WM.NCDESTROY)
            {
                WindowManager.Current.UnregisterWindow(handle);
                IsDisposed = true;
            }
            return returnValue;
        });

        return handle;
    }

    protected virtual WindowArgs CreateWindowArgs()
    {
        return new()
        {
            Style = WINDOW_STYLE.WS_CHILD | WINDOW_STYLE.WS_VISIBLE
        };
    }

    public Control Parent => parent;

    protected override void DestroyHandle()
    {
        WindowCommon.Destroy(Handle);
        WindowManager.Current.UnregisterWindow(Handle);
    }

    public static Control FromHandle(nint handle)
    {
        return WindowManager.Current.GetWindow(handle);
    }

    public void Hide() => WindowCommon.Show(Handle, SHOW_WINDOW_CMD.SW_HIDE);
    public void Show(bool alsoFocus = true) => WindowCommon.Show(Handle, alsoFocus ? SHOW_WINDOW_CMD.SW_SHOW : SHOW_WINDOW_CMD.SW_SHOWNA);
    public void Update() => WindowCommon.Update(Handle);

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

    #region Font
    private Font controlFont;
    internal void SetDefaultFont(Font newFont, nint? windowHandle = null)
    {
        if (controlFont == null) WindowCommon.SetFont(windowHandle ?? Handle, newFont?.Handle ?? default);
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
    #endregion
}

