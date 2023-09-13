using System.Runtime.InteropServices;

namespace BigChungus.Unmanaged;

public static partial class Kernel32
{
    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleExW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool GetModuleHandleEx(uint dwFlags, nuint lpModuleName, out nint phModule);

    [LibraryImport("kernel32.dll", EntryPoint = "CreateActCtxW")]
    public static unsafe partial nint CreateActCtx(in ACTCTXW pActCtx);

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool ActivateActCtx(nint hActCtx, out nuint lpCookie);

    [LibraryImport("kernel32.dll")]
    public static unsafe partial void* LocalLock(nint hMem);

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool LocalUnlock(nint hMem);
}
