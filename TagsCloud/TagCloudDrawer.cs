using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;
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

        public Result<Bitmap> DrawTagCloud(List<Word> allWords)
        {
            var image = allWords.AsResult()
                .Then(words => words.Select(word => new WordLayoutComponent(word)).ToList())
                .Then(components => colorSelector.SetColorsFor(components))
                .Then(SetLayoutRectangles)
                .Then(components => vizualizer.DrawTagCloud(components));
            layouter.Clear();
            return image;
        }

        private List<WordLayoutComponent> SetLayoutRectangles(List<WordLayoutComponent> components)
        {
            foreach (var component in components)
                component.LayoutRectangle = layouter.PutNextRectangle(component.Size);
            return components;
        }
    }
}