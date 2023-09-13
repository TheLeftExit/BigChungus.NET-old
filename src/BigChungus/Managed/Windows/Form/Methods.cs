using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public readonly partial record struct Form(nint Handle) : IWindow
{
    public Window Attributes => new Window(Handle);
}