﻿using System.Drawing;

namespace BigChungus.Unmanaged;

public struct MSG
{
    public nint hwnd;
    public uint message;
    public nint wParam;
    public nint lParam;
    public uint time;
    public Point pt;
    public int lPrivate;
}
