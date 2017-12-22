using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.TextAnalyzing
{
    public interface IFontAnalyzer
    {
        Result<List<Word>> SetFontForWords(List<Word> words, int minFontSize, int maxFontSize, string fontFamily);
    }
    public class FontAnalyzer : IFontAnalyzer
    {
        public Result<List<Word>> SetFontForWords(List<Word> words, int minFontSize, int maxFontSize, string fontFamily)
        {
            return Result.Of(() =>
            {
                foreach (var word in words)
                {
                    var weight = (Math.Log(word.Count) - Math.Log(minFontSize)) /
                                 (Math.Log(maxFontSize) - Math.Log(minFontSize));
                    var size = (int)(minFontSize + (maxFontSize - minFontSize) * weight);
                    var fontSize = Math.Max(minFontSize, size);
                    word.Font = new Font(fontFamily, fontSize);
                }
                return words;
            });
        }
    }
}