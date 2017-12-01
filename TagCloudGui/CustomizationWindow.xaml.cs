using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace TagCloudGui
{

    public partial class CustomizationWindow
    {
        private readonly TagCloudHelper tagCloudHelper;
        
        public int TopNWords { get; set; }
        public int MinWordLength { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }
        public string Font { get; set; }

        public CustomizationWindow(TagCloudHelper tagCloudHelper)
        {
            this.tagCloudHelper = tagCloudHelper;
            TopNWords = tagCloudHelper.tagCloud.TextAnalyzer.TopNWords;
            MinWordLength = tagCloudHelper.tagCloud.TextAnalyzer.MinWordLength;
            MinFontSize = tagCloudHelper.tagCloud.FontAnalyzer.MinFontSize;
            MaxFontSize = tagCloudHelper.tagCloud.FontAnalyzer.MaxFontSize;
            Font = tagCloudHelper.tagCloud.FontAnalyzer.FontFamily;

            DataContext = this;
            InitializeComponent();
        }

        private void SelectFont(object sender, RoutedEventArgs e)
        {
            var fd = new FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) return;
            Font = fd.Font.Name;
            SelectedFont.FontFamily = new System.Windows.Media.FontFamily(fd.Font.Name);
            SelectedFont.Text = Font;
        }

        private void UseRandomColors(object sender, RoutedEventArgs e)
        {
            
        }

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            tagCloudHelper.tagCloud.TextAnalyzer.TopNWords = TopNWords;
            tagCloudHelper.tagCloud.TextAnalyzer.MinWordLength = MinWordLength;
            tagCloudHelper.tagCloud.FontAnalyzer.MinFontSize = MinFontSize;
            tagCloudHelper.tagCloud.FontAnalyzer.MaxFontSize = MaxFontSize;
            tagCloudHelper.tagCloud.FontAnalyzer.FontFamily = Font;
            Hide();
        }

        private void OpenFilterFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() != true) return;
            tagCloudHelper.tagCloud.TextAnalyzer.WordFilter.TextReader.Filepath = openFileDialog.FileName;
            var filename = openFileDialog.FileName.Split('\\');
            FilterFile.Text = filename[filename.Length - 1];
        }
    }
}
