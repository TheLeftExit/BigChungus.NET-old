[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Application.EnableVisualStyles();
Application.SetFont("Segoe UI", -12);

var mainWindow = new Form1();
mainWindow.Show();
Application.Run();

public class Form1 : Form
{
    Window button1;
    Window button2;
    
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

        SubclassHelper.Subclass(button2, args =>
        {
            if (args.Message == WM.LBUTTONUP)
            {
                Application.SetFont("Comic Sans MS", -12);
                Text = "Subclassing successful";
            }
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
                Application.Quit();
                break;
        }
        return base.WndProc(hWnd, uMsg, wParam, lParam);
    }
}