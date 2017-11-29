using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IImageConfigurator
    {
        //Configure image size and translateTransform
        void Configure(IReadOnlyCollection<Rectangle> rectangles, Point center);
        Graphics Graphics { get; }
        void SaveImage(string filepath);
    }
}