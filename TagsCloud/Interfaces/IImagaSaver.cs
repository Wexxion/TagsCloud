using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IImagaSaver
    {
        void SaveImage(Bitmap bitmap, string filepath);
    }
}