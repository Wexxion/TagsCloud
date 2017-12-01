using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagCloudGui
{
    public partial class TagCloudWindow
    {
        private readonly TagCloudHelper tagCloudHelper;

        public TagCloudWindow(ITextReader textReader, TagCloud tagCloud, IImageSaver imageSaver)
        {
            tagCloudHelper = new TagCloudHelper(textReader, tagCloud, imageSaver);
            InitializeComponent();
        }

        private void OnOpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"};
            if (openFileDialog.ShowDialog() == true)
                tagCloudHelper.OpenFile(openFileDialog.FileName);
        }

        private void OnSaveFile(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog {Filter = "Png image (*.png)|*png" };
            if (saveFileDialog.ShowDialog() == true)
                tagCloudHelper.SaveFile(saveFileDialog.FileName);
        }

        private void OnCustomization(object sender, RoutedEventArgs e)
        {
            var customizationWindow = new CustomizationWindow(tagCloudHelper);
            customizationWindow.Show();
        }

        private void OnDrawing(object sender, RoutedEventArgs e)
        {
            var bitmap = tagCloudHelper.DrawTagCould();
            var img = new Image {Stretch = Stretch.Uniform, StretchDirection = StretchDirection.Both};
            img.BeginInit();
            img.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            img.Width = canvas.ActualWidth;
            img.Height = canvas.ActualHeight;
            img.EndInit();
            canvas.Children.Add(img);
        }
    }
}
