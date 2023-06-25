using System.Runtime.InteropServices;

public static class Application
{
    public static void Run()
    {
        MSG message;
        while (PInvoke.GetMessage(out message, 0, 0, 0) != 0)
        {
            PInvoke.TranslateMessage(message);
            PInvoke.DispatchMessage(message);
        }
    }

    public static void Quit() => PInvoke.PostQuitMessage(0);

    public static nint Handle => PInvoke.GetModuleHandleEx(2, 0, out var handle) ? handle : throw new ApplicationException();

    public static unsafe void EnableVisualStyles()
    {
        const string manifest =
            """
            <?xml version="1.0" encoding="UTF-8" standalone="yes" ?>
            <assembly xmlns="urn:schemas-microsoft-com:asm.v1" manifestVersion="1.0">
                <description>Windows Forms Common Control manifest</description>
                <dependency>
                    <dependentAssembly>
                        <assemblyIdentity
                            type="win32"
                            name="Microsoft.Windows.Common-Controls"
                            version="6.0.0.0"
                            processorArchitecture="*"
                            publicKeyToken="6595b64144ccf1df"
                            language="*"
                        />
                    </dependentAssembly>
                </dependency>
            </assembly>
            """;

        var tempFilePath = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());
        File.WriteAllText(tempFilePath, manifest);
        fixed (char* tempFilePathPtr = tempFilePath)
        {
            var actctx = new ACTCTXW()
            {
                cbSize = (uint)sizeof(ACTCTXW),
                lpSource = tempFilePathPtr
            };
            var contextHandle = PInvoke.CreateActCtx(actctx);
            if (contextHandle == -1) throw new ApplicationException();
            var activated = PInvoke.ActivateActCtx(contextHandle, out _);
            if (!activated) throw new ApplicationException();
        }
        File.Delete(tempFilePath);
    }

    private static nint currentFont;

    public static unsafe void SetFont(string name, int size)
    {
        if (currentFont != 0)
        {
            PInvoke.DeleteObject(currentFont);
        }
        fixed (char* namePtr = name)
        {
            var handle = PInvoke.CreateFont(
                size,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                namePtr
            );
            if (handle == default) throw new ApplicationException();
            currentFont = handle;
            Window.Broadcast(WM.SETFONT, (nuint)currentFont, 1);
        }
    }

    internal static void OnWindowCreated(Window window)
    {
        if (currentFont != 0)
        {
            PInvoke.SendMessage(window.Handle, WM.SETFONT, (nuint)currentFont, 1);
        }
    }
}