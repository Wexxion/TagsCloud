using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    class ImageConfigurator : IImageConfigurator
    {
        private Bitmap bitmap;
        private Graphics graphics;

        public void Configure(IReadOnlyCollection<Rectangle> rectangles, Point center)
        {

            var maxWidth = rectangles.Max(rect => rect.Width);
            var maxHeight = rectangles.Max(rect => rect.Height);
            var size = Geometry.CalculateImageSize(rectangles);

            bitmap = new Bitmap(size.Width, size.Height);
            graphics = Graphics.FromImage(bitmap);

            var dx = size.Width / 2 - center.X - maxWidth / 2;
            var dy = size.Height / 2 - center.Y - maxHeight / 2;
            graphics.TranslateTransform(dx, dy);

            graphics.Clear(Color.White);
        }

        public Graphics Graphics => graphics;

        public void SaveImage(string filepath)
        {
            if (string.IsNullOrEmpty(filepath)) throw new ArgumentNullException();
            bitmap.Save(filepath);
        }
    }
}
