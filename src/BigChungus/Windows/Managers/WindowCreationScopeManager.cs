namespace BigChungus.Windows; 
internal class WindowCreationScopeManager
{
    [ThreadStatic]
    private static WindowCreationScopeManager current;
    public static WindowCreationScopeManager Current => current ??= new WindowCreationScopeManager()
    {
        creationScopes = new()
    };

    private Stack<Window> creationScopes { get; init; }

    public Window Top => creationScopes.Peek();

    public WindowCreationScope CreateScope(Window window)
    {
        creationScopes.Push(window);
        return new WindowCreationScope();
    }

    internal ref struct WindowCreationScope {
        public void Dispose()
        {
            var window = Current.creationScopes.Pop();
            // WindowManager.Current.RegisterWindow(window);
        }
    }
}


