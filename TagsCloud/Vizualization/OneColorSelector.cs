using System.Drawing;
using TagsCloud.Layouter;

namespace TagsCloud.Vizualization
{
    public class OneColorSelector : IColorSelector
    {
        private readonly Brush brush;
        public OneColorSelector(Brush brush) => this.brush = brush;
        public void SetColorFor<T>(ILayoutComponent<T> component) => component.Brush = brush;
    }
}