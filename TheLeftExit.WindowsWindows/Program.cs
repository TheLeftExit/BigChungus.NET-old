using System.Diagnostics;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

var mainWindow = new Form1();

mainWindow.Show();
MessageLoop.Run();

public class Form1 : Form
{
    Window button1;
    Window button2;
    
    public Form1()
    {
        Style = WINDOW_STYLE.WS_OVERLAPPEDWINDOW;
        ExStyle = WINDOW_EX_STYLE.WS_EX_OVERLAPPEDWINDOW;
        Text = "Hello world!";
        Bounds = new System.Drawing.Rectangle(10, 10, 300, 200);
        
        button1 = new Window("Button")
        {
            Text = "Click me!",
            Style = WINDOW_STYLE.WS_CHILD | WINDOW_STYLE.WS_VISIBLE,
            Bounds = new System.Drawing.Rectangle(50, 50, 120, 30),
            Parent = this
        };

        button2 = new Window("Button")
        {
            Text = "Don't click me!",
            Style = WINDOW_STYLE.WS_CHILD | WINDOW_STYLE.WS_VISIBLE,
            Bounds = new System.Drawing.Rectangle(50, 90, 120, 30),
            Parent = this
        };

        SubclassHelper.Subclass(button2, args =>
        {
            if (args.Message == WM.LBUTTONDOWN) Text = "Subclassing successful";
            return args.DefaultWndProc(args.Handle, args.Message, args.WParam, args.LParam);
        });
    }
    
    protected override nint WndProc(nint hWnd, WM uMsg, nuint wParam, nint lParam)
    {
        switch (uMsg)
        {
            case WM.COMMAND:
                if (lParam == button1.Handle)
                {
                    button1.Text = "Good job!";
                }
                else if (lParam == button2.Handle)
                {
                    button2.Text = ">:(";
                }
                return 0;
            case WM.CLOSE:
                Destroy();
                break;
            case WM.DESTROY:
                MessageLoop.Quit();
                break;
        }
        return base.WndProc(hWnd, uMsg, wParam, lParam);
    }
}