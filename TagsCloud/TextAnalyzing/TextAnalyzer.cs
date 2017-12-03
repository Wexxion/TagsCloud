using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextAnalyzing
{
    public interface ITextAnalyzer
    {
        IEnumerable<Word> GetSortedWords(IEnumerable<string> text, int topNWords, int minWordLength);
    }

    public class TextAnalyzer : ITextAnalyzer
    {
        public IEnumerable<Word> GetSortedWords(IEnumerable<string> text, int topNWords, int minWordLength)
        {
            var allWords = FindAllwords(text, minWordLength);
            return topNWords == 0
                ? CountWords(allWords)
                : CountWords(allWords).Take(topNWords);
        }

        private IEnumerable<string> FindAllwords(IEnumerable<string> text, int minWordLength)
        {
            var delims = new[] {'.', ',', ';', ' ', '\n', '?', '!', ':', '(', ')', '[', ']', '{', '}', '\'', '"', '–', '=', '-'};
            return text.SelectMany(x => x.Split(delims, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.ToLower())
                .Where(y => y.Length > minWordLength);
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