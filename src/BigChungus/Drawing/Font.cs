using BigChungus.Core;
using BigChungus.Controls;

namespace BigChungus.Drawing;

public class Font(string name, int size) : DrawingObject {

    protected override nint CreateHandleBase()
    {
        return DrawingCommon.CreateFont(name, size);
    }

    protected override void DestroyHandle()
    {
        if (WindowManager.Current.DefaultFont == this)
        {
            WindowManager.Current.DefaultFont = null;
        }
        foreach (var window in WindowManager.Current.EnumerateWindows())
        {
            if (window.Font == this)
            {
                window.Font = null;
            }
        }
        base.DestroyHandle();
    }
}
