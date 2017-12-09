using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }

    public class SimpleWordFilter : IWordFilter
    {
        private readonly HashSet<string> boringWords;

        public SimpleWordFilter(HashSet<string> boringWords) => this.boringWords = boringWords;

        public IEnumerable<string> FilterWords(IEnumerable<string> words) 
            => words.Where(word => !boringWords.Contains(word) && word.Length > 3);
    }
}