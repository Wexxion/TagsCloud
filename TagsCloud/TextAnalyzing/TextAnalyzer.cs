using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextAnalyzing.Interfaces;

namespace TagsCloud.TextAnalyzing
{
    internal class TextAnalyzer : ITextAnalyzer
    {
        private readonly IWordFilter wordFilter;
        private readonly IWordConverter wordConverter;

        public TextAnalyzer(IWordFilter wordFilter, IWordConverter wordConverter)
        {
            this.wordFilter = wordFilter;
            this.wordConverter = wordConverter;
        }

        public int TopNWords { get; set; } = 0;
        public int MinWordLength { get; set; } = 3;

        public IEnumerable<Word> GetSortedWords(IEnumerable<string> text)
        {
            var allWords = FindAllwords(text);
            var filteredWords = wordFilter.FilterWords(allWords);
            var convertedWords = wordConverter.ConvertWords(filteredWords);
            return TopNWords == 0
                ? CountWords(convertedWords)
                : CountWords(convertedWords).Take(TopNWords);
        }

        private IEnumerable<string> FindAllwords(IEnumerable<string> text)
        {
            var delims = new[] {'.', ',', ';', ' ', '\n', '?', '!', ':', '(', ')', '[', ']', '{', '}', '\'', '"', '–'};
            return text.SelectMany(x => x.Split(delims, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.ToLower())
                .Where(y => y.Length > MinWordLength);
        }

        private IEnumerable<Word> CountWords(IEnumerable<string> words)
        {
            return words
                .GroupBy(x => x)
                .Select(y => new Word(y.Key, y.Count()))
                .OrderByDescending(z => z.Count);
        }
    }
}