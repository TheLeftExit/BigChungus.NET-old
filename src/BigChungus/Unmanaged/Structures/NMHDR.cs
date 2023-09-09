namespace BigChungus.Unmanaged;

public struct NMHDR
{
    public nint hwndFrom;
    public nuint idFrom;
    public uint code;

    public nint Handle => hwndFrom;
    public nuint Id => idFrom;
    public uint Code => code;

    public NMHDR(nint handle, nuint id, uint nCode)
    {
        hwndFrom = handle;
        idFrom = id;
        code = nCode;
    }
}
