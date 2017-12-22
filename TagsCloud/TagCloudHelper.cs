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

        public Result<List<string>> GetText() => textReader.ReadText(Settings.InputPath);

        public Result<List<Word>> GetWords(List<string> text) => textAnalyzer.GetWords(text, Settings.TopNWords,
            Settings.MinWordLength, Settings.MinFontSize, Settings.MaxFontSize, Settings.FontFamily);

        public Result<Bitmap> GetTagCloudBitmap(List<Word> words) => drawer.DrawTagCloud(words);

        public Result<Bitmap> SaveImage(Bitmap image) => imageSaver.SaveImage(image, Settings.OutputPath);
    }
}