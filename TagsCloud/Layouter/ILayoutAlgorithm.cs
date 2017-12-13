using System.Drawing;

namespace TagsCloud.Layouter
{
    public interface ILayoutAlgorithm
    {
        void SetCenterPoint(Point newCenter);
        Point GetNextPoint();
        void Restart();
    }
}