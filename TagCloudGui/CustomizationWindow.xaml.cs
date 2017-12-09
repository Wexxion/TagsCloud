using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using TagsCloud;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace TagCloudGui
{
    public partial class CustomizationWindow
    {
        private readonly TagCloudWindow parent;

        public CustomizationWindow(TagCloudWindow parent)
        {
            this.parent = parent;
            DataContext = parent;
            InitializeComponent();
            UpdateSelectedFont(parent.Settings.FontFamily);
            CheckBox.IsChecked = parent.RandomColors;
        }

        private void SelectFont(object sender, RoutedEventArgs e)
        {
            var fd = new FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) return;
            parent.Settings.FontFamily = fd.Font.Name;
            UpdateSelectedFont(fd.Font.Name);
        }

        private void UpdateSelectedFont(string fontFamily)
        {
            SelectedFont.FontFamily = new System.Windows.Media.FontFamily(fontFamily);
            SelectedFont.Text = parent.Settings.FontFamily;
        }

        private void UseRandomColors(object sender, RoutedEventArgs e) 
            => parent.RandomColors = !parent.RandomColors;

        private void SaveSettings(object sender, RoutedEventArgs e) => Hide();

        private void OpenFilterFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() != true) return;
            parent.BoringWordsPath = openFileDialog.FileName;
            var filename = openFileDialog.FileName.Split('\\');
            FilterFile.Text = filename[filename.Length - 1];
        }
    }
}
