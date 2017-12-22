using System.Collections.Generic;
using TagsCloud.Infrastructure;

namespace TagsCloud.TextAnalyzing
{
    public interface IWordConverter
    {
        Result<List<string>> ConvertWords(List<string> words);
    }

    public class SimpleWordConverter : IWordConverter
    {
        public Result<List<string>> ConvertWords(List<string> words) => words;
    }
}