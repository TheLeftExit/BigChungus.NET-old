using BigChungus.Managed;

namespace BigChungus.Windows;

public class AnyWindow(string className) : Window
{
    protected override nint CreateHandle()
    {
        return WindowCommon.Create(className);
    }
}

public class ExternalWindow(nint handle) : Window
{
    protected override nint CreateHandle()
    {
        return handle;
    }
}