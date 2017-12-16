using System.Drawing;

namespace TagsCloud.TextAnalyzing
{
    public class Word
    {
        public Word(string value, int count)
        {
            Value = value;
            Count = count;
        }

        public string Value { get; }
        public int Count { get; }
        public Font Font { get; set; }
    }
}