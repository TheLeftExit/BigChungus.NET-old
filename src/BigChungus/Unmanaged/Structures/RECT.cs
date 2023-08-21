using System.Drawing;

namespace BigChungus.Unmanaged;

public struct RECT
{
    public int left;
    public int top;
    public int right;
    public int bottom;

    public static RECT FromRectangle(Rectangle rectangle)
    {
        return new RECT
        {
            left = rectangle.Left,
            top = rectangle.Top,
            right = rectangle.Right,
            bottom = rectangle.Bottom
        };
    }

    public Rectangle ToRectangle() => Rectangle.FromLTRB(left, top, right, bottom);
}