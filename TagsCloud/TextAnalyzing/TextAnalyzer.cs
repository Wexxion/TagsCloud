using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextAnalyzing
{
    public interface ITextAnalyzer
    {
        IEnumerable<string> GetWords(IEnumerable<string> text, int minWordLength);
    }

    public class TextAnalyzer : ITextAnalyzer
    {
        public IEnumerable<string> GetWords(IEnumerable<string> text, int minWordLength)
        {
            var delims = new[] { '.', ',', ';', ' ', '\n', '?', '!', ':', '(', ')', '[', ']', '{', '}', '\'', '"', '–', '=', '-' };
            return text.SelectMany(x => x.Split(delims, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.ToLower())
                .Where(y => y.Length > minWordLength);
        }
    }
}