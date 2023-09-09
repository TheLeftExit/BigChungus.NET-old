using System.Runtime.InteropServices;

namespace BigChungus.Unmanaged;

public delegate nint WNDPROC(nint hWnd, uint msg, nint wParam, nint lParam);

public unsafe delegate int EDITWORDBREAKPROCW(char* lpch, int ichCurrent, int cch, WB code);

public static class CW
{
    public const int USEDEFAULT = -2147483648;
}