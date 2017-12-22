using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordFilter
    {
        Result<List<string>> FilterWords(List<string> words);
    }

    public class SimpleWordFilter : IWordFilter
    {
        private readonly HashSet<string> boringWords;

        public SimpleWordFilter(HashSet<string> boringWords) => this.boringWords = boringWords;

        public Result<List<string>> FilterWords(List<string> words)
            => words.Where(word => !boringWords.Contains(word) && word.Length > 3).ToList();
    }
}