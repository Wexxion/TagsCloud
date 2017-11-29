using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions.Common;
using TagsCloud.Interfaces;
using TagsCloud.Layouter.Interfaces;
using TagsCloud.TextAnalyzing;

namespace TagsCloud
{
    public class TagCloudVizualizer
    {
        private readonly Random rnd;
        private readonly IImageConfigurator imageConfigurator;
        private readonly Point center;
        public string FilePath { get; set; }

        public TagCloudVizualizer(IImageConfigurator imageConfigurator, Point center)
        {
            this.imageConfigurator = imageConfigurator;
            this.center = center;
            rnd = new Random();
        }

        public void DrawTagCloud(List<ILayoutComponent<Word>> layoutComponents)
        {
            imageConfigurator.Configure(layoutComponents.Select(word => word.LayoutRectangle).ToList(), center);

            foreach (var layoutComponent in layoutComponents)
            {
                var brush = new SolidBrush(GetRandomColor());
                imageConfigurator.Graphics.DrawString(layoutComponent.Component.Value, 
                    layoutComponent.Component.Font, brush, layoutComponent.LayoutRectangle);
            }

            imageConfigurator.SaveImage(FilePath);
        }

        public void DrawRectCloud(IReadOnlyCollection<Rectangle> rectangles)
        {
            imageConfigurator.Configure(rectangles, center);

            foreach (var rectangle in rectangles)
            {
                imageConfigurator.Graphics.FillRectangle(new SolidBrush(GetRandomColor()), rectangle);
                imageConfigurator.Graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            }

            imageConfigurator.SaveImage(FilePath);
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
