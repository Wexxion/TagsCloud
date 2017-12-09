using System.Collections.Generic;
using System.Linq;
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
        

        public TagCloudTextAnalyzer( IFontAnalyzer fontAnalyzer, ITextAnalyzer textAnalyzer,
            IEnumerable<IWordFilter> wordFilters, IEnumerable<IWordConverter> wordConverters, IWordCounter wordCounter)
        {
            this.fontAnalyzer = fontAnalyzer;
            this.textAnalyzer = textAnalyzer;
            this.wordFilters = wordFilters;
            this.wordConverters = wordConverters;
            this.wordCounter = wordCounter;
        }

        public IEnumerable<Word> GetWords(IEnumerable<string> text,int topNWords, 
            int minWordLength, int minFontSize, int maxFontSize, string fontFamily)
        {
            var words = textAnalyzer.GetWords(text, minWordLength);
            words = wordConverters.Aggregate(words, (current, wordConverter) => wordConverter.ConvertWords(current));
            words = wordFilters.Aggregate(words, (current, wordFilter) => wordFilter.FilterWords(current));
            var sortedWords = wordCounter.CountWords(words, topNWords);
            return fontAnalyzer.SetFontForWords(sortedWords, minFontSize, maxFontSize, fontFamily);
        }
    }
}