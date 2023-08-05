using BigChungus.Core.Interop;

namespace BigChungus.Core;

public static class DrawingCommon
{
    public static unsafe nint CreateFont(ReadOnlySpan<char> name, int size)
    {
        fixed (char* namePtr = name)
        {
            var returnValue = PInvoke.CreateFont(
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
            ReturnValueException.ThrowIf(nameof(PInvoke.CreateFont), returnValue is 0);
            return returnValue;
        }
    }

    public static unsafe void Delete(nint handle)
    {
        var returnValue = PInvoke.DeleteObject(handle);
        ReturnValueException.ThrowIf(nameof(PInvoke.DeleteObject), returnValue is false);
    }
}