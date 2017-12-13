using TagCloudGui;
using TagsCloud;

namespace TagCloudApplication.UI
{
    public class GraphicUi : IUi
    {
        private readonly TagCloudHelper helper;
        public GraphicUi(TagCloudHelper tagCloudHelper) => helper = tagCloudHelper;
        public void Run()
        {
            var app = new App();
            app.Run(new TagCloudWindow(helper));
        }
    }
}