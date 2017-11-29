using System.Collections.Generic;
using System.Drawing;
using TagsCloud.TextAnalyzing.Interfaces;

namespace TagsCloud.TextAnalyzing
{
    public class FontAnalyzer : IFontAnalyzer
    {
        public int MaxFontSize { get; set; } = 72;
        public int MinFontSize { get; set; } = 20;
        public string FontFamily { get; set; } = "Calibri";

        public IEnumerable<Word> SetFontForWords(IEnumerable<Word> words)
        {
            var currentFontSize = MaxFontSize;
            var delta = MinFontSize * 2 / 5;
            foreach (var word in words)
            {
                word.Font = new Font(FontFamily, currentFontSize);
                if (currentFontSize - delta > MinFontSize) currentFontSize -= delta;
                yield return word;
            }
        }
    }
}