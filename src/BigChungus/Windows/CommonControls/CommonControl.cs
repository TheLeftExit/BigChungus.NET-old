using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Windows;

public abstract class CommonControl : Window
{
    protected abstract string ClassName { get; }

    protected sealed override nint CreateHandle()
    {
        (var className, defaultCallback) = WindowClassManager.GetSuperclass(ClassName);
        return WindowCommon.Create(className);
    }

    protected internal override nint WndProc(WindowProcedureArgs args)
    {
        return defaultCallback(args);
    }

    private WindowCallback defaultCallback;
}
