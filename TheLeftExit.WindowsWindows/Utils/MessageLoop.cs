public static class MessageLoop
{
    public static void Run()
    {
        MSG message;
        while (User32.GetMessage(out message, 0, 0, 0) != 0)
        {
            User32.TranslateMessage(message);
            User32.DispatchMessage(message);
        }
    }

    public static void Quit() => User32.PostQuitMessage(0);
}