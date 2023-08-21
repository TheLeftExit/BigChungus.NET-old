using System.Runtime.InteropServices;

namespace BigChungus.Unmanaged.Libraries;

public static unsafe partial class Gdi32
{
    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW")]
    public static unsafe partial nint CreateFont(int cHeight, int cWidth, int cEscapement, int cOrientation, int cWeight, uint bItalic, uint bUnderline, uint bStrikeOut, uint iCharSet, uint iOutPrecision, uint iClipPrecision, uint iQuality, uint iPitchAndFamily, char* pszFaceName);

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static unsafe partial bool DeleteObject(nint hObject);
}
