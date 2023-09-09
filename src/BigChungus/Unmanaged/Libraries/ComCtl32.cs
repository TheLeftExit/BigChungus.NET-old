using System.Runtime.InteropServices;

namespace BigChungus.Unmanaged;

public static unsafe partial class ComCtl32
{
    [LibraryImport("comctl32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool InitCommonControlsEx(in INITCOMMONCONTROLSEX picce);
}
