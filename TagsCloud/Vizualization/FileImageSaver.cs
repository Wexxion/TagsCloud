using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.Vizualization
{
    public interface IImageSaver
    {
        Result<Bitmap> SaveImage(Bitmap bitmap, string filepath);
    }

    public class FileImageSaver : IImageSaver
    {
        public Result<Bitmap> SaveImage(Bitmap bitmap, string filepath)
        {
            return Result.Of(() =>
            {
                bitmap.Save($"{filepath}.png");
                return bitmap;
            });
        }
    }
}