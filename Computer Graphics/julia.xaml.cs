using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerGraphics
{
    /// <summary>
    /// Логика взаимодействия для julia.xaml
    /// </summary>
    public partial class julia : UserControl
    {
        public julia()
        {
            zoom = 1;
            InitializeComponent();
        }
        private double zoom;

        public void DrawFractal(int w, int h)
        {
            double cRe, cIm;
            double newRe, newIm, oldRe, oldIm;
            int maxIterations = 300;
            WriteableBitmap bit = new WriteableBitmap(w, h, 96, 96, PixelFormats.Rgb24, null);
            int r = (int)slider.Value, b = (int)slider2.Value;
            //cRe = -0.70176;
            //cIm = -0.3842;
            cRe = slider.Value * -1;
            cIm = slider2.Value * -1;
            bit.Lock();
            
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                {
                    
                    newRe = 1.5 * (x - w / 2) / (0.5 * zoom * w);
                    newIm = (y - h / 2) / (0.5 * zoom * h);

                    int i;

                    for (i = 0; i < maxIterations; i++)
                    {

                        oldRe = newRe;
                        oldIm = newIm;

                        newRe = oldRe * oldRe - oldIm * oldIm + cRe;
                        newIm = 2 * oldRe * oldIm + cIm;

                        if ((newRe * newRe + newIm * newIm) > 4) break;

                    }
                    byte[] pixel = { (byte)((i * 9) % 255), 0, (byte)((i * 9) % 255) };
                    bit.WritePixels(new Int32Rect(x, y, 1, 1), pixel, 3, 0);
                    
                }
            bit.Unlock();
            image.Source = bit;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

                DrawFractal((int)image.Width, (int)image.Height);      
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            zoom++;
            DrawFractal((int)image.Width, (int)image.Height);
        }
    }
}
