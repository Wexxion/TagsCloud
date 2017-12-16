using TagsCloud.Layouter;

namespace TagsCloud.Vizualization
{
    public interface IColorSelector
    {
        void SetColorFor<T>(ILayoutComponent<T> component);
    }
}