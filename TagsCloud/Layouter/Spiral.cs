using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Layouter
{
    public class Spiral : ILayoutAlgorithm
    {
        private Point center = Point.Empty;
        private IEnumerator<Point> enumerator;
        private const int Coils = 100;
        private const int DistanseBetweenCoils = 100;
        private const int DistanseBetweenPoints = 2;
        private const bool ClockwiceRotation = true;

        public void SetCenterPoint(Point newCenter)
        {
            center = newCenter;
        }

        public Point GetNextPoint()
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }

        public void Restart() => enumerator = GetEnumerator();

        public Spiral() => enumerator = GetEnumerator();

        public IEnumerator<Point> GetEnumerator()
        {
            yield return new Point(center.X, center.Y);
            var awayStep = DistanseBetweenCoils / (Coils * 2 * Math.PI);
            var theta = DistanseBetweenPoints / awayStep;
            while (true)
            {
                var away = awayStep * theta;
                var around = theta + (ClockwiceRotation ? 1 : -1);

                var x = (int)(center.X + Math.Cos(around) * away);
                var y = (int)(center.Y + Math.Sin(around) * away);
                yield return new Point(x, y);

                theta += DistanseBetweenPoints / away;
            }
        }
    }
}
