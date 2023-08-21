using BigChungus.Managed;
using BigChungus.Unmanaged.Libraries;
using BigChungus.Unmanaged.Messages;
using BigChungus.Unmanaged.WindowStyles;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Console.WriteLine("Hello world!");

ApplicationMethods.LoadCommonControls();
ApplicationMethods.EnableVisualStyles();

InternalMethods.Register("BigChungusWindow", (hwnd, msg, wParam, lParam) =>
{
    if(msg == WM.CLOSE)
    {
        ApplicationMethods.PostQuit();
    }
    return User32.DefWindowProc(hwnd, msg, wParam, lParam);
});


ThreadStart applicationFunction = () =>
{
    var mainForm = InternalMethods.Create("BigChungusWindow", WS.OVERLAPPEDWINDOW, 0x00040300, default);
    var button = InternalMethods.Create("Button", WS.CHILD | WS.VISIBLE | CCS.VERT | BS.COMMANDLINK, default, mainForm);
    WindowMethods.SetBounds(button, new System.Drawing.Rectangle(10, 10, 300, 100));
    WindowMethods.SetText(button, "Button!");
    ButtonMethods.SetElevationRequiredState(button, true);
    ButtonMethods.SetNote(button, "Note!");

    User32.ShowWindow(mainForm, BigChungus.Unmanaged.SHOW_WINDOW_CMD.SW_SHOW);

    ApplicationMethods.RunMessageLoop();
};

new Thread(applicationFunction).Start();
applicationFunction();