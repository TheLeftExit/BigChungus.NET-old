using BigChungus.Common;
using BigChungus.Core;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace BigChungus.Windows;

internal static class WindowClassManager {
    private static Dictionary<string, WindowClassDefinition> classes = new();

    public static (string ClassName, WindowCallback DefaultCallback) GetSuperclass(string baseClassName)
    {
        lock (classes)
        {
            if (!classes.TryGetValue(baseClassName, out var classDefinition))
            {
                var newClassName = "BigChungus." + baseClassName;
                classDefinition = new
                (
                    ClassName: newClassName,
                    Context: WindowClass.Superclass(baseClassName, newClassName, MessageRouter.HandleWindowMessage, out var defaultCallback),
                    Callback: defaultCallback
                );
                classes.Add(baseClassName, classDefinition);
            }

            return (classDefinition.ClassName, classDefinition.Callback);
        }
    }

    public static string GetCustomClass()
    {
        lock (classes)
        {
            const string className = "BigChungus.CustomClass";

            if (!classes.ContainsKey(className))
            {
                var classDefinition = new WindowClassDefinition
                (
                    ClassName: className,
                    Context: WindowClass.Register(className, MessageRouter.HandleWindowMessage),
                    Callback: null
                );
                classes.Add(className, classDefinition);
            }

            return className;
        }
    }
}

internal record struct WindowClassDefinition(string ClassName, WindowCallback Callback, IDisposable Context) { }