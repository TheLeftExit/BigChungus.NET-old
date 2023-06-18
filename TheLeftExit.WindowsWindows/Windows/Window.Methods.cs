public partial class Window
{
    public void Show(SHOW_WINDOW_CMD showMode = SHOW_WINDOW_CMD.SW_SHOW) => User32.ShowWindow(Handle, showMode);
    //public void Update() => User32.UpdateWindow(Handle); - Do we really need it?
    public void Destroy() => User32.DestroyWindow(Handle);
}