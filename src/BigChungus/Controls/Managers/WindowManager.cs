using BigChungus.Drawing;

namespace BigChungus.Controls
{
    internal class WindowManager
    {
        [ThreadStatic]
        private static WindowManager current;
        public static WindowManager Current => current ??= new WindowManager()
        {
            windows = new()
        };

        private Dictionary<nint, Control> windows;

        public IEnumerable<Control> EnumerateWindows() => windows.Values;

        public void RegisterWindow(Control window, nint handle)
        {
            windows.Add(handle, window);
        }

        public void UnregisterWindow(nint handle)
        {
            windows.Remove(handle);
        }

        public Control GetWindow(nint handle)
        {
            return windows.TryGetValue(handle, out var window) ? window : null;
        }

        private Font defaultFont;
        public Font DefaultFont
        {
            get => defaultFont;
            set
            {
                defaultFont = value;
                foreach (var window in EnumerateWindows())
                {
                    window.SetDefaultFont(defaultFont);
                }
            }
        }
    }
}
