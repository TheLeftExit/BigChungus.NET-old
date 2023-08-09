using BigChungus.Core;
using BigChungus.Windows;

namespace BigChungus.Drawing;

public class Font(string name, int size) : DrawingObject {

    protected override nint CreateHandle()
    {
        return DrawingCommon.CreateFont(name, size);
    }

    public override void Dispose()
    {
        if(WindowManager.Current.DefaultFont == this)
        {
            WindowManager.Current.DefaultFont = null;
        }
        foreach(var window in WindowManager.Current.EnumerateWindows())
        {
            if(window.Font.Handle == Handle)
            {
                window.Font = null;
            }
        }
        base.Dispose();
    }
}
