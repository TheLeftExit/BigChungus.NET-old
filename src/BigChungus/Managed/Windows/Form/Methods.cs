using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public readonly partial record struct Form(nint Handle) : IWindow
{
    public Window Attributes => new Window(Handle);
    public Form(ReadOnlySpan<char> className) : this(Internal.Create(className, WS.OVERLAPPEDWINDOW, WS.EX.OVERLAPPEDWINDOW, default)) { }
}