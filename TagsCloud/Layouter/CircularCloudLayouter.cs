using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.Layouter
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        private readonly ILayoutAlgorithm layoutAlgorithm;
        private readonly List<Rectangle> rectangles;
        public IReadOnlyCollection<Rectangle> Rectangles => rectangles.AsReadOnly();
        public CircularCloudLayouter(ILayoutAlgorithm layoutAlgorithm, Point center)
        {
            this.layoutAlgorithm = layoutAlgorithm;
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Center with negative coordinates is not allowed!");
            layoutAlgorithm.SetCenterPoint(center);
            rectangles = new List<Rectangle>();
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
