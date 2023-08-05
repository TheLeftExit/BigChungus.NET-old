using System.Runtime.InteropServices;

namespace BigChungus.Core;

public class MarshaledDelegateStorage {
    [ThreadStatic]
    private static MarshaledDelegateStorage current;
    public static MarshaledDelegateStorage Current => current ??= new() { delegatesByFunctionPointers = new() };

    private Dictionary<nint, object> delegatesByFunctionPointers;

    public nint Add<TDelegate>(TDelegate function) where TDelegate : Delegate
    {
        var pointer = Marshal.GetFunctionPointerForDelegate(function);
        delegatesByFunctionPointers.Add(pointer, function);
        return pointer;
    }

    public void Remove(nint pointer)
    {
        delegatesByFunctionPointers.Remove(pointer);
    }
}