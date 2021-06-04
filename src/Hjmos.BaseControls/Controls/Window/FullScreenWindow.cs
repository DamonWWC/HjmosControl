using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
using Hjmos.BaseControls.Tools.Extension;
using Hjmos.BaseControls.Tools.Interop;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
#if NET40
using Microsoft.Windows.Shell;
#else
using System.Windows.Shell;
#endif

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = ElementNonClientArea, Type = typeof(UIElement))]
    public class FullScreenWindow : System.Windows.Window
    {
        private const string ElementNonClientArea = "PART_NonClientArea";

        public static readonly DependencyProperty NonClientAreaContentProperty = DependencyProperty.Register(
            "NonClientAreaContent", typeof(object), typeof(FullScreenWindow), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty CloseButtonHoverBackgroundProperty = DependencyProperty.Register(
            "CloseButtonHoverBackground", typeof(Brush), typeof(FullScreenWindow),
            new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty CloseButtonHoverForegroundProperty =
            DependencyProperty.Register(
                "CloseButtonHoverForeground", typeof(Brush), typeof(FullScreenWindow),
                new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty CloseButtonBackgroundProperty = DependencyProperty.Register(
            "CloseButtonBackground", typeof(Brush), typeof(FullScreenWindow), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty CloseButtonForegroundProperty = DependencyProperty.Register(
            "CloseButtonForeground", typeof(Brush), typeof(FullScreenWindow),
            new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty OtherButtonBackgroundProperty = DependencyProperty.Register(
            "OtherButtonBackground", typeof(Brush), typeof(FullScreenWindow), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty OtherButtonForegroundProperty = DependencyProperty.Register(
            "OtherButtonForeground", typeof(Brush), typeof(FullScreenWindow),
            new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty OtherButtonHoverBackgroundProperty = DependencyProperty.Register(
            "OtherButtonHoverBackground", typeof(Brush), typeof(FullScreenWindow),
            new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty OtherButtonHoverForegroundProperty =
            DependencyProperty.Register(
                "OtherButtonHoverForeground", typeof(Brush), typeof(FullScreenWindow),
                new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty NonClientAreaBackgroundProperty = DependencyProperty.Register(
            "NonClientAreaBackground", typeof(Brush), typeof(FullScreenWindow),
            new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty NonClientAreaForegroundProperty = DependencyProperty.Register(
            "NonClientAreaForeground", typeof(Brush), typeof(FullScreenWindow),
            new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty NonClientAreaHeightProperty = DependencyProperty.Register(
            "NonClientAreaHeight", typeof(double), typeof(FullScreenWindow),
            new PropertyMetadata(22.0));

        public static readonly DependencyProperty ShowNonClientAreaProperty = DependencyProperty.Register(
            "ShowNonClientArea", typeof(bool), typeof(FullScreenWindow),
            new PropertyMetadata(true, OnShowNonClientAreaChanged));

        public static readonly DependencyProperty ShowTitleProperty = DependencyProperty.Register(
            "ShowTitle", typeof(bool), typeof(FullScreenWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty IsFullScreenProperty = DependencyProperty.Register(
            "IsFullScreen", typeof(bool), typeof(FullScreenWindow),
            new PropertyMetadata(false, OnIsFullScreenChanged));

        private bool _isFullScreen;

        private Thickness _actualBorderThickness;

        private readonly Thickness _commonPadding;

        private bool _showNonClientArea = true;

        private double _tempNonClientAreaHeight;

        private WindowState _tempWindowState;

        private WindowStyle _tempWindowStyle;

        private ResizeMode _tempResizeMode;

        private UIElement _nonClientArea;

        static FullScreenWindow()
        {
            StyleProperty.OverrideMetadata(typeof(FullScreenWindow), new FrameworkPropertyMetadata(ResourceHelper.GetResource<Style>("FullScreenWindow")));
        }

        public FullScreenWindow()
        {
            //#if NET40
            var chrome = new WindowChrome
            {
                CornerRadius = new CornerRadius(),
                GlassFrameThickness = new Thickness(0, 0, 0, 1)
            };
            //#else
            //            var chrome = new WindowChrome
            //            {
            //                CornerRadius = new CornerRadius(),
            //                GlassFrameThickness = new Thickness(0, 0, 0, 1),
            //                UseAeroCaptionButtons = false
            //            };
            //#endif
            BindingOperations.SetBinding(chrome, WindowChrome.CaptionHeightProperty,
                new Binding(NonClientAreaHeightProperty.Name) { Source = this });
            WindowChrome.SetWindowChrome(this, chrome);
            _commonPadding = Padding;

            Loaded += (s, e) => OnLoaded(e);
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            var mmi = (InteropValues.MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(InteropValues.MINMAXINFO));
            var monitor = InteropMethods.MonitorFromWindow(hwnd, InteropValues.MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero && mmi != null)
            {
                InteropValues.APPBARDATA appBarData = default;
                var autoHide = InteropMethods.SHAppBarMessage(4, ref appBarData) != 0;
                if (autoHide)
                {
                    var monitorInfo = default(InteropValues.MONITORINFO);
                    monitorInfo.cbSize = (uint)Marshal.SizeOf(typeof(InteropValues.MONITORINFO));
                    InteropMethods.GetMonitorInfo(monitor, ref monitorInfo);
                    var rcWorkArea = monitorInfo.rcWork;
                    var rcMonitorArea = monitorInfo.rcMonitor;
                    mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
                    mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
                    mmi.ptMaxSize.X = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
                    mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top - 1);
                }
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.GetHwndSource()?.AddHook(HwndSourceHook);
            base.OnSourceInitialized(e);
        }

        private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            switch (msg)
            {
                case InteropValues.WM_WINDOWPOSCHANGED:
                    Padding = WindowState == WindowState.Maximized ? WindowHelper.WindowMaximizedPadding : _commonPadding;
                    break;
                case InteropValues.WM_GETMINMAXINFO:
                    WmGetMinMaxInfo(hwnd, lparam);
                    Padding = WindowState == WindowState.Maximized ? WindowHelper.WindowMaximizedPadding : _commonPadding;
                    break;
            }

            return IntPtr.Zero;
        }


        public Brush CloseButtonBackground
        {
            get => (Brush)GetValue(CloseButtonBackgroundProperty);
            set => SetValue(CloseButtonBackgroundProperty, value);
        }

        public Brush CloseButtonForeground
        {
            get => (Brush)GetValue(CloseButtonForegroundProperty);
            set => SetValue(CloseButtonForegroundProperty, value);
        }

        public Brush OtherButtonBackground
        {
            get => (Brush)GetValue(OtherButtonBackgroundProperty);
            set => SetValue(OtherButtonBackgroundProperty, value);
        }

        public Brush OtherButtonForeground
        {
            get => (Brush)GetValue(OtherButtonForegroundProperty);
            set => SetValue(OtherButtonForegroundProperty, value);
        }

        public double NonClientAreaHeight
        {
            get => (double)GetValue(NonClientAreaHeightProperty);
            set => SetValue(NonClientAreaHeightProperty, value);
        }

        public bool IsFullScreen
        {
            get => (bool)GetValue(IsFullScreenProperty);
            set => SetValue(IsFullScreenProperty, value);
        }

        public object NonClientAreaContent
        {
            get => GetValue(NonClientAreaContentProperty);
            set => SetValue(NonClientAreaContentProperty, value);
        }

        public Brush CloseButtonHoverBackground
        {
            get => (Brush)GetValue(CloseButtonHoverBackgroundProperty);
            set => SetValue(CloseButtonHoverBackgroundProperty, value);
        }

        public Brush CloseButtonHoverForeground
        {
            get => (Brush)GetValue(CloseButtonHoverForegroundProperty);
            set => SetValue(CloseButtonHoverForegroundProperty, value);
        }

        public Brush OtherButtonHoverBackground
        {
            get => (Brush)GetValue(OtherButtonHoverBackgroundProperty);
            set => SetValue(OtherButtonHoverBackgroundProperty, value);
        }

        public Brush OtherButtonHoverForeground
        {
            get => (Brush)GetValue(OtherButtonHoverForegroundProperty);
            set => SetValue(OtherButtonHoverForegroundProperty, value);
        }

        public Brush NonClientAreaBackground
        {
            get => (Brush)GetValue(NonClientAreaBackgroundProperty);
            set => SetValue(NonClientAreaBackgroundProperty, value);
        }

        public Brush NonClientAreaForeground
        {
            get => (Brush)GetValue(NonClientAreaForegroundProperty);
            set => SetValue(NonClientAreaForegroundProperty, value);
        }

        public bool ShowNonClientArea
        {
            get => (bool)GetValue(ShowNonClientAreaProperty);
            set => SetValue(ShowNonClientAreaProperty, value);
        }

        public bool ShowTitle
        {
            get => (bool)GetValue(ShowTitleProperty);
            set => SetValue(ShowTitleProperty, value);
        }


        public bool PopupOpen
        {
            get { return (bool)GetValue(PopupOpenProperty); }
            set { SetValue(PopupOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupOpenProperty =
            DependencyProperty.Register("PopupOpen", typeof(bool), typeof(FullScreenWindow), new PropertyMetadata(false));



        private static void OnShowNonClientAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (FullScreenWindow)d;
            ctl.SwitchShowNonClientArea((bool)e.NewValue);
        }

        private void SwitchShowNonClientArea(bool showNonClientArea)
        {
            if (_nonClientArea == null)
            {
                _showNonClientArea = showNonClientArea;
                return;
            }

            if (showNonClientArea)
            {
                if (IsFullScreen)
                {
                    _nonClientArea.Show(false);
                    _tempNonClientAreaHeight = NonClientAreaHeight;
                    NonClientAreaHeight = 0;
                }
                else
                {
                    _nonClientArea.Show(true);
                    NonClientAreaHeight = _tempNonClientAreaHeight;
                }
            }
            else
            {
                _nonClientArea.Show(false);
                _tempNonClientAreaHeight = NonClientAreaHeight;
                NonClientAreaHeight = 0;
            }
        }

        private static void OnIsFullScreenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var ctl = (FullScreenWindow)d;
            ctl.SwitchIsFullScreen((bool)e.NewValue);
            if((bool)e.NewValue)
            {
                ctl.MouseMove -= ctl.FullScreenWindow_MouseMove;
                ctl.MouseMove += ctl.FullScreenWindow_MouseMove;
            }
            else
            {
                ctl.PopupOpen = false;
                ctl.MouseMove -= ctl.FullScreenWindow_MouseMove;
            }
        }

        private void SwitchIsFullScreen(bool isFullScreen)
        {
            if (_nonClientArea == null)
            {
                _isFullScreen = isFullScreen;
                return;
            }

            if (isFullScreen)
            {
                _nonClientArea.Show(false);
                _tempNonClientAreaHeight = NonClientAreaHeight;
                NonClientAreaHeight = 0;

                _tempWindowState = WindowState;
                _tempWindowStyle = WindowStyle;
                _tempResizeMode = ResizeMode;
                WindowStyle = WindowStyle.None;
                //下面三行不能改变，就是故意的
                WindowState = WindowState.Maximized;
                WindowState = WindowState.Minimized;
                WindowState = WindowState.Maximized;
            }
            else
            {
                if (ShowNonClientArea)
                {
                    _nonClientArea.Show(true);
                    NonClientAreaHeight = _tempNonClientAreaHeight;
                }
                else
                {
                    _nonClientArea.Show(false);
                    _tempNonClientAreaHeight = NonClientAreaHeight;
                    NonClientAreaHeight = 0;
                }

                WindowState = _tempWindowState;
                WindowStyle = _tempWindowStyle;
                ResizeMode = _tempResizeMode;
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (WindowState == WindowState.Maximized)
            {
                BorderThickness = new Thickness();
                _tempNonClientAreaHeight = NonClientAreaHeight;
                NonClientAreaHeight += 8;
            }
            else
            {
                BorderThickness = _actualBorderThickness;
                NonClientAreaHeight = _tempNonClientAreaHeight;
            }
        }

        protected void OnLoaded(RoutedEventArgs args)
        {
            _actualBorderThickness = BorderThickness;
            _tempNonClientAreaHeight = NonClientAreaHeight;

            if (WindowState == WindowState.Maximized)
            {
                BorderThickness = new Thickness();
                _tempNonClientAreaHeight += 8;
            }


            CommandBindings.Add(new CommandBinding(ControlCommands.CloseNavigation,
                (s, e) => { IsFullScreen = true; }));
            CommandBindings.Add(new CommandBinding(ControlCommands.OpenNavigation,
                (s, e) => { IsFullScreen = false; }));


            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand,
                (s, e) => WindowState = WindowState.Minimized));         
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (s, e) => Close()));
            CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, ShowSystemMenu));

            _tempWindowState = WindowState;
            _tempWindowStyle = WindowStyle;
            _tempResizeMode = ResizeMode;

            SwitchIsFullScreen(_isFullScreen);
            SwitchShowNonClientArea(_showNonClientArea);

            if (WindowState == WindowState.Maximized)
            {
                _tempNonClientAreaHeight -= 8;
            }

            if (SizeToContent != SizeToContent.WidthAndHeight)
                return;

            SizeToContent = SizeToContent.Height;
            Dispatcher.BeginInvoke(new Action(() => { SizeToContent = SizeToContent.WidthAndHeight; }));
          
        }

        private void FullScreenWindow_MouseMove(object sender, MouseEventArgs e)
        {
            Win32Api.POINT p = new Win32Api.POINT();
            if (Win32Api.GetCursorPos(out p))//API方法
            {                
                if ((p.Y >= 0) && (p.Y <= 100))
                {
                    if (!PopupOpen)
                    {
                        PopupOpen = true;
                    }                  
                }
                else
                {
                    if (PopupOpen)
                    {
                        PopupOpen = false;
                    }
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _nonClientArea = GetTemplateChild(ElementNonClientArea) as UIElement;                        
        }

        private void ShowSystemMenu(object sender, ExecutedRoutedEventArgs e)
        {
            var point = WindowState == WindowState.Maximized
                ? new Point(0, NonClientAreaHeight)
                : new Point(Left, Top + NonClientAreaHeight);
            SystemCommands.ShowSystemMenu(this, point);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (SizeToContent == SizeToContent.WidthAndHeight)
                InvalidateMeasure();
        }
    }


    public class Win32Api
    {
        /// <summary>   
        /// 设置鼠标的坐标   
        /// </summary>   
        /// <param name="x">横坐标</param>   
        /// <param name="y">纵坐标</param>          

        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

        }

        /// <summary>   
        /// 获取鼠标的坐标   
        /// </summary>   
        /// <param name="lpPoint">传址参数，坐标point类型</param>   
        /// <returns>获取成功返回真</returns>   

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

    }
}

