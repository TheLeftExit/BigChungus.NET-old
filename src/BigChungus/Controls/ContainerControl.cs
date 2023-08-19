using BigChungus.Common;
using BigChungus.Core;

namespace BigChungus.Controls;

public class ContainerControl(Control parent) : Control(parent)
{
    protected override nint CreateHandle()
    {
        using (ContainerControlManager.CreateScope(this))
        {
            return base.CreateHandle();
        }
    }

    protected override WindowArgs CreateWindowArgs()
    {
        return base.CreateWindowArgs() with { ClassName = ContainerControlManager.ClassName };
    }

    protected internal virtual nint WndProc(WindowProcedureArgs args)
    {
        if(WmCommandArgs.TryDecode(args, out var wmCommandArgs) && wmCommandArgs.Code == ButtonConstants.BN_CLICKED)
        {
            ((Button)FromHandle(wmCommandArgs.Handle)).RaiseClicked();
        }

        return WindowProcedure.Default(args);
    }
}

internal class ContainerControlManager
{
    [ThreadStatic]
    private static ContainerControlManager current;
    private Stack<ContainerControl> creationScopes { get; init; }

    public const string ClassName = "BigChungusWindow";
    public static ContainerControlManager Current => current ??= new ContainerControlManager() { creationScopes = new() };
    public static ContainerControl Top => Current.creationScopes.Peek();
    

    public static WindowCreationScope CreateScope(ContainerControl window)
    {
        EnsureClassRegistered();
        Current.creationScopes.Push(window);
        return new WindowCreationScope();
    }

    public ref struct WindowCreationScope
    {
        public void Dispose()
        {
            Current.creationScopes.Pop();
        }
    }

    private static bool classRegistered = false;
    private static object classRegistrationLock = new object();
    private static IDisposable classContext;
    private static void EnsureClassRegistered()
    {
        if (classRegistered) return;
        lock(classRegistrationLock)
        {
            if (classRegistered) return;
            classContext = WindowClass.Register(ClassName, HandleWindowMessage);
        }
    }

    private static nint HandleWindowMessage(WindowProcedureArgs args)
    {
        var targetWindow = (ContainerControl)WindowManager.Current.GetWindow(args.Handle) ?? Top;
        return targetWindow.WndProc(args);
    }
}