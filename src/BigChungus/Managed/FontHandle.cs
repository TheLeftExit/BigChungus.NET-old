using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public class FontHandle : IDisposable
{
    public nint Handle { get; }
    public unsafe FontHandle(ReadOnlySpan<char> name, int size)
    {
        fixed (char* namePtr = name)
        {
            Handle = PInvoke.CreateFont(
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
            if (Handle == default) throw new ApplicationException();
        }
    }

    public void Dispose()
    {
        PInvoke.DeleteObject(Handle);
    }
}