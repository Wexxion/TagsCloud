using TagCloudGui;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagCloudApplication
{
    public class GraphicUi : IUi
    {
        private readonly ITextReader textReader;
        private readonly TagCloud tagCloud;
        private readonly IImageSaver imageSaver;

        public GraphicUi(ITextReader textReader, TagCloud tagCloud, IImageSaver imageSaver)
        {
            this.textReader = textReader;
            this.tagCloud = tagCloud;
            this.imageSaver = imageSaver;
        }
        public void Run()
        {
            var app = new App();
            app.Run(new TagCloudWindow(textReader, tagCloud, imageSaver));
        }
    }
}