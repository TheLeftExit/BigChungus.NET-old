using System.Runtime.InteropServices;

namespace BigChungus.Unmanaged;

public static partial class ComCtl32
{
    [LibraryImport("comctl32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool InitCommonControlsEx(in INITCOMMONCONTROLSEX picce);
}