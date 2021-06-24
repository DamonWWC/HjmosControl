using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// Window8.xaml 的交互逻辑
    /// </summary>
    public partial class Window8 : Window
    {
        public Window8()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            InitLayout();
        }
        #region init

        void InitLayout()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Rectangle rect = new Rectangle();
                    SolidColorBrush scb = new SolidColorBrush(GetRandomColor());
                    rect.SetValue(Grid.RowProperty, i);
                    rect.SetValue(Grid.ColumnProperty, j);
                    rect.Fill = scb;
                    myGrid.Children.Add(rect);

                }
            }
            myGrid.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            myGrid.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            myGrid.PreviewMouseMove += Element_PreviewMouseMove;

        }

        Random random = new Random();

        public Color GetRandomColor()
        {
            return Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
        }

        #endregion

        #region drag and drop
        private UIElement initUE;
        private Point initPt;
        private Popup _dragdropPopup = null;
        private bool isDown = false;
        private double moveOpacity = 0.5;
        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isDown) return;
            isDown = false;
            if (this._dragdropPopup != null)
            {
                this._dragdropPopup.IsOpen = false;
                this._dragdropPopup.Child = null;
                this._dragdropPopup = null;
            }
            foreach (UIElement element in myGrid.Children)
            {
                if (element != initUE)
                {
                    element.Opacity = 1;
                }
            }
            initUE.ReleaseMouseCapture();

            double y = e.GetPosition(myGrid).Y;
            double start = 0.0;
            int row = 0;
            foreach (RowDefinition rd in myGrid.RowDefinitions)
            {
                start += rd.ActualHeight;
                if (y < start)
                {
                    break;
                }
                row++;
            }
            double x = e.GetPosition(myGrid).X;
            double cstart = 0.0;
            int column = 0;
            foreach (ColumnDefinition cd in myGrid.ColumnDefinitions)
            {
                cstart += cd.ActualWidth;
                if (x < cstart)
                {
                    break;
                }
                column++;
            }
            var initRow = Grid.GetRow(initUE);
            var initCol = Grid.GetColumn(initUE);
            UIElement uIElement = null;
            if (row != initRow || column != initCol)
            {
                uIElement = GetChildren(myGrid, row, column);
            }
            if (uIElement != null)
            {
                myGrid.Children.Remove(uIElement);
                Grid.SetColumn(initUE, column);
                Grid.SetRow(initUE, row);
                myGrid.Children.Add(uIElement);
                Grid.SetColumn(uIElement, initCol);
                Grid.SetRow(uIElement, initRow);

            }




        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
            {
                isDown = false;
                return;
            }
            initUE = (UIElement)e.Source;
            initUE.CaptureMouse();
            initPt = new Point(e.GetPosition(initUE).X, e.GetPosition(initUE).Y - SystemParameters.CaptionHeight);
            foreach (UIElement element in myGrid.Children)
            {
                if (element != initUE)
                {
                    element.Opacity = moveOpacity;
                }
            }
            CreateDragDropPopup(initUE, e);
            isDown = true;

        }

        private void Element_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == false) return;
            double y = e.GetPosition(myGrid).Y;
            double x = e.GetPosition(myGrid).X;

            if (_dragdropPopup != null)
            {
                _dragdropPopup.HorizontalOffset = x - initPt.X;
                _dragdropPopup.VerticalOffset = y - initPt.Y;
            }

            double start = 0.0;
            int row = 0;
            foreach (RowDefinition rd in myGrid.RowDefinitions)
            {
                start += rd.ActualHeight;
                if (y < start)
                {
                    break;
                }
                row++;
            }
            double cstart = 0.0;
            int column = 0;
            foreach (ColumnDefinition cd in myGrid.ColumnDefinitions)
            {
                cstart += cd.ActualWidth;
                if (x < cstart)
                {
                    break;
                }
                column++;
            }
            UIElement uIElement = GetChildren(myGrid, row, column);
            foreach (UIElement element in myGrid.Children)
            {
                if (element != initUE && element != uIElement)
                {
                    element.Opacity = moveOpacity;
                }
                else
                {
                    element.Opacity = 1;
                }
            }

        }

        private UIElement GetChildren(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            return null;

        }

        private void CreateDragDropPopup(Visual dragElement, MouseButtonEventArgs e)
        {
            this._dragdropPopup = new Popup();
            
            Rectangle r = new Rectangle();
            r.Width = ((FrameworkElement)dragElement).ActualWidth;
            r.Height = ((FrameworkElement)dragElement).ActualHeight;
            r.Fill = new VisualBrush(dragElement);
            this._dragdropPopup.Child = r;
            double y = e.GetPosition(myGrid).Y;
            double x = e.GetPosition(myGrid).X;
            _dragdropPopup.HorizontalOffset = x - initPt.X;
            _dragdropPopup.VerticalOffset = y - initPt.Y;
            this._dragdropPopup.IsOpen = true;
        }
        #endregion
    }
}
