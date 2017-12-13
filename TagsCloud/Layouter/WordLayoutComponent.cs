using System.Drawing;
using System.Windows.Forms;
using TagsCloud.TextAnalyzing;

namespace TagsCloud.Layouter
{
    public class WordLayoutComponent : ILayoutComponent<Word>
    {
        public WordLayoutComponent(Word word)
        {
            Component = word;
            Size = TextRenderer.MeasureText(word.Value, word.Font);
        }

        public Word Component { get; }
        public Size Size { get; }
        public Rectangle LayoutRectangle { get; set; }
        public Brush Brush { get; set; }
    }
}