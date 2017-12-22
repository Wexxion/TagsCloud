using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter;

namespace TagsCloud.Vizualization
{
    public class OneColorSelector : IColorSelector
    {
        private readonly Brush brush;
        public OneColorSelector(Brush brush) => this.brush = brush;

        public List<WordLayoutComponent> SetColorsFor(List<WordLayoutComponent> components)
        {
            foreach (var component in components)
                component.Brush = brush;
            return components;
        }
    }
}