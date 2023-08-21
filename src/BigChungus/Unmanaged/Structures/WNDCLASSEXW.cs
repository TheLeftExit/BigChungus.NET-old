namespace BigChungus.Unmanaged;

public unsafe struct WNDCLASSEXW
{
    public uint cbSize;
    public WNDCLASS_STYLES style;
    public nint lpfnWndProc;
    public int cbClsExtra;
    public int cbWndExtra;
    public nint hInstance;
    public nint hIcon;
    public nint hCursor;
    public nint hbrBackground;
    public char* lpszMenuName;
    public char* lpszClassName;
    public nint hIconSm;
}
