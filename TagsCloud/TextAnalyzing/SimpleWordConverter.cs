using System.Collections.Generic;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordConverter
    {
        List<string> ConvertWords(List<string> words);
    }

    public class SimpleWordConverter : IWordConverter
    {
        public List<string> ConvertWords(List<string> words) => words;
    }
}