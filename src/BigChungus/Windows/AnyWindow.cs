using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Windows;

public sealed class AnyWindow(string className) : Window
{
    protected override nint CreateHandle()
    {
        return WindowCommon.Create(className);
    }
}