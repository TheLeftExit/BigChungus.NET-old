using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Windows;

public class Form : Window
{
    protected sealed override nint CreateHandle()
    {
        var className = WindowClassManager.GetCustomClass();
        return WindowCommon.Create(className, WINDOW_EX_STYLE.WS_EX_OVERLAPPEDWINDOW, WINDOW_STYLE.WS_OVERLAPPEDWINDOW);
    }
}
