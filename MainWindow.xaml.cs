using DevExpress.BarCodes;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BarcodeGeneratorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateBarCode()
        {
            try
            {
                if (ComboSymbologies.SelectedItem == null || string.IsNullOrWhiteSpace(BarcodeBox.Text))
                {
                    return;
                }
                BarCode barCode = new BarCode
                {
                    CodeText = BarcodeBox.Text.ToUpper(),
                    Symbology = (Symbology)ComboSymbologies.SelectedItem
                };
                using (MemoryStream stream = new MemoryStream())
                {
                    barCode.BarCodeImage.Save(stream, ImageFormat.Png);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    BarCodeImage.Source = image;
                }
            }
            catch (Exception) { }
        }

        private void OnSymbologyChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateBarCode();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            GenerateBarCode();
        }
    }
}
