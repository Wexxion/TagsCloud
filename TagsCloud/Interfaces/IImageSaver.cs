using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IImageSaver
    {
        void SaveImage(Bitmap bitmap, string filepath);
    }
}