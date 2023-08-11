namespace BigChungus.Controls;
internal class WindowCreationScopeManager
{
    [ThreadStatic]
    private static WindowCreationScopeManager current;
    public static WindowCreationScopeManager Current => current ??= new WindowCreationScopeManager()
    {
        creationScopes = new()
    };

    private Stack<Control> creationScopes { get; init; }

    public Control Top => creationScopes.Peek();

    public WindowCreationScope CreateScope(Control window)
    {
        creationScopes.Push(window);
        return new WindowCreationScope();
    }

    public ref struct WindowCreationScope
    {
        public void Dispose()
        {
            Current.creationScopes.Pop();
        }
    }
}


