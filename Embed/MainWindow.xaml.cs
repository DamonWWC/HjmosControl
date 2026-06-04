using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Embed
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            //RenderTargetBitmap renderTarget = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Pbgra32);

            //renderTarget.Render(button);
            ////Image img = new Image();
            ////img.Source = renderTarget;
            //ImageSource = renderTarget;
            Button button = new System.Windows.Controls.Button
            {
                Background = Brushes.Red,
                BorderThickness=new Thickness(0)
            };
            VisualBrush = new VisualBrush
            {
                Stretch = Stretch.Fill,
                Visual = button
            };


            this.Loaded += MainWindow_Loaded;
        }
        EmbeddedApp ea;
        public RenderTargetBitmap ImageSource { get; set; }
        public VisualBrush VisualBrush { get; set; }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //RenderTargetBitmap renderTarget = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Pbgra32);

            //renderTarget.Render(button);
            ////Image img = new Image();
            ////img.Source = renderTarget;
            //ImageSource = renderTarget;

            //ea = new EmbeddedApp(border,100,100, "EmbedWindow.exe", "IMAgenGINE_MRDP");
            //border.Child = ea;
        }
        Window window;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

           
            //var dispatcher = UIDispatcher.RunNew("Background UI");
            //dispatcher.Invoke(() =>
            //{
            //    window = new Window
            //    {
            //        Content = new UserControl1()
            //    };

            //    //window.SourceInitialized += Window_SourceInitialized;
            //    window.Show();
            //});

          //await  host.SetChildAsync(new UserControl1());

            //Process appProc = new System.Diagnostics.Process();
            //appProc.StartInfo.FileName = "EmbedWindow.exe";
            //appProc.StartInfo.UseShellExecute = false;
            //appProc.StartInfo.CreateNoWindow = true;
            //appProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //appProc.StartInfo.Arguments = $"aaa bbb";
            //appProc.Start();

            //int waitTime = 0;
            //while (true)
            //{
            //    if (appProc.MainWindowHandle != IntPtr.Zero)
            //    {
            //        //hwndHost = appProc.MainWindowHandle;
            //        break;
            //    }
            //    else if (waitTime == 10)
            //    {
            //        break;
            //    }
            //    Thread.Sleep(100);
            //    waitTime++;
            //}
            //WinApiHelper.MoveWindow(appProc.MainWindowHandle, 0, 0, 1000, 1000, true);

        }
       
        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           // var timee = SystemInformation.DoubleClickTime;
        }
    }
}
