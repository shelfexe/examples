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
    /// Логика взаимодействия для fract.xaml
    /// </summary>
    public partial class fract : UserControl
    {
        public fract()
        {
            InitializeComponent();
            
            iter = 0;
        }

        private DrawingVisual drawingVisual = new DrawingVisual();
        private Pen pen;
        private DrawingContext context;
        private int iter;
        

        private int Fractal(Point p1, Point p2, Point p3, int iter)
        {
            
            if (iter > 0)  
            {
               
                var p4 = new Point((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new Point((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
               
                var ps = new Point((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new Point((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);


                 context.DrawLine(pen, p4, pn);
                 context.DrawLine(pen, p5, pn);
                 context.DrawLine(pen, p4, p5);

                
                

                Fractal(p4, pn, p5, iter - 1);
                Fractal(pn, p5, p4, iter - 1);
                Fractal(p1, p4, new Point((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), iter - 1);
                Fractal(p5, p2, new Point((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), iter - 1);

            }
            return iter;
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

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if (iter == 4) button_forward.IsEnabled = false;
            button_back.IsEnabled = true;
            drawKoch(++iter);
            
            
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (iter == 1) button_back.IsEnabled = false;
            button_forward.IsEnabled = true;
            drawKoch(--iter);
            
        }

        private void drawKoch(int iter)
        {

            canvas.Children.Add(new VisualHost { Visual = drawingVisual });

            pen = new Pen(Brushes.Black, 1);

            var point1 = new Point(590, 10);
            var point2 = new Point(290, 460);
            var point3 = new Point(890, 460);


            using (context = drawingVisual.RenderOpen())
            {

                context.DrawLine(pen, point1, point2);
                context.DrawLine(pen, point2, point3);
                context.DrawLine(pen, point3, point1);
            
                Fractal(point1, point2, point3, iter);
                Fractal(point2, point3, point1, iter);
                Fractal(point3, point1, point2, iter);
            }
        }
    }
}
