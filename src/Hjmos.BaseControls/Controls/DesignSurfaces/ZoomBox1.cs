using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = "PART_ZoomThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_Track", Type = typeof(ZoomBoxCompareTrack))]
    public class ZoomBox1 : CompareSlider
    {
        private Thumb _ZoomThumb;
        private ZoomBoxCompareTrack _ZoomBoxCompareTrack;

        private Size ViewPortSize { get; set; }
        public ScrollViewer ScrollViewer => DesignSurface.ScrollViewer;
        private DesignSurface oldSurface;

        public ZoomBox1()
        {
            this.Loaded += ZoomBox1_Loaded;
        }

        private void ZoomBox1_Loaded(object sender, RoutedEventArgs e)
        {
            InvalidateScale();
            // 设置橡皮圈位置（误差偏移量+滚动条偏移量*缩放比例）
            _ZoomThumb.Width = ViewPortSize.Width * _scale;
            ValueToOffset(0,Value);
        }

        public DesignSurface DesignSurface
        {
            get { return (DesignSurface)GetValue(DesignSurfaceProperty); }
            set { SetValue(DesignSurfaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesignSurface.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesignSurfaceProperty =
            DependencyProperty.Register("DesignSurface", typeof(DesignSurface), typeof(ZoomBox1), new FrameworkPropertyMetadata(default, (d, e) =>
            {
                if (d is ZoomBox1 zoomBox)
                {
                    if (zoomBox.oldSurface != null)
                    {
                        zoomBox.oldSurface.Move -= zoomBox.OldSurface_Move;

                       //zoomBox.oldSurface.LayoutUpdated -= zoomBox.DesignSurface_LayoutUpdated;
                    }
                    zoomBox.oldSurface = zoomBox.DesignSurface;

                    if (zoomBox.DesignSurface != null)
                    {
                        zoomBox.oldSurface.Move += zoomBox.OldSurface_Move;
                        // 添加布局改变事件
                        //zoomBox.DesignSurface.LayoutUpdated += zoomBox.DesignSurface_LayoutUpdated;
                    }
                }
            }));
        bool _isSurfaceMove = false;
        private void OldSurface_Move(object sender, Data.FunctionEventArgs<Vector> e)
             {
            if (_ZoomThumb == null || ScrollViewer == null || _isDrag )
            {                
                return;
            }
            InvalidateScale();
            // 设置橡皮圈位置（误差偏移量+滚动条偏移量*缩放比例）
            _ZoomThumb.Width = ViewPortSize.Width * _scale;
            _isSurfaceMove = true;
            OffsetToValue();
            //var molecular = _xOffset + ScrollViewer.HorizontalOffset * _scale;
            //var denominator = ActualWidth - _ZoomThumb.Width;
            //if (denominator == 0)
            //{
            //    Value = 0;
            //}
            //else
            //{
            //    Value = molecular / denominator * 10;
            //}
        }

        private void DesignSurface_LayoutUpdated(object sender, EventArgs e)
        {
            if (_ZoomThumb == null || ScrollViewer == null || _isDrag )
            {
               
                return;
            }
            InvalidateScale();
            // 设置橡皮圈位置（误差偏移量+滚动条偏移量*缩放比例）
            _ZoomThumb.Width = ViewPortSize.Width * _scale;
            OffsetToValue();


            //var molecular = _xOffset + ScrollViewer.HorizontalOffset * _scale;
            //var denominator = ActualWidth - _ZoomThumb.Width;
            //if (denominator == 0)
            //{
            //    Value = 0;
            //}
            //else
            //{
            //    Value = molecular / denominator * 10;
            //}

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _ZoomThumb = GetTemplateChild("PART_ZoomThumb") as Thumb;
            _ZoomBoxCompareTrack = GetTemplateChild("PART_Track") as ZoomBoxCompareTrack;
        }

        private bool _isDrag = false;

        protected override void OnThumbDragStarted(DragStartedEventArgs e)
        {
            base.OnThumbDragStarted(e);
            _isDrag = true;
        }

        protected override void OnThumbDragCompleted(DragCompletedEventArgs e)
        {
            base.OnThumbDragCompleted(e);
            _isDrag = false;
        }

        protected override void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            base.OnThumbDragDelta(e);
            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset + e.HorizontalChange / _scale);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + e.VerticalChange / _scale);
        }

       

        protected override void OnValueChanged(double oldValue, double newValue)
        {           
            base.OnValueChanged(oldValue, newValue);
            InvalidateScale();
            if (_isDrag || _isSurfaceMove)
            {
                _isSurfaceMove = false;       
                return;
            }

            if (_ZoomThumb == null) return;

            ValueToOffset(oldValue,newValue);
        }



        private void ValueToOffset(double oldValue, double newValue)
        {
            var denominator = ActualWidth - _ZoomThumb.Width;
            var cc = (newValue * denominator) / 10.0;
            ScrollViewer.ScrollToHorizontalOffset(cc / _scale);
            //if(oldValue==0)
            //{
            //    ScrollViewer.ScrollToHorizontalOffset(0);
            //    return;
            //}
            //double scale = newValue / oldValue;
            //var cc = ScrollViewer.HorizontalOffset * scale;
            //ScrollViewer.ScrollToHorizontalOffset(cc);


        }

        private void OffsetToValue()
        {
            var molecular = _xOffset + ScrollViewer.HorizontalOffset * _scale;
            var denominator = ActualWidth - _ZoomThumb.Width;
            if (denominator == 0)
            {
                Value = 0;
            }
            else
            {
                Value = molecular / denominator * 10;
            }
        }

        private double _scale, _xOffset, _yOffset;

        private void InvalidateScale()
        {
            _scale = 1;
            _xOffset = 0;
            _yOffset = 0;
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
            double x = this.ActualWidth;
            double y = this.ActualHeight;

            // 鸟瞰图和实际内容宽高的比例
            double scaleX = x / w;
            double scaleY = y / h;

            // 整体缩放比例（取比例值叫小的作为整体缩放比例）
            _scale = (scaleX < scaleY) ? scaleX : scaleY;

            // 橡皮圈需要增加的偏移量（ 实际缩放比例和整体缩放比例的误差 / 2 ）
            _xOffset = (x - _scale * w) / 2;
            _yOffset = (y - _scale * h) / 2;

            // 主界面内容视区宽高（不超过缩放后MainContent宽高）
            double viewportWidth = ScrollViewer.ViewportWidth > w ? w : ScrollViewer.ViewportWidth;
            double viewportHeight = ScrollViewer.ViewportHeight > h ? h : ScrollViewer.ViewportHeight;

            ViewPortSize = new Size(viewportWidth, viewportHeight);
        }
    }
}