using BigChungus.Unmanaged;

namespace BigChungus.Managed;

public interface IWindow
{
    nint Handle { get; init; }
    Window Attributes { get; }
}

public interface ICommonControl<T> : IWindow where T : ICommonControl<T>
{
    static abstract string ClassName { get; }
}

public delegate nint WindowProcedure(Message args);

public static class WindowFactory
{
    public static T CreateControl<T>(nint parent, uint style = 0, uint exStyle = 0) where T : ICommonControl<T>, new()
    {
        return new T
        {
            Handle = Internal.Create(T.ClassName, style | WS.CHILD | WS.VISIBLE, exStyle, parent)
        };
    }

    public static Window CreateUserControl(nint parent, ushort classAtom, uint style = 0, uint exStyle = 0)
    {
        return new Window
        {
            Handle = Internal.Create(classAtom, style | WS.CHILD | WS.VISIBLE, exStyle, parent)
        };
    }

    public static Window CreateUserControl(nint parent, ReadOnlySpan<char> className, uint style = 0, uint exStyle = 0)
    {
        return new Window
        {
            Handle = Internal.Create(className, style | WS.CHILD | WS.VISIBLE, exStyle, parent)
        };
    }

    public static Form CreateForm(ushort classAtom, uint style = 0, uint exStyle = 0)
    {
        return new Form
        {
            Handle = Internal.Create(classAtom, style | WS.OVERLAPPEDWINDOW, exStyle | WS.EX.OVERLAPPEDWINDOW, 0)
        };
    }

    public static Form CreateForm(ReadOnlySpan<char> className, uint style = 0, uint exStyle = 0)
    {
        return new Form
        {
            Handle = Internal.Create(className, style | WS.OVERLAPPEDWINDOW, exStyle | WS.EX.OVERLAPPEDWINDOW, 0)
        };
    }

    public static ushort RegisterClass(ReadOnlySpan<char> className, WindowProcedure procedure)
    {
        return Internal.Register(className, (hWnd, msg, wParam, lParam) => procedure(new(hWnd, msg, wParam, lParam)));
    }

    public static void UnregisterClass(ReadOnlySpan<char> className)
    {
        Internal.Unregister(className);
    }

    public static void UnregisterClass(ushort atom)
    {
        Internal.Unregister(atom);
    }
}