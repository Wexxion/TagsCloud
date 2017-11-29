using System.Collections.Generic;

namespace TagsCloud.TextAnalyzing.Interfaces
{
    public interface IWordConverter
    {
        IEnumerable<string> ConvertWords(IEnumerable<string> words);
    }
}