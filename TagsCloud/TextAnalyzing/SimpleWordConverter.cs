using System.Collections.Generic;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordConverter
    {
        IEnumerable<string> ConvertWords(IEnumerable<string> words);
    }

    public class SimpleWordConverter : IWordConverter
    {
        public IEnumerable<string> ConvertWords(IEnumerable<string> words) => words;
    }
}