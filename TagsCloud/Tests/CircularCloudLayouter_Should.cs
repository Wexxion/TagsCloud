using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloud.Infrastructure;
using TagsCloud.Layouter;
using TagsCloud.Vizualization;

namespace TagsCloud.Tests
{
    [TestFixture]
    class CircularCloudLayouter_Should
    {
        private Point center;
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            center = Point.Empty;
            layouter = new CircularCloudLayouter(new Spiral(), center);
        }

        [Test]
        public void PlaceFirstRetInCenter()
        {
            var size = new Size(50, 20);
            var res = layouter.PutNextRectangle(size);

            res.Location.ShouldBeEquivalentTo(center);
        }

        [TestCase(-1, -1)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        public void ThrowArgumentException_WhenCenterHasNegativeCoordinates(int x, int y)
        {
            center = new Point(x, y);
            Action act = () => new CircularCloudLayouter(new Spiral(), center);

            act.ShouldThrow<ArgumentException>().WithMessage("*negative*");
        }

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(500)]
        public void PlaceRectangeWithGivenSize(int count)
        {
            var sizes = GenerateRandomRectSize(count).ToArray();
            var rects = sizes.Select(size => layouter.PutNextRectangle(size));

            sizes.Should().Equal(rects.Select(rect => rect.Size));
        }

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(500)]
        public void PlaceAllGivenRects(int count)
        {
            foreach (var size in GenerateRandomRectSize(count))
                layouter.PutNextRectangle(size);

            layouter.Rectangles.Should().HaveCount(count);
        }

        [TestCase(2)]
        [TestCase(100)]
        [TestCase(500)]
        public void NotIntersectRectangles(int count)
        {
            foreach (var size in GenerateRandomRectSize(count))
                layouter.PutNextRectangle(size);
            foreach (var rectangle in layouter.Rectangles)
            {
                var otherRects = layouter.Rectangles.ReferenceClone();
                otherRects.Remove(rectangle);

                rectangle.IntersectsWith(otherRects).Should().BeFalse();
            }
        }

        [Test]
        public void JustFailingTest()
        {
            for (int i = 0; i < 2000; i++)
                layouter.PutNextRectangle(new Size(60, 25));
            layouter.Rectangles.Should().HaveCount(2001);
        }

        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.Outcome.Status != TestStatus.Failed) return;
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = $@"{appPath}\..\..\Tests\Fails\{context.Test.Name}.jpg";
            var visualizer = new TagCloudVizualizer(new ImageConfigurator(), center) {FilePath = filepath};
            visualizer.DrawRectCloud(layouter.Rectangles);

            Console.WriteLine($"Tag cloud visualization saved to file {filepath}");
        }

        private IEnumerable<Size> GenerateRandomRectSize(int count = 1)
        {
            var rnd = new Random();
            for (var i = 0; i < count; i++)
                yield return new Size(rnd.Next(35, 75), rnd.Next(15, 50));
        }
    }
}
