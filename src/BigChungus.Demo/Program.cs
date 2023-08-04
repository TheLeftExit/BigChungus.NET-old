using BigChungus.Interop;
using BigChungus.Utils;
using BigChungus.Windows;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Application.EnableVisualStyles();
Application.SetFont("Segoe UI", -12);

var mainWindow = new Form1();
mainWindow.Show();
Application.Run();

public class Form1 : Form {
    Window button1;
    Window button2;
    Subclass button1Subclass;

    public Form1()
    {
        Text = "Hello world!";
        Bounds = new System.Drawing.Rectangle(10, 10, 300, 200);

        button1 = new AnyWindow("BUTTON")
        {
            Text = "Click me!",
            Style = WINDOW_STYLE.WS_CHILD | WINDOW_STYLE.WS_VISIBLE,
            Bounds = new System.Drawing.Rectangle(50, 50, 120, 30),
            Parent = this
        };

        button2 = new AnyWindow("BUTTON")
        {
            Text = "Don't click me!",
            Style = WINDOW_STYLE.WS_CHILD | WINDOW_STYLE.WS_VISIBLE,
            Bounds = new System.Drawing.Rectangle(50, 90, 120, 30),
            Parent = this
        };

        button1Subclass = WindowProcedure.Subclass(button2.Handle, (args, defWndProc) =>
        {
            if (args.Message == WM.LBUTTONUP)
            {
                Application.SetFont("Comic Sans MS", -12);
                Text = "Subclassing successful";
            }
            return defWndProc(args);
        });
    }

    protected override nint WndProc(WindowProcedureArgs args)
    {
        switch (args.Message)
        {
            case WM.COMMAND:
                if (args.LParam == button1.Handle)
                {
                    button1.Text = "Good job!";
                } else if (args.LParam == button2.Handle)
                {
                    button2.Text = ">:(";
                }
                return 0;
            case WM.CLOSE:
                Destroy();
                break;
            case WM.DESTROY:
                Application.Quit();
                break;
        }
        return base.WndProc(args);
    }
}