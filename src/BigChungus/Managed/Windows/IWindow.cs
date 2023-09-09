namespace BigChungus.Managed;

public interface IWindow
{
    nint Handle { get; }
    Window Attributes { get; }
}