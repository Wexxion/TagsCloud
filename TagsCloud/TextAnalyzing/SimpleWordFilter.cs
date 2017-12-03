using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordFilter
    {
        IEnumerable<Word> FilterWords(IEnumerable<Word> words, HashSet<string> boringWords);
    }

    public class SimpleWordFilter : IWordFilter
    {
        public IEnumerable<Word> FilterWords(IEnumerable<Word> words, HashSet<string> boringWords) 
            => words.Where(word => !boringWords.Contains(word.Value) && word.Value.Length > 3);
    }
}