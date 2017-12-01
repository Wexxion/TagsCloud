using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;
using TagsCloud.Layouter.Interfaces;

namespace TagsCloud.Layouter
{
    public class Spiral : ILayoutAlgorithm
    {
        private Point center;
        private IEnumerator<Point> enumerator;
        private const int Coils = 100;
        private const int DistanseBetweenCoils = 100;
        private const int DistanseBetweenPoints = 2;
        private const int RotationDirection = 1; //Clockwise if positive

        public Point GetNextPoint()
        {
            var res = enumerator.Current;
            enumerator.MoveNext();
            return res;
        }

        public void Restart()
        {
            enumerator = GetEnumerator();
            enumerator.MoveNext();
        }

        public Spiral(PointFactory pointFactory)
        {
            center = pointFactory.Create();
            enumerator = GetEnumerator();
            enumerator.MoveNext();
        }

        public IEnumerator<Point> GetEnumerator()
        {
            yield return new Point(center.X, center.Y);
            var awayStep = DistanseBetweenCoils / (Coils * 2 * Math.PI);
            var theta = DistanseBetweenPoints / awayStep;
            while (true)
            {
                var away = awayStep * theta;
                var around = theta + RotationDirection;

                var x = (int)(center.X + Math.Cos(around) * away);
                var y = (int)(center.Y + Math.Sin(around) * away);
                yield return new Point(x, y);

                theta += DistanseBetweenPoints / away;
            }
        }
    }
}
