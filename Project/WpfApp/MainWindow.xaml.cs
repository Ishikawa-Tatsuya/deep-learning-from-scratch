using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        public ContensTreeVM GetSelectedContentsItem() => _contentsTree.SelectedItem as ContensTreeVM;

        public void ShowImage(string path)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(path);
            source.EndInit();
            var img = new Image { Source = source };
            new Window { Content = img, Width = Width, Height = Height }.Show();
        }
    }
}
