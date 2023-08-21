using BigChungus.Unmanaged.Libraries;
using BigChungus.Unmanaged;
using System.Diagnostics;

namespace BigChungus.Managed;

public static class ApplicationMethods
{
    public static void RunMessageLoop()
    {
        MSG message;
        while (User32.GetMessage(out message, 0, 0, 0) != 0)
        {
            User32.TranslateMessage(message);
            User32.DispatchMessage(message);
        }
    }

    public static void PostQuit() => User32.PostQuitMessage(0);

    public static nint GetApplicationHandle() => Kernel32.GetModuleHandleEx(2, 0, out var handle) ? handle : throw new UnreachableException();

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
            var contextHandle = Kernel32.CreateActCtx(actctx);
            if (contextHandle == -1) throw new UnreachableException();
            var activated = Kernel32.ActivateActCtx(contextHandle, out _);
            if (!activated) throw new UnreachableException();
        }
        File.Delete(tempFilePath);
    }

    public static unsafe void LoadCommonControls()
    {
        bool returnValue = ComCtl32.InitCommonControlsEx(new INITCOMMONCONTROLSEX
        {
            dwSize = (uint)sizeof(INITCOMMONCONTROLSEX),
            dwICC = INITCOMMONCONTROLSEX_ICC.ICC_WIN95_CLASSES
        });
        if(!returnValue) throw new UnreachableException();
    }
}