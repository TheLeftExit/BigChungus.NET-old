using BigChungus.Core;

namespace BigChungus.Drawing;

public class Font(string name, int size) : DrawingObject {

    protected override nint CreateHandle()
    {
        return DrawingCommon.CreateFont(name, size);
    }
}
