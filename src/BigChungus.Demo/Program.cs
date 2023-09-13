using BigChungus.Managed;
using BigChungus.Unmanaged;
using System.Diagnostics;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Console.WriteLine("Hello world!");

ApplicationMethods.LoadCommonControls();
ApplicationMethods.EnableVisualStyles();

var newAtom = WindowFactory.RegisterClass("DummyWindow", args =>
{
    Debug.WriteLine(args.Code);
    return args.Default();
});
var newForm = WindowFactory.CreateForm(newAtom);
newForm.Attributes.Destroy();
WindowFactory.UnregisterClass(newAtom);

var atom = WindowFactory.RegisterClass("BigChungusWindow", args =>
{
    if (Window.TryClose(args))
    {
        ApplicationMethods.PostQuit();
    }
    if (Button.TryClicked(args, out var header))
    {
        Debug.WriteLine(new Button(header.Handle).Attributes.GetText());
    }
    return args.Default();
});

var mainForm = WindowFactory.CreateForm(atom);
var button = WindowFactory.CreateControl<Button>(mainForm.Handle, BS.COMMANDLINK);

button.Attributes.SetBounds(new System.Drawing.Rectangle(10, 10, 300, 100));
button.Attributes.SetText("Button!");
button.SetElevationRequiredState(true);
button.SetNote("Note!");

User32.ShowWindow(mainForm.Handle, SW.SHOWNORMAL);

ApplicationMethods.RunMessageLoop();
