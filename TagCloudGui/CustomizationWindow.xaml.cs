using System.Windows;
using System.Windows.Forms;
using TagsCloud;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace TagCloudGui
{
    public partial class CustomizationWindow
    {
        public TagCloudHelper Helper { get; }

        public CustomizationWindow(TagCloudHelper tagCloudHelper)
        {
            Helper = tagCloudHelper;
            DataContext = Helper.Settings;
            InitializeComponent();
            UpdateSelectedFont(Helper.Settings.FontFamily);
        }

        private void SelectFont(object sender, RoutedEventArgs e)
        {
            var fd = new FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) return;
            Helper.Settings.FontFamily = fd.Font.Name;
            UpdateSelectedFont(fd.Font.Name);
        }

        private void UpdateSelectedFont(string fontFamily)
        {
            SelectedFont.FontFamily = new System.Windows.Media.FontFamily(fontFamily);
            SelectedFont.Text = Helper.Settings.FontFamily;
        }
        private void SaveSettings(object sender, RoutedEventArgs e) => Hide();

        //private void UseRandomColors(object sender, RoutedEventArgs e) => Helper.RandomColors = !Helper.RandomColors;

        //private void OpenFilterFile(object sender, RoutedEventArgs e)
        //{
        //    var openFileDialog = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
        //    if (openFileDialog.ShowDialog() != true) return;
        //    Helper.BoringWordsPath = openFileDialog.FileName;
        //    var filename = openFileDialog.FileName.Split('\\');
        //    FilterFile.Text = filename[filename.Length - 1];
        //}
    }
}