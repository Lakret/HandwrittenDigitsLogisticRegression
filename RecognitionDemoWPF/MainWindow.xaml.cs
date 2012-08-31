using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

//using Interface;

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
            //var rtb = new RenderTargetBitmap(300, 300, 96d, 96d, PixelFormats.Default);
            //rtb.Render(inkCanvas1);
            //var stream = new System.IO.MemoryStream();
            //var pixels = new int[300 * 300];
            ////BitmapFrame.Create(rtb).CopyPixels(pixels, 300, 89999);

            //Interface.Reshaper.Reshape();

        }

        //private static double[] ConvertToRecognizerFormat(Image image)
        //{
        //    var small = (Bitmap)image.GetThumbnailImage(16, 16, () => false, IntPtr.Zero);
        //    var pixels = new double[16 * 16];
        //    for (var x = 0; x < 16; x++)
        //    {
        //        for (var y = 0; y < 16; y++)
        //        {
        //            var idx = (x + 1) * (y + 1) - 1;
        //            if (small.GetPixel(x, y) == Color.Black)
        //            {
        //                pixels[idx] = 1.0;
        //            }
        //            else
        //            {
        //                pixels[idx] = 0.0;
        //            }
        //        }
        //    }
        //    return pixels;
        //}
    }
}
