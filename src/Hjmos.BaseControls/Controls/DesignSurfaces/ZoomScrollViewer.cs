using Hjmos.BaseControls.Data;
using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hjmos.BaseControls.Controls
{
    /// <summary>
    /// 控制滚动视图缩放的类
    /// </summary>
    public class ZoomScrollViewer : ScrollViewer
    {
        static ZoomScrollViewer()
        {
            // 平移时的鼠标手势
            _panToolCursor = GetCursor("Resources/PanToolCursor.cur");
            _panToolCursorMouseDown = GetCursor("Resources/PanToolCursorMouseDown.cur");
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(typeof(ZoomScrollViewer)));
        }

        public static readonly DependencyProperty CurrentZoomProperty =
            DependencyProperty.Register("CurrentZoom", typeof(double), typeof(ZoomScrollViewer),
                                        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, CalculateZoomButtonCollapsed, CoerceZoom));

        public double CurrentZoom
        {
            get => (double)GetValue(CurrentZoomProperty);
            set => SetValue(CurrentZoomProperty, value);
        }

        private static object CoerceZoom(DependencyObject d, object baseValue)
        {
            double zoom = (double)baseValue;
            ZoomScrollViewer sv = (ZoomScrollViewer)d;
            return Math.Max(sv.MinimumZoom, Math.Min(sv.MaximumZoom, zoom));
        }

        public static readonly DependencyProperty MinimumZoomProperty =
            DependencyProperty.Register("MinimumZoom", typeof(double), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(0.2));

        public double MinimumZoom
        {
            get => (double)GetValue(MinimumZoomProperty);
            set => SetValue(MinimumZoomProperty, value);
        }

        public static readonly DependencyProperty MaximumZoomProperty =
            DependencyProperty.Register("MaximumZoom", typeof(double), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(5.0));

        public double MaximumZoom
        {
            get => (double)GetValue(MaximumZoomProperty);
            set => SetValue(MaximumZoomProperty, value);
        }

        public static readonly DependencyProperty MouseWheelZoomProperty =
            DependencyProperty.Register("MouseWheelZoom", typeof(bool), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(true));

        public bool MouseWheelZoom
        {
            get => (bool)GetValue(MouseWheelZoomProperty);
            set => SetValue(MouseWheelZoomProperty, value);
        }

        public static readonly DependencyProperty AlwaysShowZoomButtonsProperty =
            DependencyProperty.Register("AlwaysShowZoomButtons", typeof(bool), typeof(ZoomScrollViewer),
                                        new FrameworkPropertyMetadata(false, CalculateZoomButtonCollapsed));

        public bool AlwaysShowZoomButtons
        {
            get => (bool)GetValue(AlwaysShowZoomButtonsProperty);
            set => SetValue(AlwaysShowZoomButtonsProperty, value);
        }

        private static readonly DependencyPropertyKey ComputedZoomButtonCollapsedPropertyKey =
            DependencyProperty.RegisterReadOnly("ComputedZoomButtonCollapsed", typeof(bool), typeof(ZoomScrollViewer),
                                                new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty ComputedZoomButtonCollapsedProperty = ComputedZoomButtonCollapsedPropertyKey.DependencyProperty;

        public bool ComputedZoomButtonCollapsed
        {
            get => (bool)GetValue(ComputedZoomButtonCollapsedProperty);
            private set => SetValue(ComputedZoomButtonCollapsedPropertyKey, value);
        }

        private static void CalculateZoomButtonCollapsed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ZoomScrollViewer z)
                z.ComputedZoomButtonCollapsed = (z.AlwaysShowZoomButtons == false) && (z.CurrentZoom == 1.0);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!e.Handled && Keyboard.Modifiers == ModifierKeys.Control && MouseWheelZoom)
            {
                double oldZoom = CurrentZoom;
                double newZoom = RoundToOneIfClose(CurrentZoom * Math.Pow(1.001, e.Delta));
                newZoom = Math.Max(this.MinimumZoom, Math.Min(this.MaximumZoom, newZoom));

                // 调整滚动位置，使鼠标停留在相同的可视坐标上
                Vector relMousePos;

                if (Template.FindName("PART_Presenter", this) is ContentPresenter presenter)
                {
                    Point mousePos = e.GetPosition(presenter);
                    relMousePos = new Vector(mousePos.X / presenter.ActualWidth, mousePos.Y / presenter.ActualHeight);
                }
                else
                {
                    relMousePos = new Vector(0.5, 0.5);
                }

                Point scrollOffset = new Point(this.HorizontalOffset, this.VerticalOffset);
                Vector oldHalfViewport = new Vector(this.ViewportWidth / 2, this.ViewportHeight / 2);
                Vector newHalfViewport = oldHalfViewport / newZoom * oldZoom;
                Point oldCenter = scrollOffset + oldHalfViewport;
                Point virtualMousePos = scrollOffset + new Vector(relMousePos.X * this.ViewportWidth, relMousePos.Y * this.ViewportHeight);

                // As newCenter, we want to choose a point between oldCenter and virtualMousePos. The more we zoom in, the closer
                // to virtualMousePos. We'll create the line x = oldCenter + lambda * (virtualMousePos-oldCenter).
                // On this line, we need to choose lambda between -1 and 1:
                // -1 = zoomed out completely
                //  0 = zoom unchanged
                // +1 = zoomed in completely
                // But the zoom factor (newZoom/oldZoom) we have is in the range [0,+Infinity].

                // Basically, I just played around until I found a function that maps this to [-1,1] and works well.
                // "f" is squared because otherwise the mouse simply stays over virtualMousePos, but I wanted virtualMousePos
                // to move towards the middle -> squaring f causes lambda to be closer to 1, giving virtualMousePos more weight
                // then oldCenter.

                double f = Math.Min(newZoom, oldZoom) / Math.Max(newZoom, oldZoom);
                double lambda = 1 - f * f;
                if (oldZoom > newZoom)
                    lambda = -lambda;

                Point newCenter = oldCenter + lambda * (virtualMousePos - oldCenter);
                scrollOffset = newCenter - newHalfViewport;

                SetCurrentValue(CurrentZoomProperty, newZoom);

                this.ScrollToHorizontalOffset(scrollOffset.X);
                this.ScrollToVerticalOffset(scrollOffset.Y);

                e.Handled = true;
            }
            base.OnMouseWheel(e);
        }

        //private void OnMouseHorizontalWheel(object d, RoutedEventArgs e)
        //{
        //    if (Keyboard.Modifiers != ModifierKeys.Control)
        //    {
        //        MouseHorizontalWheelEventArgs ea = e as MouseHorizontalWheelEventArgs;

        //        this.ScrollToHorizontalOffset(this.HorizontalOffset + ea.HorizontalDelta);
        //    }
        //}

        internal static double RoundToOneIfClose(double val)
        {
            return Math.Abs(val - 1.0) < 0.001 ? 1.0 : val;
        }

        #region ZoomControl

        public object AdditionalControls
        {
            get => GetValue(AdditionalControlsProperty);
            set => SetValue(AdditionalControlsProperty, value);
        }

        public static readonly DependencyProperty AdditionalControlsProperty =
            DependencyProperty.Register("AdditionalControls", typeof(object), typeof(ZoomScrollViewer), new PropertyMetadata(null));

        /// <summary>
        /// 根据路径加载Assets程序集下的鼠标手势
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>光标</returns>
        internal static Cursor GetCursor(string path)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            ResourceManager m = new(a.GetName().Name + ".g", a);
            using Stream s = m.GetStream(path.ToLowerInvariant());
            return new Cursor(s);
        }

        /// <summary>平移工具图标（手掌张开）</summary>
        private static readonly Cursor _panToolCursor;

        /// <summary>鼠标按下时的平移工具图标（手掌抓住）</summary>
        private static readonly Cursor _panToolCursorMouseDown;

        private double _startHorizontalOffset;
        private double _startVericalOffset;

        /// <summary>按下鼠标时的坐标</summary>
        private Point _startPoint;

        /// <summary>是否按下鼠标</summary>
        private bool _isMouseDown;

        /// <summary>是否平移</summary>
        private bool _pan;

        /// <summary>
        /// 键盘按下时触发
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!_pan && e.Key == Key.Space)
            {
                // 按住空格键平移
                _pan = true;
                Mouse.UpdateCursor();
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 键盘弹起时触发
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                // 停止平移
                _pan = false;
                Mouse.UpdateCursor();
            }
            base.OnKeyUp(e);
        }

        /// <summary>
        /// 鼠标按下时触发
        /// </summary>
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (!_pan && e.MiddleButton == MouseButtonState.Pressed)
            {
                // 按住鼠标中键平移
                _pan = true;
                Mouse.UpdateCursor();
            }

            if (_pan && !e.Handled)
            {
                // 捕获鼠标
                if (Mouse.Capture(this))
                {
                    // 标记鼠标按下
                    _isMouseDown = true;
                    e.Handled = true;
                    // 纪录鼠标起始位置
                    _startPoint = e.GetPosition(this);
                    PanStart();
                    Mouse.UpdateCursor();
                }
            }
            base.OnPreviewMouseDown(e);
        }

        /// <summary>
        /// 鼠标移动时触发
        /// </summary>
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                // 获取当前鼠标位置
                Point endPoint = e.GetPosition(this);
                PanContinue(endPoint - _startPoint);
            }
            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (_pan && e.MiddleButton != MouseButtonState.Pressed && !Keyboard.IsKeyDown(Key.Space))
            {
                _pan = false;
                Mouse.UpdateCursor();
            }

            if (_isMouseDown)
            {
                _isMouseDown = false;
                ReleaseMouseCapture();
                Mouse.UpdateCursor();
            }
            base.OnPreviewMouseUp(e);
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _isMouseDown = false;
                ReleaseMouseCapture();
                Mouse.UpdateCursor();
            }
            base.OnLostMouseCapture(e);
        }

        protected override void OnQueryCursor(QueryCursorEventArgs e)
        {
            base.OnQueryCursor(e);
            if (!e.Handled && (_pan || _isMouseDown))
            {
                e.Handled = true;
                e.Cursor = _isMouseDown ? _panToolCursorMouseDown : _panToolCursor;
            }
        }

        /// <summary>
        /// 开始平移
        /// </summary>
        private void PanStart()
        {
            _startHorizontalOffset = this.HorizontalOffset;
            _startVericalOffset = this.VerticalOffset;
        }

        /// <summary>
        /// 继续平移
        /// </summary>
        /// <param name="delta">平移的量</param>
        private void PanContinue(Vector delta)
        {
            ScrollToHorizontalOffset(_startHorizontalOffset - delta.X / this.CurrentZoom);
            ScrollToVerticalOffset(_startVericalOffset - delta.Y / this.CurrentZoom);
            RaiseEvent(new FunctionEventArgs<Vector>(MoveEvent, this)
            {
                Info = delta
            });
        }

        public static readonly RoutedEvent MoveEvent =
            EventManager.RegisterRoutedEvent("Move", RoutingStrategy.Bubble,
                typeof(EventHandler<FunctionEventArgs<Vector>>), typeof(ZoomScrollViewer));

        public event EventHandler<FunctionEventArgs<Vector>> Move
        {
            add => AddHandler(MoveEvent, value);
            remove => RemoveHandler(MoveEvent, value);
        }

        #endregion ZoomControl
    }
}