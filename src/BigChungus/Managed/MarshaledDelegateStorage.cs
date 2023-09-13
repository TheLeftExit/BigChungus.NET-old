using System.Runtime.InteropServices;

namespace BigChungus.Managed;

public static class MarshaledDelegateStorage
{
    private static Dictionary<nint, Delegate> delegatesByFunctionPointers = new();

    public static nint Add<TDelegate>(TDelegate function) where TDelegate : Delegate
    {
        var pointer = Marshal.GetFunctionPointerForDelegate(function);
        lock (delegatesByFunctionPointers) delegatesByFunctionPointers.Add(pointer, function);
        return pointer;
    }

    public static void Remove(nint pointer)
    {
        lock (delegatesByFunctionPointers) delegatesByFunctionPointers.Remove(pointer);
    }
}