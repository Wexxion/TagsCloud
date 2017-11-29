using System.Collections.Generic;

namespace TagsCloud.TextAnalyzing.Interfaces
{
    public interface ITextAnalyzer
    {
        IEnumerable<Word> GetSortedWords(IEnumerable<string> text);

        int TopNWords { get; set; }
        int MinWordLength { get; set; }
    }
}
