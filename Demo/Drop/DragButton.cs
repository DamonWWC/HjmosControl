using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drop
{
    /// <summary>
    /// 拖拽按钮
    /// </summary>
    public class DragButton : Button
    {
        //依赖属性
        private static readonly DependencyProperty IsDragProperty = DependencyProperty.Register("IsDrag", typeof(Boolean), typeof(DragButton));
        private static readonly DependencyProperty CurrentPosProperty = DependencyProperty.Register("CurrentPos", typeof(Point), typeof(DragButton));
        private static readonly DependencyProperty ClickPosProperty = DependencyProperty.Register("ClickPos", typeof(Point), typeof(DragButton));
        private static readonly DependencyProperty RectProperty = DependencyProperty.Register("Rect", typeof(Rectangle), typeof(DragButton));

        /// <summary>
        /// 是否拖拽
        /// </summary>
        public bool IsDrag
        {
            get
            {
                return (bool)this.GetValue(IsDragProperty);
            }
            set
            {
                this.SetValue(IsDragProperty, value);
            }
        }

        /// <summary>
        /// 按钮的定位位置
        /// 按钮左上角的位置
        /// </summary>
        public Point CurrentPos
        {
            get
            {
                //第一次获取如果是没有被初始化，那么吧按钮的坐标初始化过来
                Point p = (Point)this.GetValue(CurrentPosProperty);
                if (p.X == 0 && p.Y == 0)
                {
                    p.X = Canvas.GetLeft(this);
                    p.Y = Canvas.GetTop(this);
                }
                return p;
            }
            set
            {
                this.SetValue(CurrentPosProperty, value);
            }
        }

        /// <summary>
        /// 当前鼠标点在按钮上的位置
        /// </summary>
        public Point ClickPos
        {
            get
            {
                return (Point)this.GetValue(ClickPosProperty);
            }
            set
            {
                this.SetValue(ClickPosProperty, value);
            }
        }

        /// <summary>
        /// 虚拟出来的按钮的显示矩形
        /// </summary>
        public Rectangle Rect
        {
            get
            {
                if (this.GetValue(RectProperty) == null)
                {
                    //创建VisualBrush
                    VisualBrush visualBrush = new VisualBrush(this);
                    Rectangle rect = new Rectangle() { Width = 80, Height = 30, Fill = visualBrush, Name = "rect" };

                    //设置值
                    Canvas.SetLeft(rect, Canvas.GetLeft(this));
                    Canvas.SetTop(rect, Canvas.GetTop(this));

                    rect.RenderTransform = new TranslateTransform(0d, 0d);
                    rect.Opacity = 0.6;

                    this.SetValue(RectProperty, rect);
                }

                return (Rectangle)this.GetValue(RectProperty);
            }
        }
    }

}
