using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.TextAnalyzing;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudTextAnalyzer_Should
    {
        private readonly List<string> testText = new List<string> { "a b c", "abc", "a b", "a", "a" };

        private TagCloudTextAnalyzer tagCloudTextAnalyzer;
        private Mock<WordCounter> wordCounter;
        private Mock<FontAnalyzer> fontAnalyzer;
        private Mock<ITextAnalyzer> textAnalyzer;
        private Mock<IWordFilter> wordFilter1;
        private Mock<IWordFilter> wordFilter2;
        private Mock<IWordConverter> wordConverter1;
        private Mock<IWordConverter> wordConverter2;


        private int TopNWords = 250;
        private int MinWordLength = 3;
        private int MinFontSize = 20;
        private int MaxFontSize = 72;
        private string FontFamily = "Calibri";

        [SetUp]
        public void SetUp()
        {
            wordCounter = new Mock<WordCounter> { CallBase = true };
            fontAnalyzer = new Mock<FontAnalyzer> { CallBase = true };

            textAnalyzer = new Mock<ITextAnalyzer>();
            textAnalyzer
                .Setup(x => x.GetWords(It.IsAny<List<string>>(), It.IsAny<int>()))
                .Returns(new List<string> { "a", "a", "a", "a", "b", "b", "c", "abc" });

            wordFilter1 = new Mock<IWordFilter>();
            wordFilter1
                .Setup(x => x.FilterWords(It.IsAny<List<string>>()))
                .Returns<List<string>>(x => x);

            wordFilter2 = new Mock<IWordFilter>();
            wordFilter2
                .Setup(x => x.FilterWords(It.IsAny<List<string>>()))
                .Returns<List<string>>(x => x);

            wordConverter1 = new Mock<IWordConverter>();
            wordConverter1
                .Setup(x => x.ConvertWords(It.IsAny<List<string>>()))
                .Returns<List<string>>(x => x);

            wordConverter2 = new Mock<IWordConverter>();
            wordConverter2
                .Setup(x => x.ConvertWords(It.IsAny<List<string>>()))
                .Returns<List<string>>(x => x);

            tagCloudTextAnalyzer = new TagCloudTextAnalyzer(fontAnalyzer.Object, textAnalyzer.Object,
                new[] { wordFilter1.Object, wordFilter2.Object },
                new[] { wordConverter1.Object, wordConverter2.Object }, wordCounter.Object);
        }


        [Test]
        public void CallConverterAndFilterOnlyOnce()
        {
            var expected = new[] { new Word("a", 4), new Word("b", 2), new Word("c", 1), new Word("abc", 1) };

            var res = tagCloudTextAnalyzer.GetWords(testText, TopNWords, MinWordLength, MinFontSize, MaxFontSize, FontFamily);

            wordConverter1.Verify(o => o.ConvertWords(It.IsAny<List<string>>()), Times.Once);
            wordConverter2.Verify(o => o.ConvertWords(It.IsAny<List<string>>()), Times.Once);
            wordFilter1.Verify(o => o.FilterWords(It.IsAny<List<string>>()), Times.Once);
            wordFilter2.Verify(o => o.FilterWords(It.IsAny<List<string>>()), Times.Once);

            res.GetValueOrThrow().ShouldAllBeEquivalentTo(expected, opt => opt.Excluding(w => w.Font));
        }

        [Test]
        public void UseFilters()
        {
            wordFilter1
                .Setup(x => x.FilterWords(It.IsAny<List<string>>()))
                .Returns<IEnumerable<string>>(x => x.Where(y => y.Length > 2).ToList());
            var expected = new[] { new Word("abc", 1) };

            var res = tagCloudTextAnalyzer.GetWords(testText, TopNWords, MinWordLength, MinFontSize, MaxFontSize, FontFamily);

            res.GetValueOrThrow().ShouldAllBeEquivalentTo(expected, opt => opt.Excluding(w => w.Font));
        }

        [Test]
        public void UseConverters()
        {
            wordConverter1
                .Setup(x => x.ConvertWords(It.IsAny<List<string>>()))
                .Returns<IEnumerable<string>>(x => x.Select(y => y + "!").ToList());
            wordConverter2
                .Setup(x => x.ConvertWords(It.IsAny<List<string>>()))
                .Returns<IEnumerable<string>>(x => x.Select(y => y + "?").ToList());
            var expected = new[] { new Word("a!?", 4), new Word("b!?", 2), new Word("c!?", 1), new Word("abc!?", 1) };

            var res = tagCloudTextAnalyzer.GetWords(testText, TopNWords, MinWordLength, MinFontSize, MaxFontSize, FontFamily);

            res.GetValueOrThrow().ShouldAllBeEquivalentTo(expected, opt => opt.Excluding(w => w.Font));
        }
    }
}