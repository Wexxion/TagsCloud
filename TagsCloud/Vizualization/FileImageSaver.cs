using System;
using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud.Vizualization
{
    public class FileImageSaver : IImagaSaver
    {
        public void SaveImage(Bitmap bitmap, string filepath)
        {
            if (string.IsNullOrEmpty(filepath)) throw new ArgumentNullException();
            bitmap.Save($"{filepath}.png");
        }
    }
}