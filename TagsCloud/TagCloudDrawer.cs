using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter;
using TagsCloud.TextAnalyzing;
using TagsCloud.Vizualization;

namespace TagsCloud
{
    public class TagCloudDrawer
    {
        private readonly ITagCloudLayouter layouter;
        private readonly IColorSelector colorSelector;
        private readonly TagCloudVizualizer vizualizer;

        public TagCloudDrawer(TagCloudVizualizer vizualizer, ITagCloudLayouter layouter, IColorSelector colorSelector)
        {
            this.vizualizer = vizualizer;
            this.layouter = layouter;
            this.colorSelector = colorSelector;
        }

        public Bitmap DrawTagCloud(IEnumerable<Word> words)
        {
            var components = new List<ILayoutComponent<Word>>();
            foreach (var word in words)
            {
                var component = new WordLayoutComponent(word);
                colorSelector.SetColorFor(component);
                component.LayoutRectangle = layouter.PutNextRectangle(component.Size);
                components.Add(component);
            }
            layouter.Clear();
            return vizualizer.DrawTagCloud(components);
        }
    }
}