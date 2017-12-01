using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter;
using TagsCloud.Layouter.Interfaces;
using TagsCloud.TextAnalyzing;
using TagsCloud.TextAnalyzing.Interfaces;
using TagsCloud.Vizualization;

namespace TagsCloud
{
    public class TagCloud
    {
        public ITagCloudLayouter Layouter { get; }
        public IFontAnalyzer FontAnalyzer { get; }
        public ITextAnalyzer TextAnalyzer { get; }
        public TagCloudVizualizer Vizualizer { get; }

        public TagCloud(ITagCloudLayouter layouter, IFontAnalyzer fontAnalyzer, 
            ITextAnalyzer textAnalyzer, TagCloudVizualizer vizualizer)
        {
            Layouter = layouter;
            FontAnalyzer = fontAnalyzer;
            TextAnalyzer = textAnalyzer;
            Vizualizer = vizualizer;
        }

        public Bitmap DrawTagCloud(IEnumerable<string> text)
        {
            var words = TextAnalyzer.GetSortedWords(text);
            words = FontAnalyzer.SetFontForWords(words);
            var components = new List<ILayoutComponent<Word>>();
            foreach (var word in words)
            {
                var component = new WordLayoutComponent(word);
                component.LayoutRectangle = Layouter.PutNextRectangle(component.Size);
                components.Add(component);
            }
            Layouter.Clear();
            return Vizualizer.DrawTagCloud(components);
        }
    }
}