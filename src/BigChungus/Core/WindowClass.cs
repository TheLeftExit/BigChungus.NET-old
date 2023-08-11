using BigChungus.Common;
using BigChungus.Core.Interop;
using System.Reflection.Metadata;

namespace BigChungus.Core;

public static class WindowClass {
    public static unsafe IDisposable Register(ReadOnlySpan<char> className, WindowCallback windowProcedure, WNDCLASS_STYLES style = WNDCLASS_STYLES.CS_DBLCLKS | WNDCLASS_STYLES.CS_HREDRAW | WNDCLASS_STYLES.CS_VREDRAW, bool enableBackgroundBrush = true)
    {
        fixed (char* classNamePtr = className)
        {
            WNDPROC wndProc = (hWnd, msg, wParam, lParam) => windowProcedure(new(hWnd, msg, wParam, lParam));
            var wndProcPtr = MarshaledDelegateStorage.Current.Add(wndProc);

            var wndClassEx = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = style,
                hbrBackground = enableBackgroundBrush ? PInvoke.GetSysColorBrush(SYS_COLOR_INDEX.COLOR_BTNFACE) : default,
                lpszClassName = classNamePtr,
                lpfnWndProc = wndProcPtr,
                hInstance = ApplicationCommon.Handle
            };

            var returnValue = PInvoke.RegisterClassEx(wndClassEx);
            ReturnValueException.ThrowIf(nameof(PInvoke.RegisterClassEx), returnValue is 0);

            return new ClassContext(returnValue, wndProcPtr);
        }
    }

    public static unsafe IDisposable Superclass(ReadOnlySpan<char> baseClassName, ReadOnlySpan<char> newClassName, WindowCallback callback, out WindowCallback defaultCallback)
    {
        WNDCLASSEXW classInfo;
        classInfo.cbSize = (uint)sizeof(WNDCLASSEXW);
        fixed(char* baseClassNamePtr = baseClassName)
        {
            bool returnValue = PInvoke.GetClassInfoEx(ApplicationCommon.Handle, baseClassNamePtr, out classInfo);
            ReturnValueException.ThrowIf(nameof(PInvoke.GetClassInfoEx), returnValue is false);
        }

        nint baseWndProcPtr = classInfo.lpfnWndProc;
        defaultCallback = args => WindowProcedure.Call(baseWndProcPtr, args);
        WNDPROC newWndProc = (nint handle, WM message, nint wParam, nint lParam) => callback(new(handle, message, wParam, lParam));
        nint newWndProcPtr = MarshaledDelegateStorage.Current.Add(newWndProc);
        classInfo.lpfnWndProc = newWndProcPtr;

        ushort atom;

        fixed (char* newClassNamePtr = newClassName)
        {
            classInfo.lpszClassName = newClassNamePtr;
            var returnValue = PInvoke.RegisterClassEx(classInfo);
            ReturnValueException.ThrowIf(nameof(PInvoke.RegisterClassEx), returnValue is 0);
            atom = returnValue;
        }

        return new SuperclassContext(new(atom, baseWndProcPtr), newWndProcPtr);
    }
}

internal class ClassContext(ushort atom, nint wndProcPtr) : IDisposable
{
    public unsafe void Dispose()
    {
        var returnValue = PInvoke.UnregisterClass((char*)atom, ApplicationCommon.Handle);
        ReturnValueException.ThrowIf(nameof(PInvoke.UnregisterClass), returnValue is false);

        MarshaledDelegateStorage.Current.Remove(wndProcPtr);
    }
}

internal class SuperclassContext(ClassContext classCtx, nint wndProcPtr) : IDisposable
{
    public unsafe void Dispose()
    {
        classCtx.Dispose();
        MarshaledDelegateStorage.Current.Remove(wndProcPtr);
    }
}