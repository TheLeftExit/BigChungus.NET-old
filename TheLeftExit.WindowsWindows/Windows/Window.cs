using System.Drawing;

public abstract class Window
{
    [ThreadStatic]
    internal static Dictionary<nint, Window> windows;
    [ThreadStatic]
    protected static Stack<Window> createdWindows;

    private static unsafe void InitCommonControlSex()
    {
        PInvoke.InitCommonControlsEx(new INITCOMMONCONTROLSEX
        {
            dwSize = (uint)sizeof(INITCOMMONCONTROLSEX),
            dwICC = INITCOMMONCONTROLSEX_ICC.ICC_WIN95_CLASSES
        });
    }

    static unsafe Window()
    {
        InitCommonControlSex();
    }

    public Window()
    {
        windows ??= new();
        createdWindows ??= new();

        createdWindows.Push(this);

        Handle = CreateHandle();
        Application.OnWindowCreated(this);
        
        createdWindows.Pop();

        windows.Add(Handle, this);
    }

    protected abstract nint CreateHandle();

    protected unsafe nint CreateWindow(WINDOW_EX_STYLE exStyle, ReadOnlySpan<char> className, ReadOnlySpan<char> windowName, WINDOW_STYLE style, int X, int Y, int nWidth, int nHeight, nint hWndParent)
    {
        fixed (char* classNamePtr = className)
        {
            fixed (char* windowNamePtr = windowName)
            {
                var handle = PInvoke.CreateWindowEx(
                    exStyle,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    X,
                    Y,
                    nWidth,
                    nHeight,
                    hWndParent,
                    default,
                    Application.Handle,
                    default
                );
                if (handle == 0)
                {
                    throw new ApplicationException();
                }
                return handle;
            }
        }
    }

    public nint Handle { get; }

    internal static void Broadcast(WM uMsg, nuint wParam, nint lParam)
    {
        if (windows == null) return;
        foreach (var windowHandle in windows.Keys)
        {
            PInvoke.SendMessage(windowHandle, uMsg, wParam, lParam);
        }
    }

    public void Show(SHOW_WINDOW_CMD showMode = SHOW_WINDOW_CMD.SW_SHOW) => PInvoke.ShowWindow(Handle, showMode);
    public void Update() => PInvoke.UpdateWindow(Handle);
    public void Destroy() => PInvoke.DestroyWindow(Handle);

    public WINDOW_STYLE Style
    {
        get => (WINDOW_STYLE)PInvoke.GetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        set => PInvoke.SetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_STYLE, (long)value);
    }

    public WINDOW_EX_STYLE ExStyle
    {
        get => (WINDOW_EX_STYLE)PInvoke.GetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        set => PInvoke.SetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, (long)value);
    }

    public Window Parent
    {
        get => windows[PInvoke.GetWindowLongPtr(Handle, WINDOW_LONG_PTR_INDEX.GWLP_HWNDPARENT)];
        set => PInvoke.SetParent(Handle, value.Handle);
    }

    public unsafe string Text
    {
        get
        {
            var length = PInvoke.GetWindowTextLength(Handle);
            var buffer = stackalloc char[length];
            PInvoke.GetWindowText(Handle, buffer, length);
            return new string(buffer);
        }
        set
        {
            fixed (char* ptr = value)
            {
                PInvoke.SetWindowText(Handle, ptr);
            }
        }
    }

    public Rectangle Bounds
    {
        get
        {
            PInvoke.GetWindowRect(Handle, out var result);
            return result.ToRectangle();
        }
        set
        {
            PInvoke.MoveWindow(Handle, value.X, value.Y, value.Width, value.Height, true);
        }
    }
}
