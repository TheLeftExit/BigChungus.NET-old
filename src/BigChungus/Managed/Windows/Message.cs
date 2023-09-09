using BigChungus.Unmanaged;
using System.Runtime.CompilerServices;

namespace BigChungus.Managed;

public readonly record struct DWord(ushort Low, ushort High)
{
    public DWord(nint value) : this((ushort)(value & 0xFFFF), (ushort)((value >> 16) & 0xFFFF)) { }
    public readonly nint Value => (High << 16) | Low;
    public static implicit operator nint(DWord dWord) => dWord.Value;
    public static implicit operator DWord(nint value) => new(value);
    public static implicit operator (ushort, ushort)(DWord dWord) => (dWord.Low, dWord.High);
}

public struct Message
{
    public nint Handle { get; set; }
    public uint Code { get; set; }
    public nint WParam { get; set; }
    public nint LParam { get; set; }

    public Message(nint hWnd, uint msg, nint wParam, nint lParam)
    {
        Handle = hWnd;
        Code = msg;
        WParam = wParam;
        LParam = lParam;
    }

    public unsafe bool TryDecode(out NMHDR header)
    {
        if (Code == WM.COMMAND)
        {
            var dWord = new DWord(WParam);
            header = new(LParam, dWord.Low, dWord.High);
            return true;
        }
        if (Code == WM.NOTIFY)
        {
            header = *(NMHDR*)LParam;
            return true;
        }
        header = default;
        return false;
    }

    public unsafe ref T GetExtraInfo<T>() where T : unmanaged
    {
        return ref Unsafe.AsRef<T>((void*)LParam);
    }
}