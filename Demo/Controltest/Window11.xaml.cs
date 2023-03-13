using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using WpfApp1;

namespace Controltest
{
    /// <summary>
    /// Window11.xaml 的交互逻辑
    /// </summary>
    public partial class Window11 : Window
    {
        public Window11()
        {
            InitializeComponent();
            DataContext = this;
            this.MouseMove += Window9_MouseMove;
            //designSurface.Move += DesignSurface_Move; ;
            itemsSource = new List<FloodInformation>
            {
                new FloodInformation{StationName="湖南工业大学"},
                new FloodInformation{StationName ="白鸽站"},
                new FloodInformation{StationName ="湘雅三医院站"},
                new FloodInformation{StationName ="六沟垅站",IsChangeStation=true},
                new FloodInformation{StationName ="文昌阁站" ,IsChangeStation=true},
                new FloodInformation{StationName ="湘雅医院站"},
                new FloodInformation{StationName="湖南工业大学1"},
                new FloodInformation{StationName ="白鸽站1"},
                new FloodInformation{StationName ="湘雅三医院站1"},
                new FloodInformation{StationName ="六沟垅站1",IsChangeStation=true},
                new FloodInformation{StationName ="文昌阁站1" ,IsChangeStation=true},
                new FloodInformation{StationName ="湘雅医院站1" ,IsLast=true}
            };
        }

        private void Window9_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(this);

            text.Text = $"{point.X},{point.Y}";
        }

        public List<FloodInformation> itemsSource { get; set; }
        private List<Window> Windows { get; set; }
        private double x;
        private double y;

        //Window window;
        public ICommand ButtonCommand => new DelegateCommand<object>((obj) =>
         {
             var oo = obj as StackPanel;

             Window window = new Window
             {
                 Content = new UserControl1(),
                 Owner = Application.Current.MainWindow,
             };
            //window.Show();
            //IntPtr win = new WindowInteropHelper(window).Handle;
            //WinApiHelper.SetParent(win, oo.Handle);
            //WinApiHelper.MoveWindow(win, 0, 0, 200, 200, false);
            oo.Children.Add(new UserControl1());
            //itemsSource[3].IsShow = true;

            //if (obj is Button element)
            //{
            //    Vector vector = VisualTreeHelper.GetOffset(element);

            //    var po = Mouse.GetPosition(element);

            //    var po1 = Mouse.GetPosition(this);

            //    var point = element.PointToScreen(new Point(0, 0));

            //    //Popup popup = new Popup();
            //    //popup.PlacementTarget = element;
            //    //popup.Child = new UserControl1();
            //    //popup.IsOpen = true;
            //    //popup.StaysOpen = true;

            //    var point1 = element.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            //    IntPtr hwnd = ((HwndSource)PresentationSource.FromVisual(element)).Handle;
            //    window = new Window
            //    {
            //        Content = new UserControl1(),
            //        Width = 0,
            //        Height = 0,

            //        Owner = Application.Current.MainWindow,
            //        ShowInTaskbar = false
            //    };
            //    window.Show();

            //    IntPtr win = new WindowInteropHelper(window).Handle;
            //    IntPtr mainwindow = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            //    WinApiHelper.SetParent(win, mainwindow);
            //     x = po1.X - po.X - (100 - element.ActualWidth / 2);
            //     y = po1.Y - po.Y - 200;
            //    WinApiHelper.MoveWindow(win, (int)x, (int)y, 200, 200, false);
            //    if (Windows == null)
            //        Windows = new List<Window>();
            //    Windows.Add(window);
            //}
        });

        private void DesignSurface_Move(object sender, Hjmos.BaseControls.Data.FunctionEventArgs<Vector> e)
        {
            //if (Windows != null)
            //{
            //     x += e.Info.X;
            //    //var y = e.Info.Y;
            //    //window.Left += x;
            //    //window.Top += y;
            //    IntPtr win = new WindowInteropHelper(window).Handle;
            //      IntPtr mainwindow = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            //    WinApiHelper.SetParent(win, mainwindow);
            //    WinApiHelper.MoveWindow(win, (int)(x), (int)( y), 200, 200, false);
            //}
        }
    }
}