using System.Collections.Generic;

namespace TagsCloud.TextAnalyzing.Interfaces
{
    public interface IFontAnalyzer
    {
        IEnumerable<Word> SetFontForWords(IEnumerable<Word> words);

        int MaxFontSize { get; set; }
        int MinFontSize { get; set; }
        string FontFamily { get; set; } 
    }
}
