using System.Collections.Generic;
using TagsCloud.TextAnalyzing.Interfaces;

namespace TagsCloud.TextAnalyzing
{
    public class SimpleWordConverter : IWordConverter
    {
        public IEnumerable<string> ConvertWords(IEnumerable<string> words) => words;
    }
}