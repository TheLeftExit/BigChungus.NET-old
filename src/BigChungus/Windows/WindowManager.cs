using BigChungus.Drawing;

namespace BigChungus.Windows {
    public class WindowManager {
        [ThreadStatic]
        private static WindowManager current;
        public static WindowManager Current => current ??= new WindowManager() {
            windows = new(),
        };

        private Dictionary<nint, Window> windows;

        public IEnumerable<Window> EnumerateWindows() => windows.Values;

        internal void RegisterWindow(Window window)
        {
            windows.Add(window.Handle, window);
            window.Font = font;
        }

        internal void UnregisterWindow(Window window)
        {
            windows.Remove(window.Handle);
        }

        public Window GetWindow(nint handle)
        {
            return windows.TryGetValue(handle, out var window) ? window : null;
        }

        private Font font;
        public void SetFont(Font newFont)
        {
            font = newFont;
            foreach(var window in EnumerateWindows())
            {
                window.Font = font;
            }
        }
    }
}
