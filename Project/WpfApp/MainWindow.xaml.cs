using System;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        public ContensTreeVM GetSelectedContentsItem() => _contentsTree.SelectedItem as ContensTreeVM;

        public void ShowImageFile(string path)
        {
            var source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(path);
            source.EndInit();
            var img = new Image { Source = source };
            new Window { Content = img, Width = Width, Height = Height }.Show();
        }

        public void ShowImageBinary(byte[][] bin)
        {
            int width = bin.Length;
            int height = bin[0].Length;
            BitmapSource bitmapSource;

            using (var bitmap = new System.Drawing.Bitmap(width, height))
            using (var gr = System.Drawing.Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < height; ++i)
                {
                    for (int j = 0; j < width; ++j)
                    {
                        int pixelColor = 255 - bin[i][j];
                        var c = System.Drawing.Color.FromArgb(pixelColor, pixelColor, pixelColor);
                        using (var sb = new System.Drawing.SolidBrush(c))
                        {
                            gr.FillRectangle(sb, j, i, 1, 1);
                        }
                    }
                }

                using (var ms = new System.IO.MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Bmp);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    bitmapSource = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }

            new Window { Content = new Image { Source = bitmapSource }, Width = Width, Height = Height }.Show();
        }
    }
}
