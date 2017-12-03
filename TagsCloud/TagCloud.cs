using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter;
using TagsCloud.TextAnalyzing;
using TagsCloud.Vizualization;

namespace TagsCloud
{
    public class TagCloud
    {
        private readonly ITagCloudLayouter layouter;
        private readonly IFontAnalyzer fontAnalyzer;
        private readonly ITextAnalyzer textAnalyzer;
        private readonly IWordFilter wordFilter;
        private readonly IWordConverter wordConverter;
        private readonly TagCloudVizualizer vizualizer;

        public TagCloud(ITagCloudLayouter layouter, IFontAnalyzer fontAnalyzer, 
            ITextAnalyzer textAnalyzer, IWordFilter wordFilter, 
            IWordConverter wordConverter, TagCloudVizualizer vizualizer)
        {
            this.layouter = layouter;
            this.fontAnalyzer = fontAnalyzer;
            this.textAnalyzer = textAnalyzer;
            this.wordFilter = wordFilter;
            this.wordConverter = wordConverter;
            this.vizualizer = vizualizer;
        }

        public Bitmap DrawTagCloud(IEnumerable<string> text, HashSet<string> boringWords, int topNWords, 
            int minWordLength, int minFontSize, int maxFontSize, string fontFamily, bool randomColors)
        {
            var sortedWords = textAnalyzer.GetSortedWords(text, topNWords, minWordLength);
            var filteredWords = wordFilter.FilterWords(sortedWords, boringWords);
            var convertedWords = wordConverter.ConvertWords(filteredWords);
            var words = fontAnalyzer.SetFontForWords(convertedWords, minFontSize, maxFontSize, fontFamily);

            var components = new List<ILayoutComponent<Word>>();
            foreach (var word in words)
            {
                var component = new WordLayoutComponent(word);
                component.LayoutRectangle = layouter.PutNextRectangle(component.Size);
                components.Add(component);
            }
            layouter.Clear();
            return vizualizer.DrawTagCloud(components, randomColors);
        }
    }
}