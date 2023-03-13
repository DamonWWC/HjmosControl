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

namespace Drop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.btn.AddHandler(Canvas.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btn_MouseLeftButtonDown), true);
        }
        DragButton dragbutton;
        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            //var point1 = Mouse.GetPosition(canvas);
            var point = e.GetPosition(canvas);
            if(button !=null)
            {
                dragbutton = new DragButton
                {
                    Width = 80,
                    Height = 30,
                    Content = "拖拽",
                    IsDrag = false
                };
                Canvas.SetLeft(dragbutton, point.X);
                Canvas.SetTop(dragbutton, point.Y);
                dragbutton.Visibility = Visibility.Hidden;
                dragbutton.RenderTransform = new TranslateTransform(0d, 0d);
                this.canvas.Children.Add(dragbutton);
              
            }
           
        }

        /// <summary>
        /// 区域移动事件
        /// </summary>
        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            //DragButton dragButton = sender as DragButton;
            //if (dragButton != null)
            if(dragbutton.IsDrag)
            {
                Point offsetPoint = e.GetPosition(this.canvas);
                double xOffset = offsetPoint.X - dragbutton.CurrentPos.X - dragbutton.ClickPos.X;
                double yOffset = offsetPoint.Y - dragbutton.CurrentPos.Y - dragbutton.ClickPos.Y;

                TranslateTransform transform = (TranslateTransform)dragbutton.RenderTransform;
                transform.X += xOffset;
                transform.Y += yOffset;

                dragbutton.CurrentPos = new Point(offsetPoint.X - dragbutton.ClickPos.X, offsetPoint.Y - dragbutton.ClickPos.Y);
            
            }
        }
        /// <summary>
        /// 区域鼠标左键抬起
        /// </summary>
        private void CanvasButtonLeftUp(object sender, MouseButtonEventArgs e)
        {
            //ReducingButton(sender);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
      
        private void canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dragbutton == null) return;

            var point = e.GetPosition(canvas);
            Canvas.SetLeft(dragbutton, point.X-40);
            Canvas.SetTop(dragbutton, point.Y-15);
            dragbutton.ClickPos = new Point(40,15);
            dragbutton.Visibility = Visibility.Visible;
            dragbutton.IsDrag = true; 
            //注册事件
            canvas.AddHandler(Canvas.MouseMoveEvent, new MouseEventHandler(this.CanvasMouseMove), true);
            canvas.AddHandler(Canvas.MouseLeaveEvent, new MouseEventHandler(this.CanvasMouseLeave), true);
            
        }
        private void CanvasMouseLeave(object sender, MouseEventArgs e)
        {
            dragbutton.Visibility = Visibility.Hidden;
            dragbutton.IsDrag = false;
            canvas.RemoveHandler(Canvas.MouseMoveEvent, new MouseEventHandler(this.CanvasMouseMove));
            canvas.RemoveHandler(Canvas.MouseLeaveEvent, new MouseEventHandler(this.CanvasMouseLeave));
        }
    }
}
