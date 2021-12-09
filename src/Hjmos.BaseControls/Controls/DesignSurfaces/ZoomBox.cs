using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class ZoomBox : Control
    {
        // 鸟瞰图画布内的拖动控件
        private Thumb ZoomThumb { get; set; }

        // 鸟瞰图画布
        private Canvas ZoomCanvas { get; set; }

        // 保存缩放比例
        private ScaleTransform ScaleTransform { get; set; }

        // 主界面内容视区大小
        private Size ViewPortSize { get; set; }

        // 鸟瞰图滑块
        public Slider ZoomSlider
        {
            get => (Slider)GetValue(ZoomSliderProperty);
            set => SetValue(ZoomSliderProperty, value);
        }

        private DesignSurface oldSurface;

        public static readonly DependencyProperty ZoomSliderProperty = DependencyProperty.Register("ZoomSlider", typeof(Slider), typeof(ZoomBox));

        public ScrollViewer ScrollViewer => DesignSurface.ScrollViewer;

        // 主界面
        public DesignSurface DesignSurface
        {
            get => (DesignSurface)GetValue(DesignSurfaceProperty);
            set => SetValue(DesignSurfaceProperty, value);
        }

        public static readonly DependencyProperty DesignSurfaceProperty = DependencyProperty.Register("DesignSurface", typeof(DesignSurface), typeof(ZoomBox),
            new FrameworkPropertyMetadata(default, (d, e) =>
            {
                if (d is ZoomBox zoomBox)
                {
                    if (zoomBox.oldSurface != null)
                    {
                        zoomBox.oldSurface.LayoutUpdated -= zoomBox.DesignSurface_LayoutUpdated;
                    }
                    zoomBox.oldSurface = zoomBox.DesignSurface;

                    if (zoomBox.DesignSurface != null)
                    {
                        // 添加布局改变事件
                        zoomBox.DesignSurface.LayoutUpdated += zoomBox.DesignSurface_LayoutUpdated;
                    }
                }
            }));

        /// <summary>
        /// DesignSurface布局改变时触发
        /// TODO：调试的时候这个方法会一直循环，到时研究一下
        /// </summary>
        private void DesignSurface_LayoutUpdated(object sender, EventArgs e)
        {
            if (ZoomThumb == null || ScrollViewer == null) return;
            InvalidateScale(out double scale, out double xOffset, out double yOffset);
            // ViewPortSize为主界面内容视区宽高（不超过缩放后MainContent宽高）
            ZoomThumb.Width = ViewPortSize.Width * scale;
            ZoomThumb.Height = ViewPortSize.Height * scale;
            // ScrollViewer.ViewportWidth主界面内容视区宽高
            // this.ZoomThumb.Width = ScrollViewer.ViewportWidth * scale;
            // this.ZoomThumb.Height = ScrollViewer.ViewportHeight * scale;

            // 设置橡皮圈位置（误差偏移量+滚动条偏移量*缩放比例）
            Canvas.SetLeft(ZoomThumb, xOffset + ScrollViewer.HorizontalOffset * scale);
            Canvas.SetTop(ZoomThumb, yOffset + ScrollViewer.VerticalOffset * scale);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ScrollViewer == null) return;

            ZoomThumb = Template.FindName("PART_ZoomThumb", this) as Thumb;
            if (ZoomThumb == null) throw new Exception("PART_ZoomThumb template is missing!");

            ZoomCanvas = Template.FindName("PART_ZoomCanvas", this) as Canvas;
            if (ZoomCanvas == null) throw new Exception("PART_ZoomCanvas template is missing!");

            //ZoomSlider = Template.FindName("PART_ZoomSlider", this) as Slider;
            //if (ZoomSlider == null) throw new Exception("PART_ZoomSlider template is missing!");

            ZoomThumb.DragDelta += Thumb_DragDelta;
            //ZoomSlider.ValueChanged += ZoomSlider_ValueChanged;
            ScaleTransform = new ScaleTransform();
        }

        /// <summary>
        /// 滑块值改变时触发
        /// </summary>
        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double scale = e.NewValue / e.OldValue;
            double halfViewportHeight = ViewPortSize.Height / 2;
            double newVerticalOffset = (ScrollViewer.VerticalOffset + halfViewportHeight) * scale - halfViewportHeight;
            double halfViewportWidth = ViewPortSize.Width / 2;
            double newHorizontalOffset = (ScrollViewer.HorizontalOffset + halfViewportWidth) * scale - halfViewportWidth;
            ScaleTransform.ScaleX *= scale;
            ScaleTransform.ScaleY *= scale;
            ScrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
            ScrollViewer.ScrollToVerticalOffset(newVerticalOffset);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            InvalidateScale(out double scale, out _, out _);
            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset + e.HorizontalChange / scale);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + e.VerticalChange / scale);
        }

        /// <summary>
        /// 使缩放失效（重新计算缩放）
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        private void InvalidateScale(out double scale, out double xOffset, out double yOffset)
        {
            scale = 1;
            xOffset = 0;
            yOffset = 0;
            var designedElement = this.DesignSurface?.MainContent;
            if (designedElement == null) return;

            // MainContent的测量宽高
            double w = designedElement.DesiredSize.Width;
            double h = designedElement.DesiredSize.Height;
            // DesiredSize表示MainContent测量后需要的宽高（内容宽高），ActualWidth表示实际渲染后的宽度
            // 如果想要橡皮圈矩形框比例和PageShell实际渲染后的宽高一样，就用下面这两句
            //double w = this.DesignSurface.MainContent.ActualWidth;
            //double h = this.DesignSurface.MainContent.ActualHeight;

            // 鸟瞰图画布实际宽高
            double x = ZoomCanvas.ActualWidth;
            double y = ZoomCanvas.ActualHeight;

            // 鸟瞰图和实际内容宽高的比例
            double scaleX = x / w;
            double scaleY = y / h;

            // 整体缩放比例（取比例值叫小的作为整体缩放比例）
            scale = (scaleX < scaleY) ? scaleX : scaleY;

            // 橡皮圈需要增加的偏移量（ 实际缩放比例和整体缩放比例的误差 / 2 ）
            xOffset = (x - scale * w) / 2;
            yOffset = (y - scale * h) / 2;

            // 主界面内容视区宽高（不超过缩放后MainContent宽高）
            double viewportWidth = ScrollViewer.ViewportWidth > w ? w : ScrollViewer.ViewportWidth;
            double viewportHeight = ScrollViewer.ViewportHeight > h ? h : ScrollViewer.ViewportHeight;

            ViewPortSize = new Size(viewportWidth, viewportHeight);
        }

        /// <summary>
        /// 使缩放失效（重新计算缩放）
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        private void InvalidateScale1(out double scale, out double xOffset, out double yOffset)
        {
            // 缩放后MainContent的大小
            // double w = this.DesignSurface.MainContent.DesiredSize.Width * ScaleTransform.ScaleX;
            // double h = this.DesignSurface.MainContent.DesiredSize.Height * ScaleTransform.ScaleY;
            // DesiredSize表示MainContent测量后需要的宽高（内容宽高），ActualWidth表示实际渲染后的宽度
            // 如果想要橡皮圈矩形框比例和PageShell实际渲染后的宽高一样，就用下面这两句
            //double w = this.DesignSurface.MainContent.ActualWidth * ScaleTransform.ScaleX;
            //double h = this.DesignSurface.MainContent.ActualHeight * ScaleTransform.ScaleY;

            // 鸟瞰图画布大小

            var fac1 = DesignSurface.MainContent.DesiredSize.Width / ZoomCanvas.ActualWidth;
            var fac2 = DesignSurface.MainContent.DesiredSize.Height / ZoomCanvas.ActualHeight;

            double x = ZoomCanvas.ActualWidth;
            double y = ZoomCanvas.ActualHeight;

            if (fac1 < fac2)
            {
                x = DesignSurface.MainContent.ActualWidth / fac2;
                xOffset = (ZoomCanvas.ActualWidth - x) / 2;
                yOffset = 0;
            }
            else
            {
                y = DesignSurface.MainContent.ActualHeight / fac1;
                xOffset = 0;
                yOffset = (ZoomCanvas.ActualHeight - y) / 2;
            }

            double w = this.DesignSurface.MainContent.DesiredSize.Width;
            double h = this.DesignSurface.MainContent.DesiredSize.Height;

            double scaleX = x / w;
            double scaleY = y / h;
            scale = (scaleX < scaleY) ? scaleX : scaleY;

            if (ScrollViewer.ViewportHeight > h)
            {
                yOffset -= ((ScrollViewer.ViewportHeight - h) / 2) * scale;
            }
            if (ScrollViewer.ViewportWidth > w)
            {
                xOffset -= ((ScrollViewer.ViewportWidth - w) / 2) * scale;
            }

            xOffset += (x - scale * w) / 2;
            yOffset += (y - scale * h) / 2;

            // 主界面内容视区宽高（不超过缩放后RootCanvas宽高）
            double viewportWidth = ScrollViewer.ViewportWidth > w ? w : ScrollViewer.ViewportWidth;
            double viewportHeight = ScrollViewer.ViewportHeight > h ? h : ScrollViewer.ViewportHeight;

            ViewPortSize = new Size(viewportWidth, viewportHeight);
        }
    }
}