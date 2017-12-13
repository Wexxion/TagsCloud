using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Moq;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.Layouter;
using TagsCloud.TextAnalyzing;
using TagsCloud.Vizualization;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudDrawer_Should
    {
        private static readonly Font font = new Font("Calibri", 72);

        private readonly IEnumerable<Word> words = new[]
        {
            new Word("a", 4) {Font = font},
            new Word("b", 2) {Font = font},
            new Word("c", 1) {Font = font},
            new Word("abc", 1) {Font = font}
        };


        private Mock<ITagCloudLayouter> layouter;
        private Mock<OneColorSelector> colorSelector;
        private Mock<TagCloudVizualizer> vizualizer;
        private Mock<IImageConfigurator> imageConfigurator;
        private TagCloudDrawer drawer;

        [SetUp]
        public void SetUp()
        {
            layouter = new Mock<ITagCloudLayouter>();
            layouter
                .Setup(x => x.Rectangles)
                .Returns(() => new[] {new Rectangle(Point.Empty, new Size(10, 10))});
            colorSelector = new Mock<OneColorSelector>(Brushes.Black) {CallBase = true};

            imageConfigurator = new Mock<IImageConfigurator>();
            var bitmap = new Bitmap(20, 20);
            var graphics = Graphics.FromImage(bitmap);
            imageConfigurator
                .Setup(x => x.Configure(It.IsAny<IReadOnlyCollection<Rectangle>>(), It.IsAny<Point>()))
                .Returns(() => (bitmap, graphics));

            vizualizer = new Mock<TagCloudVizualizer>(imageConfigurator.Object, Point.Empty) {CallBase = true};

            drawer = new TagCloudDrawer(vizualizer.Object, layouter.Object, colorSelector.Object);
        }

        [Test]
        public void CallImageConfiguratorOnlyOnce()
        {
            drawer.DrawTagCloud(words);

            imageConfigurator
                .Verify(x => x.Configure(It.IsAny<IReadOnlyCollection<Rectangle>>(), It.IsAny<Point>()), 
                Times.Once);
        }

        [Test]
        public void CallLayouterExactlyWordsCountTimes()
        {
            drawer.DrawTagCloud(words);

            layouter.Verify(x => x.PutNextRectangle(It.IsAny<Size>()), Times.Exactly(words.Count()));
            layouter.Verify(x => x.Rectangles, Times.Never);
        }
    }
}