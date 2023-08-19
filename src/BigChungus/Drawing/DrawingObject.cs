using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Drawing;

public abstract class DrawingObject : Win32Object {
    protected override nint CreateHandle()
    {
        var handle = CreateHandleBase();
        DrawingObjectManager.Current.RegisterObject(this, handle);
        return handle;
    }

    protected abstract nint CreateHandleBase();

    protected override void DestroyHandle()
    {
        DrawingCommon.Delete(Handle);
        DrawingObjectManager.Current.UnregisterObject(Handle);
    }
}
