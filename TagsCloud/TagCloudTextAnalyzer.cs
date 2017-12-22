using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;
using TagsCloud.TextAnalyzing;

namespace TagsCloud
{
    public class TagCloudTextAnalyzer
    {
        private readonly IWordCounter wordCounter;
        private readonly IFontAnalyzer fontAnalyzer;
        private readonly ITextAnalyzer textAnalyzer;
        private readonly IEnumerable<IWordFilter> wordFilters;
        private readonly IEnumerable<IWordConverter> wordConverters;


        public TagCloudTextAnalyzer(IFontAnalyzer fontAnalyzer, ITextAnalyzer textAnalyzer,
            IEnumerable<IWordFilter> wordFilters, IEnumerable<IWordConverter> wordConverters, IWordCounter wordCounter)
        {
            this.fontAnalyzer = fontAnalyzer;
            this.textAnalyzer = textAnalyzer;
            this.wordFilters = wordFilters;
            this.wordConverters = wordConverters;
            this.wordCounter = wordCounter;
        }


        public Result<List<Word>> GetWords(List<string> text, int topNWords,
            int minWordLength, int minFontSize, int maxFontSize, string fontFamily)
        {
            return textAnalyzer.GetWords(text, minWordLength)
                .Then(words => wordConverters.Aggregate(words,
                    (current, converter) => converter.ConvertWords(current).GetValueOrThrow()))
                .Then(words =>
                    wordFilters.Aggregate(words, (current, filter) => filter.FilterWords(current).GetValueOrThrow()))
                .Then(words => wordCounter.CountWords(words, topNWords))
                .Then(words => fontAnalyzer.SetFontForWords(words, minFontSize, maxFontSize, fontFamily));
        }
    }
}