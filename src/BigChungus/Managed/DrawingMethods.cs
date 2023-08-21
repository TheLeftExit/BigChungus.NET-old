using BigChungus.Unmanaged.Libraries;

namespace BigChungus.Managed;

public static class DrawingMethods
{
    public static unsafe nint CreateFont(ReadOnlySpan<char> name, int size)
    {
        fixed (char* namePtr = name)
        {
            var returnValue = Gdi32.CreateFont(
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
            );
            ReturnValueException.ThrowIf(nameof(Gdi32.CreateFont), returnValue is 0);
            return returnValue;
        }
    }

    public static unsafe void Delete(nint handle)
    {
        var returnValue = Gdi32.DeleteObject(handle);
        ReturnValueException.ThrowIf(nameof(Gdi32.DeleteObject), returnValue is false);
    }
}