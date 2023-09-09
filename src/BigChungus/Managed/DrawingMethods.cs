using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public static class DrawingMethods
{
    public static unsafe nint CreateFont(ReadOnlySpan<char> name, int size)
    {
        fixed (char* namePtr = name)
        {
            return Gdi32.CreateFont(
                size,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                namePtr
            ).ThrowIf(0);
        }
    }

    public static unsafe void Delete(nint handle)
    {
        Gdi32.DeleteObject(handle).ThrowIfFalse();
    }
}