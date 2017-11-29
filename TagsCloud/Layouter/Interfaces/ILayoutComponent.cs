using System.Drawing;

namespace TagsCloud.Layouter.Interfaces
{
    public interface ILayoutComponent<out TComponent>
    {
        TComponent Component { get; }
        Size Size { get; }
        Rectangle LayoutRectangle { get; set; }
    }
}