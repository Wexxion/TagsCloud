using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordCounter
    {
        Result<List<Word>> CountWords(List<string> words, int topNWords);
    }

    public class WordCounter : IWordCounter
    {
        public Result<List<Word>> CountWords(List<string> words, int topNWords)
            => Result.Of(() =>
                words.GroupBy(x => x)
                    .Select(y => new Word(y.Key, y.Count()))
                    .OrderByDescending(z => z.Count)
                    .Take(topNWords)
                    .ToList());
    }
}