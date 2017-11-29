using System.Collections.Generic;

namespace TagsCloud.TextAnalyzing.Interfaces
{
    public interface IWordFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}