namespace BigChungus.Drawing;

public class DrawingObjectManager
{
    [ThreadStatic]
    private static DrawingObjectManager current;
    public static DrawingObjectManager Current => current ??= new DrawingObjectManager() { objects = new() };

    private Dictionary<nint, DrawingObject> objects;

    internal void RegisterObject(DrawingObject drawingObject, nint handle)
    {
        objects.Add(handle, drawingObject);
    }

    internal void UnregisterObject(nint handle)
    {
        objects.Remove(handle);
    }

    public T GetObject<T>(nint handle) where T : DrawingObject
    {
        return objects.TryGetValue(handle, out var window) ? (T)window : null;
    }
}