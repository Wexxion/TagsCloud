using System.Collections.Generic;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordConverter
    {
        IEnumerable<Word> ConvertWords(IEnumerable<Word> words);
    }

    public class SimpleWordConverter : IWordConverter
    {
        public IEnumerable<Word> ConvertWords(IEnumerable<Word> words) => words;
    }
}