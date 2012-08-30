using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecognitionDemoWPF
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var rtb = new RenderTargetBitmap(300, 300, 96d, 96d, PixelFormats.Default);
            rtb.Render(inkCanvas1);
            var bmp = new Image {Source = rtb};
            
        }

        private static double[] ConvertToRecognizerFormat(Image image)
        {
            var small = (Bitmap)image.GetThumbnailImage(16, 16, () => false, IntPtr.Zero);
            var pixels = new double[16 * 16];
            for (var x = 0; x < 16; x++)
            {
                for (var y = 0; y < 16; y++)
                {
                    var idx = (x + 1) * (y + 1) - 1;
                    if (small.GetPixel(x, y) == Color.Black)
                    {
                        pixels[idx] = 1.0;
                    }
                    else
                    {
                        pixels[idx] = 0.0;
                    }
                }
            }
            return pixels;
        }
    }
}
