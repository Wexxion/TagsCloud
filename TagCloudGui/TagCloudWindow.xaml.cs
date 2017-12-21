using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using TagsCloud;
using TagsCloud.Infrastructure;
using Image = System.Windows.Controls.Image;

namespace TagCloudGui
{
    public partial class TagCloudWindow
    {
        private readonly TagCloudHelper helper;
        private Result<Bitmap> bitmap;

        public TagCloudWindow(TagCloudHelper tagCloudHelper)
        {
            helper = tagCloudHelper;
            InitializeComponent();
        }

        private void OnOpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == true)
                helper.Settings.InputPath = openFileDialog.FileName;
        }

        private void OnSaveFile(object sender, RoutedEventArgs e)
        {
            if (!bitmap.IsSuccess)
            {
                MessageBox.Show("Невозможно сохранить несуществующее избражение!",
                    "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var saveFileDialog = new SaveFileDialog { Filter = "Png image (*.png)|*png" };
            if (saveFileDialog.ShowDialog() == true)
                helper.Settings.OutputPath = saveFileDialog.FileName;
            helper.SaveImage(bitmap.GetValueOrThrow());
        }

        private void OnCustomization(object sender, RoutedEventArgs e)
        {
            var customizationWindow = new CustomizationWindow(helper);
            customizationWindow.Show();
        }

        private void OnDrawing(object sender, RoutedEventArgs e)
        {
            bitmap = helper.GetText()
                .Then(text => helper.GetWords(text))
                .RefineError("Can't get words: ")
                .Then(words => helper.GetTagCloudBitmap(words))
                .RefineError("Can't visualize words: ")
                .OnFail(error => MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error));
            if (!bitmap.IsSuccess) return;
            var img = new Image { Stretch = Stretch.Uniform, StretchDirection = StretchDirection.Both };
            img.BeginInit();
            img.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetValueOrThrow().GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            img.Width = Canvas.ActualWidth;
            img.Height = Canvas.ActualHeight;
            img.EndInit();
            Canvas.Children.Add(img);
        }
    }
}