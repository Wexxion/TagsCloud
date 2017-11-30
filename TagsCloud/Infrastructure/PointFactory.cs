using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class PointFactory
    {
        public Point Create() => Point.Empty;
        public Point Create(int x, int y) => new Point(x, y);
    }
}