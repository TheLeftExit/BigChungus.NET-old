using BigChungus.Controls;
using BigChungus.Common;
using BigChungus.Drawing;
using BigChungus.Core;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

Application.DefaultFont = new Font("Segoe UI", -12);

Application.Run(new Form1());

public class Form1 : Form
{
    Button button1;
    Button button2;
    IDisposable button1Subclass;

    public Form1()
    {
        Text = "Hello world!";
        Bounds = new System.Drawing.Rectangle(10, 10, 300, 200);

        button1 = new Button(this)
        {
            Text = "Click me!",
            Bounds = new System.Drawing.Rectangle(50, 50, 120, 30),
        };

        button2 = new Button(this)
        {
            Text = "Don't click me!",
            Bounds = new System.Drawing.Rectangle(50, 90, 120, 30),
            Font = new Font("Cascadia Mono", -12)
        };

        button2.Clicked += button =>
        {
            Application.DefaultFont = new Font("Comic Sans MS", -12);
            Text = "Subclassing successful";
        };

        button1.Clicked += button => button.Text = "Good job!";

        button2.Clicked += button => button2.Text = ">:(";

        WindowProcedure.Subclass(button2.Handle, (args, defWndProc) =>
        {
            if (args.Message == WM.LBUTTONUP)
            {
                Application.DefaultFont = new Font("Comic Sans MS", -12);
                Text = "Subclassing successful";
            }
            return defWndProc(args);
        });
    }
}