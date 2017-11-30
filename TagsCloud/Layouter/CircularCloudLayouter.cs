using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;
using TagsCloud.Layouter.Interfaces;

namespace TagsCloud.Layouter
{
    class CircularCloudLayouter : ITagCloudLayouter
    {
        private readonly Spiral spiral;
        private readonly List<Rectangle> rectangles;
        public IReadOnlyCollection<Rectangle> Rectangles => rectangles.AsReadOnly();
        public readonly Point Center;
        public CircularCloudLayouter(PointFactory pointFactory)
        {
            var center = pointFactory.Create();
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Center with negative coordinates is not allowed!");
            Center = center;
            rectangles = new List<Rectangle>();
            spiral = new Spiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var point = spiral.GetNextPoint();
                var rectangle = new Rectangle(point, rectangleSize);
                
                if (rectangle.IntersectsWith(rectangles))
                    continue;
                
                rectangles.Add(rectangle);
                return rectangle;
            }
        }
    }
}
