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
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Timers;
namespace ComputerGraphics
{
    /// <summary>
    /// Логика взаимодействия для dragon.xaml
    /// </summary>
    public partial class dragon : UserControl
    {
        public dragon()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0,0,0,0,500);
            timer.Tick += new EventHandler(Draw_dragon_curve);
            k = 0;
        }
        
        private DrawingVisual drawingVisual = new DrawingVisual();
        private DrawingContext context;
        private DispatcherTimer timer = new DispatcherTimer();
        private int k;
        
        private void dragon_func(int x1, int y1, int x2, int y2, int n)
        {
            int xn, yn;
            
            var drawingPen = new Pen(Brushes.Navy, 1);

            if (n > 0)
            {
                xn = (x1 + x2) / 2 + (y2 - y1) / 2;
                yn = (y1 + y2) / 2 - (x2 - x1) / 2;

                dragon_func(x2, y2, xn, yn, n - 1);
                dragon_func(x1, y1, xn, yn, n - 1);
            }

            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);
            
            context.DrawLine(drawingPen, point1, point2);
            
            
        }

        private void Draw_dragon_curve(object sender, EventArgs e)
        {
            
            int x1, y1, x2, y2;
            canvas.Children.Add(new VisualHost { Visual = drawingVisual });

            x1 = 400;
            y1 = 300;
            x2 = 790;
            y2 = 450;
            
            using (context = drawingVisual.RenderOpen())
            {
                dragon_func(x1, y1, x2, y2, k);
            }
            if (k == 13)
            {
                timer.Stop();
                k = 0;
                button.IsEnabled = true;
            }
            else k++;


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            timer.Start();
        }
    }

    public class VisualHost : UIElement
    {
        public Visual Visual { get; set; }

        protected override int VisualChildrenCount
        {
            get { return Visual != null ? 1 : 0; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return Visual;
        }
    }
}
