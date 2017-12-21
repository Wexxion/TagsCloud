using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordFilter
    {
        List<string> FilterWords(List<string> words);
    }

    public class SimpleWordFilter : IWordFilter
    {
        private readonly HashSet<string> boringWords;

        public SimpleWordFilter(HashSet<string> boringWords) => this.boringWords = boringWords;

        public List<string> FilterWords(List<string> words)
            => words.Where(word => !boringWords.Contains(word) && word.Length > 3).ToList();
    }
}