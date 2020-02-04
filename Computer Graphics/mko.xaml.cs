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
using System.Drawing;
namespace ComputerGraphics
{
    /// <summary>
    /// Логика взаимодействия для mko.xaml
    /// </summary>
    public partial class mko : UserControl
    {
        public mko()
        {
            InitializeComponent();
            
           
            BitmapImage bitmap = new BitmapImage(new Uri("mko.png", UriKind.Relative));
            stride = bitmap.PixelWidth * 4;
            int size = bitmap.PixelHeight * stride;
            pixels = new byte[size];
            bitmap.CopyPixels(pixels, stride, 0);
            line = new Line();
            line.X1 = 366;
            line.Y1 = 378;
            line.X2 = line.X1;
            line.Y2 = line.Y1;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;
            
            line.Visibility = Visibility.Hidden;
            canvas.Children.Add(line);
        }

        private bool isMoved = false;
        private int stride;
        private byte[] pixels;
        private Line line;
       

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            isMoved = true;
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoved == true)
            {
               
                int index = (int)e.GetPosition(image).Y * stride + 4 * (int)e.GetPosition(image).X;
                byte r, g, b, extra_r, extra_b, extra_g;
                r = pixels[index + 2];
                g = pixels[index + 1];
                b = pixels[index];
                byte[] hex = { r, g, b };
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    textBox8.Text = (1.0 - ((double)hex.Min() / (double)hex.Max())).ToString("P");
                    
                    double changeX = e.GetPosition(canvas).X - line.X1,
                      changeY = e.GetPosition(canvas).Y - line.Y1;
                    int mult;
                    if (changeX > 0) mult = (int)(300 / changeX);
                     else mult = (int)(-300 / changeX);
                    line.X2 = line.X1 + changeX * mult;
                    line.Y2 = line.Y1 + changeY * mult;
                    
                    extra_r = (byte)(255 - r);
                    extra_g = (byte)(255 - g);
                    extra_b = (byte)(255 - b);
                    byte[] extra_hex = { extra_r, extra_g, extra_b };
                    mainColor.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                    extraColor.Fill = new SolidColorBrush(Color.FromRgb(extra_r, extra_g, extra_b));
                    mixedColor.Fill = new SolidColorBrush(Color.FromRgb((byte)((r + ((SolidColorBrush)(secondColor.Fill)).Color.R) / 2),
                                                                        (byte)((g + ((SolidColorBrush)(secondColor.Fill)).Color.G) / 2),
                                                                        (byte)((b + ((SolidColorBrush)(secondColor.Fill)).Color.B) / 2)));
                    textBox.Text = r.ToString();
                    textBox1.Text = g.ToString();
                    textBox2.Text = b.ToString();
                    textBox3.Text = BitConverter.ToString(hex).Replace("-", string.Empty);
                    textBox4.Text = extra_r.ToString();
                    textBox5.Text = extra_g.ToString();
                    textBox6.Text = extra_b.ToString();
                    textBox7.Text = BitConverter.ToString(extra_hex).Replace("-", string.Empty);
                }
                else if(e.RightButton == MouseButtonState.Pressed && e.LeftButton == MouseButtonState.Released)
                {
                    secondColor.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                    mixedColor.Fill = new SolidColorBrush(Color.FromRgb((byte)((r + ((SolidColorBrush)(mainColor.Fill)).Color.R) / 2), 
                                                                        (byte)((g + ((SolidColorBrush)(mainColor.Fill)).Color.G) / 2), 
                                                                        (byte)((b + ((SolidColorBrush)(mainColor.Fill)).Color.B) / 2)));
                }
                    
            }
        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMoved = false;
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            line.Visibility = Visibility.Visible;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            line.Visibility = Visibility.Hidden;
        }
    }
}
