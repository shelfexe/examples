using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для bezier.xaml
    /// </summary>
    public partial class bezier : UserControl
    {
        public bezier(bool isQuadratic)
        {
            this.isQuadratic = isQuadratic;
            InitializeComponent();
            BezierItem();
        }


        private Rectangle Start;
        private Rectangle End;
        private Rectangle Middle;
        private Rectangle Middle2;
        private Rectangle rect;
        private BezierSegment beziers;
        private QuadraticBezierSegment quadraticBezier;
        private PathFigure pathFigure;
        private bool isMoved = false;
        private bool isQuadratic;
        private void BezierItem()
        {

            if(isQuadratic==true)
            {
                endX.Visibility = Visibility.Hidden;
                endY.Visibility = Visibility.Hidden;
                label6.Visibility = Visibility.Hidden;
                label7.Visibility = Visibility.Hidden;
                label4.Foreground = Brushes.Blue;
                label5.Foreground = Brushes.Blue;
            }
            else
            {
                endX.Visibility = Visibility.Visible;
                endY.Visibility = Visibility.Visible;
                label6.Visibility = Visibility.Visible;
                label7.Visibility = Visibility.Visible;
                label4.Foreground = Brushes.Brown;
                label5.Foreground = Brushes.Brown;
            }
            DrawingArea.Children.Clear();
            Start = CustomRect(10, Brushes.Red, new Point(200, 200));
            Middle = CustomRect(10, Brushes.Green, new Point(400, 100));
            End = CustomRect(10, Brushes.Blue, new Point(700, 200));

            Point p1 = new Point(Canvas.GetLeft(Start) + 5, Canvas.GetTop(Start) + 5);
            Point p2 = new Point(Canvas.GetLeft(Middle) + 5, Canvas.GetTop(Middle) + 5);
            
            Point p4 = new Point(Canvas.GetLeft(End) + 5, Canvas.GetTop(End) + 5);
            if (isQuadratic == false)
            {
                Middle2 = CustomRect(10, Brushes.Brown, new Point(300, 300));
                Point p3 = new Point(Canvas.GetLeft(Middle2) + 5, Canvas.GetTop(Middle2) + 5);
                DrawingArea.Children.Add(Middle2);
                DrawBezier_3(p1, p2, p3, p4);
                Middle2.MouseDown += Rect_MouseDown;
                Middle2.MouseUp += Rect_MouseUp;
                Middle2.MouseMove += Rect_MouseMove;
            }
            else DrawBezier_3(p1, p2, new Point(0, 0) , p4);
            
            DrawingArea.Children.Add(Start);
            DrawingArea.Children.Add(End);
            DrawingArea.Children.Add(Middle);
            Start.MouseDown += Rect_MouseDown;
            Start.MouseUp += Rect_MouseUp;
            Start.MouseMove += Rect_MouseMove;
            Middle.MouseDown += Rect_MouseDown;
            Middle.MouseUp += Rect_MouseUp;
            Middle.MouseMove += Rect_MouseMove;
            End.MouseDown += Rect_MouseDown;
            End.MouseUp += Rect_MouseUp;
            End.MouseMove += Rect_MouseMove;
        }



        private void Rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rect = sender as Rectangle;
            rect.CaptureMouse();
            isMoved = true;
        }

        private void Rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoved == true)
            {
                rect = sender as Rectangle;
                double X = e.GetPosition(DrawingArea).X;
                double Y = e.GetPosition(DrawingArea).Y;
                if (X > 0 && X < DrawingArea.ActualWidth - 10 && Y > 0 && Y < DrawingArea.ActualHeight)
                {
                    Canvas.SetLeft(rect, e.GetPosition(DrawingArea).X - 5);
                    Canvas.SetTop(rect, e.GetPosition(DrawingArea).Y - 5);
                }
                Point p1 = new Point(Canvas.GetLeft(Start) + 5, Canvas.GetTop(Start) + 5);

                Point p2 = new Point(Canvas.GetLeft(Middle) + 5, Canvas.GetTop(Middle) + 5);
                Point p4 = new Point(Canvas.GetLeft(End) + 5, Canvas.GetTop(End) + 5);
                startX.Text = p1.X.ToString();
                startY.Text = p1.Y.ToString();
                middleX.Text = p2.X.ToString();
                middleY.Text = p2.Y.ToString();

                if (isQuadratic == false)
                {
                    Point p3 = new Point(Canvas.GetLeft(Middle2) + 5, Canvas.GetTop(Middle2) + 5);

                    middle2X.Text = p3.X.ToString();
                    middle2Y.Text = p3.Y.ToString();
                    endX.Text = p4.X.ToString();
                    endY.Text = p4.Y.ToString();
                    beziers.Point1 = p2;
                    beziers.Point2 = p3;
                    beziers.Point3 = p4;
                }
                else
                {
                    middle2X.Text = p4.X.ToString();
                    middle2Y.Text = p4.Y.ToString();
                    quadraticBezier.Point1 = p2;
                    quadraticBezier.Point2 = p4;
                }
                pathFigure.StartPoint = p1;
            }

        }

        private void Rect_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = sender as Rectangle;
            rect.ReleaseMouseCapture();
            isMoved = false;
        }

        private void DrawBezier_3(Point start, Point middle, Point middle2, Point end)
        {
            Path path = new Path();
            PathGeometry pathG = new PathGeometry();
            pathFigure = new PathFigure();
            pathFigure.StartPoint = start;
            if (isQuadratic == false)
            {
                beziers = new BezierSegment(middle, middle2, end, true);
                pathFigure.Segments.Add(beziers);
            }
            else
            {
                quadraticBezier = new QuadraticBezierSegment(middle, end, true);
                pathFigure.Segments.Add(quadraticBezier);
            }

            pathG.Figures.Add(pathFigure);
            path.StrokeThickness = 5;
            path.Stroke = Brushes.Black;
            path.Data = pathG;
            DrawingArea.Children.Add(path);

        }

        private Rectangle CustomRect(int thickness, Brush brush, Point coord)
        {
            Rectangle rect = new Rectangle
            {
                Stroke = brush,
                StrokeThickness = thickness
            };
            Canvas.SetLeft(rect, coord.X);
            Canvas.SetTop(rect, coord.Y);
            return rect;
        }

     

    }
}
