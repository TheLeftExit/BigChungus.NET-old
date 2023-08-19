using BigChungus.Common;
using BigChungus.Core;
using System.Runtime.CompilerServices;

namespace BigChungus.Controls;

file static class IntPtrExtensions
{
    public static ushort LOWORD(this nint value) => (ushort)(value & 0xFFFF);
    public static ushort HIWORD(this nint value) => (ushort)(value >> 16 & 0xFFFF);
}

public readonly struct WmCommandArgs
{
    public readonly nint Id;
    public readonly nint Handle;
    public readonly nint Code;

    private WmCommandArgs(nint id, nint handle, nint code)
    {
        Id = id;
        Handle = handle;
        Code = code;
    }

    public static bool TryDecode(WindowProcedureArgs args, out WmCommandArgs result)
    {
        if (args.Message != WM.COMMAND)
        {
            result = default;
            return false;
        }
        result = new(args.WParam.LOWORD(), args.LParam, args.WParam.HIWORD());
        return true;
    }
}

public interface IWmNotifyInfo
{
    nint Id { get; }
    nint Handle { get; }
    nint Code { get; }
}

public readonly ref struct WmNotifyArgs<TInfo> where TInfo : unmanaged, IWmNotifyInfo
{
    public nint Id => Info.Id;
    public nint Handle => Info.Handle;
    public nint Code => Info.Code;
    public readonly ref TInfo Info;

    private WmNotifyArgs(ref TInfo infoPtr)
    {
        Info = ref infoPtr;
    }

    public static unsafe bool TryDecode(WindowProcedureArgs args, out WmNotifyArgs<TInfo> result)
    {
        if (args.Message != WM.NOTIFY)
        {
            result = default;
            return false;
        }
        result = new(ref Unsafe.AsRef<TInfo>((void*)args.LParam));
        return true;
    }
}