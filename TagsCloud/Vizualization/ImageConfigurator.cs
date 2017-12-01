using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;
using TagsCloud.Interfaces;

namespace TagsCloud.Vizualization
{
    public class ImageConfigurator : IImageConfigurator
    {
        (Bitmap bitmap, Graphics graphics) IImageConfigurator.Configure(IReadOnlyCollection<Rectangle> rectangles, Point center)
        {
            var maxWidth = rectangles.Max(rect => rect.Width);
            var maxHeight = rectangles.Max(rect => rect.Height);
            var size = Geometry.CalculateImageSize(rectangles);

            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            var dx = size.Width / 2 - center.X - maxWidth / 4;
            var dy = size.Height / 2 - center.Y - maxHeight / 4;
            graphics.TranslateTransform(dx, dy);

            graphics.Clear(Color.White);
            return (bitmap, graphics);
        }
    }
}
