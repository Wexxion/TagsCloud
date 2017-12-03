using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Vizualization
{
    public interface IImageConfigurator
    {
        //Configure image size and translateTransform
        (Bitmap bitmap, Graphics graphics) Configure(IReadOnlyCollection<Rectangle> rectangles, Point center);
    }
    public class ImageConfigurator : IImageConfigurator
    {
        public (Bitmap bitmap, Graphics graphics) Configure(IReadOnlyCollection<Rectangle> rectangles, Point center)
        {
            var maxWidth = rectangles.Max(rect => rect.Width);
            var maxHeight = rectangles.Max(rect => rect.Height);
            var size = CalculateImageSize(rectangles);

            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            var dx = size.Width / 2 - center.X - maxWidth / 4;
            var dy = size.Height / 2 - center.Y - maxHeight / 4;
            graphics.TranslateTransform(dx, dy);

            graphics.Clear(Color.White);
            return (bitmap, graphics);
        }

        private Size CalculateImageSize(IReadOnlyCollection<Rectangle> rectangles)
        {
            var minX = rectangles.Min(rect => rect.X);
            var minY = rectangles.Min(rect => rect.Y);
            var maxX = rectangles.Max(rect => rect.X + rect.Width);
            var maxY = rectangles.Max(rect => rect.Y + rect.Height);
            var maxWidth = rectangles.Max(rect => rect.Width);
            var maxHeight = rectangles.Max(rect => rect.Height);
            var width = maxX - minX + maxWidth;
            var height = maxY - minY + maxHeight;
            return new Size(width, height);
        }
    }
}
