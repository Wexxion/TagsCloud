using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using TagsCloud;

namespace TagCloudGui
{
    public partial class TagCloudWindow
    {
        private readonly TagCloudHelper helper;

        public TagCloudWindow(TagCloudHelper tagCloudHelper)
        {
            helper = tagCloudHelper;
            InitializeComponent();
        }

        private void OnOpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"};
            if (openFileDialog.ShowDialog() == true)
                helper.InputPath = openFileDialog.FileName;
        }

        private void OnSaveFile(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog {Filter = "Png image (*.png)|*png" };
            if (saveFileDialog.ShowDialog() == true)
                helper.OutputPath = saveFileDialog.FileName;
        }

        private void OnCustomization(object sender, RoutedEventArgs e)
        {
            var customizationWindow = new CustomizationWindow(helper);
            customizationWindow.Show();
        }

        private void OnDrawing(object sender, RoutedEventArgs e)
        {

            var text = helper.GetText();
            var boringWords = helper.GetBoringWords();
            var bitmap = helper.DrawTagCould(text, boringWords);
            var img = new Image {Stretch = Stretch.Uniform, StretchDirection = StretchDirection.Both};
            img.BeginInit();
            img.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            img.Width = Canvas.ActualWidth;
            img.Height = Canvas.ActualHeight;
            img.EndInit();
            Canvas.Children.Add(img);
        }
    }
}
