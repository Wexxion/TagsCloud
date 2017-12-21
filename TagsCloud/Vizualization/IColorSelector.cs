using System.Collections.Generic;
using TagsCloud.Layouter;

namespace TagsCloud.Vizualization
{
    public interface IColorSelector
    {
        List<WordLayoutComponent> SetColorsFor(List<WordLayoutComponent> components);
    }
}