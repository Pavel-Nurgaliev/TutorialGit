public struct Point
{
    public int X;
    public int Y;
    public Point()
    {

    }
    public Point(int x, int y)
    {
        this.X = x; this.Y = y;
    }
    public Point Moved(int dx, int dy)
    {
        return new Point(X + dx, Y + dy);
    }
}