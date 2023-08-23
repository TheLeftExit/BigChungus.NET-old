using BigChungus.Unmanaged.WindowStyles;

namespace BigChungus.Managed;

public struct FormArgs
{
    public FormArgs(string className)
    {
        ClassName = className;
    }
    public string ClassName { get; set; }

    public readonly nint Create()
    {
        return Internal.Create(ClassName, WS.OVERLAPPEDWINDOW, WS.EX.OVERLAPPEDWINDOW, default);
    }
}
