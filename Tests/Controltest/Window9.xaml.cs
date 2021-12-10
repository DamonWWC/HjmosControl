using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using WpfApp1;

namespace Controltest
{
    /// <summary>
    /// Window9.xaml 的交互逻辑
    /// </summary>
    public partial class Window9 : Window
    {
        public Window9()
        {
            InitializeComponent();
            DataContext = this;
            this.MouseMove += Window9_MouseMove;
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

        public ICommand ButtonCommand => new DelegateCommand<object>((obj) =>
                       {
                           if (obj is Button element)
                           {
                               Vector vector = VisualTreeHelper.GetOffset(element);

                               var po = Mouse.GetPosition(element);

                               var po1 = Mouse.GetPosition(Application.Current.MainWindow);

                               var point = element.PointToScreen(new Point(0, 0));

                              //Popup popup = new Popup();
                              //popup.PlacementTarget = element;
                              //popup.Child = new UserControl1();
                              //popup.IsOpen = true;
                              //popup.StaysOpen = true;

                              var point1 = element.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

                               IntPtr hwnd = ((HwndSource)PresentationSource.FromVisual(element)).Handle;
                               Window window = new Window
                               {
                                   Content = new UserControl1(),
                                   Width = 200,
                                   Height = 200,

                                   Owner = Application.Current.MainWindow,
                                   ShowInTaskbar = false
                               };
                               window.Show();
                               IntPtr win = new WindowInteropHelper(window).Handle;
                               IntPtr mainwindow = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                               WinApiHelper.SetParent(win, mainwindow);
                               WinApiHelper.MoveWindow(win, 0, 0, 200, 200, false);
                           }
                       });
    }

    public class FloodInformation
    {
        public string StationName { get; set; }
        public bool IsChangeStation { get; set; }

        public bool IsLast { get; set; }
    }
}