using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.TextAnalyzing
{
    public interface ITextAnalyzer
    {
        Result<List<string>> GetWords(List<string> text, int minWordLength);
    }

    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly char[] delims =
        {
            '.', ',', ';', ' ', '\n', '?', '!', ':', '(', ')', '[', ']',
            '{', '}', '\'', '"', '–', '=', '-'
        };

        public Result<List<string>> GetWords(List<string> text, int minWordLength)
        {
            return Result.Of(() =>
                text.SelectMany(x => x.Split(delims, StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => x.ToLower())
                    .Where(y => y.Length > minWordLength)
                    .ToList());
        }
    }
}