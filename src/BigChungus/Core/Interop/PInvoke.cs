using BigChungus.Common;
using System.Runtime.InteropServices;

[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]

namespace BigChungus.Core.Interop;

public static unsafe partial class PInvoke
{
    [LibraryImport("user32.dll", EntryPoint = "DefWindowProcW")]
    public static partial nint DefWindowProc(nint hWnd, WM msg, nint wParam, nint lParam);

    [LibraryImport("user32.dll", EntryPoint = "GetMessageW")]
    public static partial int GetMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool TranslateMessage(in MSG lpMsg);

    [LibraryImport("user32.dll", EntryPoint = "DispatchMessageW")]
    public static partial nint DispatchMessage(in MSG lpMsg);

    [LibraryImport("user32.dll")]
    public static partial void PostQuitMessage(int nExitCode);

    [LibraryImport("user32.dll")]
    public static partial nint GetSysColorBrush(SYS_COLOR_INDEX nIndex);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ShowWindow(nint hWnd, SHOW_WINDOW_CMD nCmdShow);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UpdateWindow(nint hWnd);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DestroyWindow(nint hWnd);

    [LibraryImport("user32.dll", EntryPoint = "RegisterClassExW")]
    public static partial ushort RegisterClassEx(in WNDCLASSEXW wndClassEx);

    [LibraryImport("user32.dll", EntryPoint = "GetClassInfoExW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetClassInfoEx(nint hInstance, char* lpszClass, out WNDCLASSEXW lpwcx);

    [LibraryImport("user32.dll", EntryPoint = "UnregisterClassW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UnregisterClass(char* lpClassName, nint hInstance);

    [LibraryImport("user32.dll", EntryPoint = "CreateWindowExW")]
    public static partial nint CreateWindowEx(WINDOW_EX_STYLE dwExStyle, char* lpClassName, char* lpWindowName, WINDOW_STYLE dwStyle, int X, int Y, int nWidth, int nHeight, nint hWndParent, nint hMenu, nint hInstance, void* lpParam);

    public const int CW_USEDEFAULT = unchecked((int)0x80000000);

    [LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    public static partial nint GetWindowLongPtr(nint hWnd, WINDOW_LONG_PTR_INDEX nIndex);
    
    [LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    public static partial nint SetWindowLongPtr(nint hWnd, WINDOW_LONG_PTR_INDEX nIndex, long dwNewLong);

    [LibraryImport("user32.dll", EntryPoint = "SetWindowTextW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetWindowText(nint hWnd, char* lpString);

    [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW")]
    public static partial int GetWindowText(nint hWnd, char* lpString, int nMaxCount);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetWindowRect(nint hWnd, out RECT lpRect);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

    [LibraryImport("user.dll", EntryPoint = "GetWindowTextLengthW")]
    public static partial int GetWindowTextLength(nint hWnd);

    [LibraryImport("user32.dll", EntryPoint = "CallWindowProcW")]
    public static partial nint CallWindowProc(nint lpPrevWndFunc, nint hWnd, WM msg, nint wParam, nint lParam);

    [LibraryImport("user32.dll")]
    public static partial nint SetParent(nint hWndChild, nint hWndNewParent);

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleExW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool GetModuleHandleEx(uint dwFlags, nuint lpModuleName, out nint phModule);

    [LibraryImport("comctl32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool InitCommonControlsEx(in INITCOMMONCONTROLSEX picce);

    [LibraryImport("user32.dll", EntryPoint = "SendMessageW")]
    public static unsafe partial nint SendMessage(nint hWnd, WM Msg, nint wParam, nint lParam);

    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW")]
    public static unsafe partial nint CreateFont(int cHeight, int cWidth, int cEscapement, int cOrientation, int cWeight, uint bItalic, uint bUnderline, uint bStrikeOut, uint iCharSet, uint iOutPrecision, uint iClipPrecision, uint iQuality, uint iPitchAndFamily, char* pszFaceName);

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool DeleteObject(nint hObject);

    [LibraryImport("kernel32.dll", EntryPoint = "CreateActCtxW")]
    public static unsafe partial nint CreateActCtx(in ACTCTXW pActCtx);

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool ActivateActCtx(nint hActCtx, out nuint lpCookie);
}
