using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autofac;
using Microsoft.Win32;
using TagsCloud;
using TagsCloud.Infrastructure;
using TagsCloud.TextAnalyzing;
using TagsCloud.Vizualization;
using Point = System.Drawing.Point;

namespace TagCloudGui
{
    public partial class TagCloudWindow
    {
        private ITextReader textReader;
        private TagCloudTextAnalyzer textAnalyzer;
        private TagCloudDrawer drawer;


        public TagCloudSettings Settings { get; }
        public bool RandomColors { get; set; } = true;
        public Brush WordColor = Brushes.Black;
        public string BoringWordsPath { get; set; }

        public TagCloudWindow()
        {
            Settings = new TagCloudSettings();
            InitializeComponent();
        }

        private void OnOpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"};
            if (openFileDialog.ShowDialog() == true)
                Settings.InputPath = openFileDialog.FileName;
        }

        private void OnSaveFile(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog {Filter = "Png image (*.png)|*png" };
            if (saveFileDialog.ShowDialog() == true)
                Settings.OutputPath = saveFileDialog.FileName;
        }

        private void OnCustomization(object sender, RoutedEventArgs e)
        {
            var customizationWindow = new CustomizationWindow(this);
            customizationWindow.Show();
        }

        private void OnDrawing(object sender, RoutedEventArgs e)
        {
            InitializeTypes();
            var text = textReader.ReadText(Settings.InputPath);
            var words = textAnalyzer.GetWords(text, Settings.TopNWords, Settings.MinWordLength,
                Settings.MinFontSize, Settings.MaxFontSize, Settings.FontFamily);
            var bitmap = drawer.DrawTagCloud(words);
            var img = new Image {Stretch = Stretch.Uniform, StretchDirection = StretchDirection.Both};
            img.BeginInit();
            img.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            img.Width = Canvas.ActualWidth;
            img.Height = Canvas.ActualHeight;
            img.EndInit();
            Canvas.Children.Add(img);
        }

        private void InitializeTypes()
        {
            var container = RegisterTypes();
            textReader = container.Resolve<ITextReader>();
            textAnalyzer = container.Resolve<TagCloudTextAnalyzer>();
            drawer = container.Resolve<TagCloudDrawer>();
        }

        private IContainer RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterAssemblyTypes(typeof(TagCloudSettings).Assembly)
                .Except<IWordFilter>()
                .Except<IWordConverter>()
                .Except<IColorSelector>()
                .WithParameter("center", Point.Empty)
                .AsSelf()
                .AsImplementedInterfaces();

            RegisterColorSelector(builder);
            RegisterWordFilters(builder);
            RegisterWordConverters(builder);

            return builder.Build();
        }

        private void RegisterColorSelector(ContainerBuilder builder)
        {
            if (RandomColors)
                builder.RegisterType<RandomColorSelector>().As<IColorSelector>();
            else
                builder.RegisterType<OneColorSelector>().As<IColorSelector>().WithParameter("brush", WordColor);
        }

        private void RegisterWordFilters(ContainerBuilder builder)
        {
            var boringWords = File.ReadLines(BoringWordsPath).ToHashSet();
            builder.RegisterType<SimpleWordFilter>().As<IWordFilter>().WithParameter("boringWords", boringWords);
        }

        private void RegisterWordConverters(ContainerBuilder builder)
        {
            builder.RegisterType<SimpleWordConverter>().As<IWordConverter>();
        }
    }
}
