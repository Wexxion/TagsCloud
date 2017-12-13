using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;
using TagsCloud.TextAnalyzing;
using TagsCloud.Vizualization;

namespace TagsCloud
{
    public class TagCloudHelper
    {
        public TagCloudSettings Settings { get; }
        private readonly ITextReader textReader;
        private readonly TagCloudTextAnalyzer textAnalyzer;
        private readonly TagCloudDrawer drawer;
        private readonly IImageSaver imageSaver;

        public TagCloudHelper(ITextReader textReader, TagCloudTextAnalyzer textAnalyzer, TagCloudDrawer drawer,
            IImageSaver imageSaver)
        {
            this.textReader = textReader;
            this.textAnalyzer = textAnalyzer;
            this.drawer = drawer;
            this.imageSaver = imageSaver;
            Settings = new TagCloudSettings();
        }

        public IEnumerable<string> GetText() => textReader.ReadText(Settings.InputPath);

        public IEnumerable<Word> GetWords(IEnumerable<string> text) => textAnalyzer.GetWords(text, Settings.TopNWords,
            Settings.MinWordLength, Settings.MinFontSize, Settings.MaxFontSize, Settings.FontFamily);

        public Bitmap GetTagCloudBitmap(IEnumerable<Word> words) => drawer.DrawTagCloud(words);

        public void SaveImage(Bitmap image) => imageSaver.SaveImage(image, Settings.OutputPath);
    }
}