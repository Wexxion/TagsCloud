﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Infrastructure;
using TagsCloud.Layouter;

namespace TagsCloud.Tests
{
    [TestFixture]
    class Spiral_Should
    {
        private Point center;
        private Spiral spiral;

        [SetUp]
        public void SetUp()
        {
            center = new Point(0, 0);
            spiral = new Spiral(new PointFactory());
        }

        [Test]
        public void ReturnFirstPointAsCenter()
        {
            var firstPoint = spiral.GetNextPoint();

            firstPoint.ShouldBeEquivalentTo(center);
        }

        [TestCaseSource("TestCasesForSpiralPontsTest")]
        public void ReturnPointsOnSpiral(IEnumerable<Point> expectedPoints, int count, int offset)
        {
            var points = new List<Point>();
            for (var i = 0; i < count; i++)
                points.Add(spiral.GetNextPoint());

            points.Should().HaveCount(count);
            points.Skip(offset).Should().Equal(expectedPoints);
        }

        private static IEnumerable TestCasesForSpiralPontsTest()
        {
            return new[]
            {
                new TestCaseData(new List<Point>
                { new Point(0, 0), new Point(1, 1), new Point(0, 1), new Point(-2, 0), new Point(-1, -1)}, 5, 0)
                    .SetName("First 5 points"),
                new TestCaseData(new List<Point>
                { new Point(0, -2), new Point(1, -2), new Point(2, 0), new Point(2, 1),  new Point(0, 2)}, 10, 5)
                    .SetName("Another 5 points (10 Totaly)"),
                new TestCaseData(new List<Point>
                { new Point(-1, 2), new Point(-2, 1), new Point(-3, 0), new Point(-2, -1), new Point(-1, -3)}, 15, 10)
                    .SetName("Another 5 points (15 Totaly)"),
            };
        }
    }
}
