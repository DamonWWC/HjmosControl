using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Windows;
using LibVLCSharp.WPF;

namespace Embed
{
    [ContentProperty("Source")]
    public class EmbeddedWindow : HwndHost, IKeyboardInputSink
    {
        public EmbeddedWindow()
        {

        }

        private Window window;
        IntPtr hwndHost = IntPtr.Zero;
        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
          
            //var dispatcher = UIDispatcher.RunNew("Background UI");
            //dispatcher.Invoke(() =>
            //{
              
            //});
            window = new Window();

            void Window_SourceInitialized(object sender, EventArgs e) { Window_SourceInitialized1(sender, hwndParent.Handle); }
            window.SourceInitialized += Window_SourceInitialized;
            window.Show();
            hwndHost = new WindowInteropHelper(window).Handle;
            return new HandleRef(this, hwndHost);

        }

        private void Window_SourceInitialized1(object sender, IntPtr e)
        {
            //hwndHost = new WindowInteropHelper((Window)sender).Handle;
            //_ = WinApiHelper.SetWindowLong(hwndHost,
            //        WinApiHelper.WindowLong.GWL_EXSTYLE,
            //      WinApiHelper.GetWindowLong(hwndHost, WinApiHelper.WindowLong.GWL_EXSTYLE) | ((uint)WinApiHelper.WindowExtendStyle.WS_EX_LAYERED));
            //WinApiHelper.SetParent(hwndHost, e);
            //WinApiHelper.MoveWindow(hwndHost, 0, 0, 300, 300, true);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
           
        }




        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(EmbeddedWindow), new PropertyMetadata(default(object)));


    }
}
