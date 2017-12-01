using System.Drawing;

namespace TagsCloud.Layouter.Interfaces
{
    public interface ILayoutAlgorithm
    {
        Point GetNextPoint();
        void Restart();
    }
}