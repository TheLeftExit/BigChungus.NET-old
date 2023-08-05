namespace BigChungus.Drawing;

public class DrawingObjectManager
{
    [ThreadStatic]
    private static DrawingObjectManager current;
    public static DrawingObjectManager Current => current ??= new DrawingObjectManager() { objects = new() };

    private Dictionary<nint, DrawingObject> objects;

    internal void RegisterObject(DrawingObject drawingObject)
    {
        objects.Add(drawingObject.Handle, drawingObject);
    }

    internal void UnregisterObject(DrawingObject drawingObject)
    {
        objects.Remove(drawingObject.Handle);
    }

    public T GetObject<T>(nint handle) where T : DrawingObject
    {
        return objects.TryGetValue(handle, out var window) ? (T)window : null;
    }
}