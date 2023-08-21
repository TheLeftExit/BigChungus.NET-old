namespace BigChungus.Unmanaged;

public unsafe struct ACTCTXW
{
    public uint cbSize;
    public uint dwFlags;
    public char* lpSource;
    public ushort wProcessorArchitecture;
    public ushort wLangId;
    public char* lpAssemblyDirectory;
    public char* lpResourceName;
    public char* lpApplicationName;
    public nint hModule;
}
