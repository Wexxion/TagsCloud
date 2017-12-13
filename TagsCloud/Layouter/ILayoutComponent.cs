using System.Drawing;

namespace TagsCloud.Layouter
{
    public interface ILayoutComponent<out TComponent>
    {
        TComponent Component { get; }
        Size Size { get; }
        Rectangle LayoutRectangle { get; set; }
        Brush Brush { get; set; }
    }
}