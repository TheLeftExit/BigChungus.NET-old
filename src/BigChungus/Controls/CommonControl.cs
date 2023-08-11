using BigChungus.Common;

namespace BigChungus.Controls;

public class CommonControl(string className, Control parent) : Control(parent)
{
    protected override CreateParams CreateParams
    {
        get
        {
            var args = base.CreateParams;
            (args.ClassName, defaultCallback) = WindowClassManager.GetSuperclass(className);
            return args;
        }
    }

    protected internal override nint WndProc(WindowProcedureArgs args)
    {
        return defaultCallback(args);
    }

    private WindowCallback defaultCallback;
}
