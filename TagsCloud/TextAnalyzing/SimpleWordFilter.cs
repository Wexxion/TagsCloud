using System.Collections.Generic;
using TagsCloud.TextAnalyzing.Interfaces;

namespace TagsCloud.TextAnalyzing
{
    public class SimpleWordFilter : IWordFilter
    {
        public IEnumerable<string> FilterWords(IEnumerable<string> words) => words;
    }
}