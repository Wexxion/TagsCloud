using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Layouter;
using TagsCloud.TextAnalyzing;

namespace TagsCloud.Vizualization
{
    public class TagCloudVizualizer
    {
        private readonly IImageConfigurator imageConfigurator;
        private readonly Point center;
        public string FilePath { get; set; }

        public TagCloudVizualizer(IImageConfigurator imageConfigurator, Point center)
        {
            this.imageConfigurator = imageConfigurator;
            this.center = center;
        }

        public Bitmap DrawTagCloud(List<ILayoutComponent<Word>> layoutComponents)
        {
            var (bitmap, graphics) = imageConfigurator
                .Configure(layoutComponents.Select(word => word.LayoutRectangle).ToList(), center);

            foreach (var layoutComponent in layoutComponents)
                graphics.DrawString(layoutComponent.Component.Value, 
                    layoutComponent.Component.Font, layoutComponent.Brush, layoutComponent.LayoutRectangle);

            return bitmap;
        }

        public Bitmap DrawRectCloud(IReadOnlyCollection<Rectangle> rectangles)
        {
            var (bitmap, graphics) = imageConfigurator.Configure(rectangles, center);

            foreach (var rectangle in rectangles)
            {
                graphics.FillRectangle(Brushes.Black, rectangle);
                graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            }

            return bitmap;
        }
    }
}
