namespace BigChungus.Windows {
    internal class OverlappedWindowManager
    {
        [ThreadStatic]
        private static OverlappedWindowManager current;
        public static OverlappedWindowManager Current => current ??= new OverlappedWindowManager()
        {
            creationScopes = new()
        };

        private Stack<OverlappedWindow> creationScopes { get; init; }

        public OverlappedWindow Top => creationScopes.Peek();

        public OverlappedWindowCreationScope CreateScope(OverlappedWindow window)
        {
            creationScopes.Push(window);
            return new OverlappedWindowCreationScope();
        }

        internal ref struct OverlappedWindowCreationScope {
            public void Dispose()
            {
                Current.creationScopes.Pop();
            }
        }
    }

    
}
