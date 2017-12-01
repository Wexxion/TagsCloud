using System.Collections.Generic;
using System.Drawing;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagCloudGui
{
    public class TagCloudHelper
    {
        private readonly IImageSaver imageSaver;
        private readonly ITextReader textReader;
        private Bitmap bitmap;
        private IEnumerable<string> text;

        public readonly TagCloud tagCloud;

        public TagCloudHelper(ITextReader textReader, TagCloud tagCloud, IImageSaver imageSaver)
        {
            this.textReader = textReader;
            this.tagCloud = tagCloud;
            this.imageSaver = imageSaver;
        }

        public void OpenFile(string filepath)
        {
            textReader.Filepath = filepath;
            text = textReader.ReadText();
        }

        public Bitmap DrawTagCould() => bitmap = tagCloud.DrawTagCloud(text);

        public void SaveFile(string filepath) => imageSaver.SaveImage(bitmap, filepath);
    }
}