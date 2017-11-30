using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.TextAnalyzing.Interfaces;

namespace TagsCloud.TextAnalyzing
{
    public class FontAnalyzer : IFontAnalyzer
    {
        public int MaxFontSize { get; set; } = 72;
        public int MinFontSize { get; set; } = 28;
        public string FontFamily { get; set; } = "Calibri";

        public IEnumerable<Word> SetFontForWords(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                var weight = (Math.Log(word.Count) - Math.Log(MinFontSize)) /
                           (Math.Log(MaxFontSize) - Math.Log(MinFontSize));
                var size = (int)(MinFontSize + (MaxFontSize - MinFontSize) * weight);
                var fontSize = Math.Max(MinFontSize, size);
                word.Font = new Font(FontFamily, fontSize);
                yield return word;
            }
        }
    }
}