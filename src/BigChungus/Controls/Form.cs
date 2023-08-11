using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Controls;

public class Form : Control
{
    public Form() : base(null) { }

    protected override CreateParams CreateParams
    {
        get
        {
            return base.CreateParams with
            {
                Style = WINDOW_STYLE.WS_OVERLAPPEDWINDOW,
                ExStyle = WINDOW_EX_STYLE.WS_EX_OVERLAPPEDWINDOW
            };
        }
    }

    public void Maximize() => ShowWindow(SHOW_WINDOW_CMD.SW_SHOWMAXIMIZED);
    public void Minimize(bool alsoFocus = false) => ShowWindow(alsoFocus ? SHOW_WINDOW_CMD.SW_SHOWMINNOACTIVE : SHOW_WINDOW_CMD.SW_SHOWMINIMIZED);
    public void Restore() => ShowWindow(SHOW_WINDOW_CMD.SW_RESTORE);
    public void Close() => WindowCommon.SendMessage(new(Handle, WM.CLOSE, 0, 0));

    public FormCloseBehavior CloseBehavior { get; set; } = FormCloseBehavior.DestroyForm;

    protected internal override nint WndProc(WindowProcedureArgs args)
    {
        if(args.Message == WM.CLOSE && CloseBehavior != FormCloseBehavior.HideForm)
        {
            Dispose();
        }
        if(args.Message == WM.DESTROY && CloseBehavior == FormCloseBehavior.ExitApplication)
        {
            Application.Quit();
        }
        return base.WndProc(args);
    }
}

public enum FormCloseBehavior
{
    HideForm,
    DestroyForm,
    ExitApplication
}