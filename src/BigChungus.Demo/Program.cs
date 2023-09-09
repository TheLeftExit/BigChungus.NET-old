using BigChungus.Managed;
using BigChungus.Unmanaged;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Console.WriteLine("Hello world!");

ApplicationMethods.LoadCommonControls();
ApplicationMethods.EnableVisualStyles();

Internal.Register("BigChungusWindow", (hwnd, msg, wParam, lParam) =>
{
    if (msg == WM.CLOSE)
    {
        ApplicationMethods.PostQuit();
    }
    return User32.DefWindowProc(hwnd, msg, wParam, lParam);
});

var mainForm = new Form("BigChungusWindow");
var button = new Button(mainForm.Handle, BS.COMMANDLINK);

button.Attributes.SetBounds(new System.Drawing.Rectangle(10, 10, 300, 100));
button.Attributes.SetText("Button!");
button.SetElevationRequiredState(true);
button.SetNote("Note!");

User32.ShowWindow(mainForm.Handle, SW.SHOW);

ApplicationMethods.RunMessageLoop();
