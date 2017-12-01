using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;
using TagsCloud.Layouter.Interfaces;

namespace TagsCloud.Layouter
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        private readonly ILayoutAlgorithm layoutAlgorithm;
        private readonly List<Rectangle> rectangles;
        public IReadOnlyCollection<Rectangle> Rectangles => rectangles.AsReadOnly();
        public readonly Point Center;
        public CircularCloudLayouter(ILayoutAlgorithm layoutAlgorithm, PointFactory pointFactory)
        {
            var center = pointFactory.Create();
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Center with negative coordinates is not allowed!");
            Center = center;
            rectangles = new List<Rectangle>();
            this.layoutAlgorithm = layoutAlgorithm;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var point = layoutAlgorithm.GetNextPoint();
                var rectangle = new Rectangle(point, rectangleSize);
                
                if (rectangle.IntersectsWith(rectangles))
                    continue;
                
                rectangles.Add(rectangle);
                return rectangle;
            }
        }

        public void Clear()
        {
            rectangles.Clear();
            layoutAlgorithm.Restart();
        }
    }
}
