using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordCounter
    {
        IEnumerable<Word> CountWords(IEnumerable<string> words, int topNWords);
    }

    public class WordCounter : IWordCounter
    {
        public IEnumerable<Word> CountWords(IEnumerable<string> words, int topNWords) 
            => words
                .GroupBy(x => x)
                .Select(y => new Word(y.Key, y.Count()))
                .OrderByDescending(z => z.Count)
                .Take(topNWords);
    }
}