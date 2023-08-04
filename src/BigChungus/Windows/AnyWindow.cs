using BigChungus.Utils;

namespace BigChungus.Windows;

public class AnyWindow(string className) : Window
{
    protected override nint CreateHandle()
    {
        return WindowCommon.Create(className);
    }
}