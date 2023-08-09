using BigChungus.Drawing;

namespace BigChungus.Windows {
    public class WindowManager {
        [ThreadStatic]
        private static WindowManager current;
        public static WindowManager Current => current ??= new WindowManager() {
            windows = new()
        };

        private Dictionary<nint, Window> windows;

        public IEnumerable<Window> EnumerateWindows() => windows.Values;

        public void RegisterWindow(Window window)
        {
            windows.Add(window.Handle, window);
        }

        public void UnregisterWindow(nint handle)
        {
            windows.Remove(handle);
        }

        public Window GetWindow(nint handle)
        {
            return windows.TryGetValue(handle, out var window) ? window : null;
        }

        private Font defaultFont;
        public Font DefaultFont {
            get => defaultFont;
            set {
                defaultFont = value;
                foreach(var window in EnumerateWindows())
                {
                    window.SetDefaultFont(defaultFont);
                }
            }
        }
    }
}
