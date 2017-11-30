using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IImageConfigurator
    {
        //Configure image size and translateTransform
        (Bitmap bitmap, Graphics graphics) Configure (IReadOnlyCollection<Rectangle> rectangles, Point center);
    }
}