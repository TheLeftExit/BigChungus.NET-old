using BigChungus.Windows;
using BigChungus.Common;
using BigChungus.Drawing;
using BigChungus.Core;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Application.DefaultFont = new Font("Segoe UI", -12);

var mainWindow = new Form1();
mainWindow.Show();
Application.Run();

public class Form1 : Form {
    Window button1;
    Window button2;
    IDisposable button1Subclass;

    public Form1()
    {
        Text = "Hello world!";
        Bounds = new System.Drawing.Rectangle(10, 10, 300, 200);

        button1 = new Button
        {
            Text = "Click me!",
            Style = WINDOW_STYLE.WS_CHILD | WINDOW_STYLE.WS_VISIBLE,
            Bounds = new System.Drawing.Rectangle(50, 50, 120, 30),
            Parent = this,
            Font = new Font("Cascadia Mono", -12)
        };

        button2 = new Button
        {
            Text = "Don't click me!",
            Style = WINDOW_STYLE.WS_CHILD | WINDOW_STYLE.WS_VISIBLE,
            Bounds = new System.Drawing.Rectangle(50, 90, 120, 30),
            Parent = this
        };

        button1Subclass = button2.Subclass((args, defWndProc) =>
        {
            if (args.Message == WM.LBUTTONUP)
            {
                Application.DefaultFont = new Font("Comic Sans MS", -12);
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
                Dispose();
                break;
            case WM.DESTROY:
                Application.Quit();
                break;
        }
        return base.WndProc(args);
    }
}