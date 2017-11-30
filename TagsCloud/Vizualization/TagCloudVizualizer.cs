using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;
using TagsCloud.Interfaces;
using TagsCloud.Layouter.Interfaces;
using TagsCloud.TextAnalyzing;

namespace TagsCloud.Vizualization
{
    public class TagCloudVizualizer
    {
        private readonly Random rnd;
        private readonly IImageConfigurator imageConfigurator;
        private readonly Point center;
        public string FilePath { get; set; }

        public TagCloudVizualizer(IImageConfigurator imageConfigurator, PointFactory pointFactory)
        {
            this.imageConfigurator = imageConfigurator;
            center = pointFactory.Create();
            rnd = new Random();
        }

        public Bitmap DrawTagCloud(List<ILayoutComponent<Word>> layoutComponents)
        {
            var (bitmap, graphics) = imageConfigurator
                .Configure(layoutComponents.Select(word => word.LayoutRectangle).ToList(), center);

            foreach (var layoutComponent in layoutComponents)
            {
                var brush = new SolidBrush(GetRandomColor());
                graphics.DrawString(layoutComponent.Component.Value, 
                    layoutComponent.Component.Font, brush, layoutComponent.LayoutRectangle);
            }

            return bitmap;
        }

        public Bitmap DrawRectCloud(IReadOnlyCollection<Rectangle> rectangles)
        {
            var (bitmap, graphics) = imageConfigurator.Configure(rectangles, center);

            foreach (var rectangle in rectangles)
            {
                graphics.FillRectangle(new SolidBrush(GetRandomColor()), rectangle);
                graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            }

            return bitmap;
        }

        private Color GetRandomColor()
        {
            var r = rnd.Next(255);
            var g = rnd.Next(255);
            var b = rnd.Next(255);
            if (r + g + b > 680) return GetRandomColor();
            var color = Color.FromArgb(r, g, b);
            return color;
        }
    }
}
