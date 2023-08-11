using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Drawing;

public abstract class DrawingObject : IWin32Object {
    public DrawingObject()
    {
        Handle = CreateHandle();
        DrawingObjectManager.Current.RegisterObject(this);
    }

    protected abstract nint CreateHandle();

    public nint Handle { get; }

    public bool IsDisposed { get; private set; } = false;

    public virtual void Dispose()
    {
        if(IsDisposed) return;
        DrawingCommon.Delete(Handle);
        DrawingObjectManager.Current.UnregisterObject(this);
        IsDisposed = true;
    }
}
