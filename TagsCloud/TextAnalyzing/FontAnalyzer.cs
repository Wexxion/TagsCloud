using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.TextAnalyzing
{
    public interface IFontAnalyzer
    {
        IEnumerable<Word> SetFontForWords(IEnumerable<Word> words, int minFontSize, int maxFontSize, string fontFamily);
    }
    public class FontAnalyzer : IFontAnalyzer
    {
        public IEnumerable<Word> SetFontForWords(IEnumerable<Word> words, int minFontSize, int maxFontSize, string fontFamily)
        {
            foreach (var word in words)
            {
                var weight = (Math.Log(word.Count) - Math.Log(minFontSize)) /
                           (Math.Log(maxFontSize) - Math.Log(minFontSize));
                var size = (int)(minFontSize + (maxFontSize - minFontSize) * weight);
                var fontSize = Math.Max(minFontSize, size);
                word.Font = new Font(fontFamily, fontSize);
                yield return word;
            }
        }
    }
}