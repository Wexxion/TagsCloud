using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud.TextAnalyzing.Interfaces
{
    public interface IWordFilter
    {
        ITextReader TextReader { get; }
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}