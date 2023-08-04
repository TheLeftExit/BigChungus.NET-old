using BigChungus.Managed;

namespace BigChungus.Windows {
    public class WindowManager {
        [ThreadStatic]
        private static WindowManager current;
        public static WindowManager Current => current ??= new WindowManager() {
            windows = new(),
            windowCreationScopeStack = new()
        };

        private Dictionary<nint, Window> windows;
        private Stack<Window> windowCreationScopeStack;

        public IEnumerable<Window> EnumerateWindows() => windows.Values;

        internal void PreRegisterWindow(Window window)
        {
            windowCreationScopeStack.Push(window);
        }

        internal void RegisterWindow(Window window)
        {
            windowCreationScopeStack.Pop();
            windows.Add(window.Handle, window);
            if (font != null) WindowCommon.SetFont(window.Handle, font.Handle);
        }

        internal void UnregisterWindow(Window window)
        {
            windows.Remove(window.Handle);
        }

        public Window GetWindow(nint handle)
        {
            return windows.TryGetValue(handle, out var window) ? window : null;
        }

        internal Window GetCreatedWindow()
        {
            return windowCreationScopeStack.Peek();
        }

        private FontHandle font;
        public void SetFont(ReadOnlySpan<char> name, int size)
        {
            font?.Dispose();
            font = new(name, size);
            foreach(var window in EnumerateWindows())
            {
                WindowCommon.SetFont(window.Handle, font.Handle);
            }
        }
    }
}
