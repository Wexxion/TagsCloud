using System;
using System.Drawing;

namespace TagsCloud.Vizualization
{
    public interface IImageSaver
    {
        void SaveImage(Bitmap bitmap, string filepath);
    }
    public class FileImageSaver : IImageSaver
    {
        public void SaveImage(Bitmap bitmap, string filepath)
        {
            if (string.IsNullOrEmpty(filepath)) throw new ArgumentNullException();
            bitmap.Save($"{filepath}.png");
        }
    }
}