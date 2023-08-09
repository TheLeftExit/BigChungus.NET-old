using BigChungus.Core;

namespace BigChungus.Drawing;

public abstract class DrawingObject : IDisposable {
    public DrawingObject()
    {
        Handle = CreateHandle();
        DrawingObjectManager.Current.RegisterObject(this);
    }

    protected abstract nint CreateHandle();

    public nint Handle { get; }

    public virtual void Dispose()
    {
        DrawingCommon.Delete(Handle);
        DrawingObjectManager.Current.UnregisterObject(this);
    }
}
