using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Layouter.Interfaces
{
    public interface ITagCloudLayouter
    {
        IReadOnlyCollection<Rectangle> Rectangles { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
