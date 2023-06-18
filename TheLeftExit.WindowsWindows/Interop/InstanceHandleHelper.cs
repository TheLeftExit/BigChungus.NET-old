using System.Runtime.InteropServices;

public static partial class InstanceHandleHelper
{
    public static nint GetHandle()
    {
        if(!GetModuleHandleEx(2, 0, out var handle)) throw new ApplicationException();
        return handle;
    }

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleExW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static unsafe partial bool GetModuleHandleEx(uint dwFlags, nuint lpModuleName, out nint phModule);
}