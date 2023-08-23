using BigChungus.Managed;
using BigChungus.Unmanaged;
using BigChungus.Unmanaged.Libraries;
using BigChungus.Unmanaged.Messages;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Console.WriteLine("Hello world!");

ApplicationMethods.LoadCommonControls();
ApplicationMethods.EnableVisualStyles();

Internal.Register("BigChungusWindow", (hwnd, msg, wParam, lParam) =>
{
    if(msg == WM.CLOSE)
    {
        ApplicationMethods.PostQuit();
    }
    return User32.DefWindowProc(hwnd, msg, wParam, lParam);
});

var mainFormHandle = new FormArgs("BigChungusWindow").Create();
var buttonHandle = new ButtonArgs(ButtonKind.CommandLink).Create(mainFormHandle);

var button = new Button(buttonHandle);

button.AsWindow().SetBounds(new System.Drawing.Rectangle(10, 10, 300, 100));
button.AsWindow().SetText("Button!");
button.SetElevationRequiredState(true);
button.SetNote("Note!");

User32.ShowWindow(mainFormHandle, SHOW_WINDOW_CMD.SW_SHOW);

ApplicationMethods.RunMessageLoop();