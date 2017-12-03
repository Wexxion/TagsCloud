using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;
using TagsCloud.Vizualization;

namespace TagsCloud
{
    public class TagCloudHelper
    {
        private readonly ITextReader textReader;
        private readonly TagCloud tagCloud;
        private readonly IImageSaver imageSaver;

        public int TopNWords { get; set; } = 250;
        public int MinWordLength { get; set; } = 3;
        public int MinFontSize { get; set; } = 20;
        public int MaxFontSize { get; set; } = 72;
        public string FontFamily { get; set; } = "Calibri";
        public bool RandomColors { get; set; } = true;
        public string InputPath { get; set; } = null;
        public string OutputPath { get; set; } = null;
        public string BoringWordsPath { get; set; } = null;

        public TagCloudHelper(ITextReader textReader, TagCloud tagCloud, IImageSaver imageSaver)
        {
            this.textReader = textReader;
            this.tagCloud = tagCloud;
            this.imageSaver = imageSaver;
        }

        public IEnumerable<string> GetText() => textReader.ReadText(InputPath);
        public HashSet<string> GetBoringWords() 
            => BoringWordsPath == null ? new HashSet<string>() : textReader.ReadText(BoringWordsPath).ToHashSet();

        public void Save(Bitmap image) => imageSaver.SaveImage(image, OutputPath);
        public Bitmap DrawTagCould(IEnumerable<string> text, HashSet<string> boringWords) 
            => tagCloud.DrawTagCloud(text, boringWords, TopNWords, MinWordLength, MinFontSize, MaxFontSize, FontFamily, RandomColors);
    }
}