using System.Drawing;

namespace BigChungus.Unmanaged;

public struct RECT
{
    public int left;
    public int top;
    public int right;
    public int bottom;

    public static implicit operator Rectangle(RECT rect) => Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
    public static implicit operator RECT(Rectangle rectangle) => new RECT
    {
        left = rectangle.Left,
        top = rectangle.Top,
        right = rectangle.Right,
        bottom = rectangle.Bottom
    };
}

public struct SIZE
{
    public int cx;
    public int cy;

    public static implicit operator Size(SIZE size) => new Size(size.cx, size.cy);
    public static implicit operator SIZE(Size size) => new SIZE { cx = size.Width, cy = size.Height };
}

public struct POINT
{
    public int x;
    public int y;

    public static implicit operator Point(POINT point) => new Point(point.x, point.y);
    public static implicit operator POINT(Point point) => new POINT { x = point.X, y = point.Y };
}