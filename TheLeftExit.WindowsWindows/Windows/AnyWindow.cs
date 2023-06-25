public class AnyWindow(string className) : Window
{
    protected override nint CreateHandle()
    {
        return CreateWindow(
            default,
            className,
            default,
            default,
            PInvoke.CW_USEDEFAULT,
            PInvoke.CW_USEDEFAULT,
            PInvoke.CW_USEDEFAULT,
            PInvoke.CW_USEDEFAULT,
            default
        );
    }
}