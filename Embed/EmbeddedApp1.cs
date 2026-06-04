using LibVLCSharp.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Embed
{
    internal class EmbeddedApp1: HwndHost, IKeyboardInputSink
    {

        public EmbeddedApp1()
        {
            
        }

        private Process appProc;
        private uint oldStyle;
        private IntPtr hwndHost = IntPtr.Zero;


        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            appProc = new Process();
            appProc.StartInfo.FileName = Source;
            appProc.StartInfo.UseShellExecute = false;
            appProc.StartInfo.CreateNoWindow = true;
            appProc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            appProc.StartInfo.Arguments = "";

            if (!string.IsNullOrWhiteSpace(Source) && appProc.Start())
            {
                int waitTime = 1;
                Thread.Sleep(1000);
                while (appProc.MainWindowHandle.ToInt32()==0)
                {
                    Thread.Sleep(1000);
                }
                //while (true)
                //{
                //    if (appProc.MainWindowHandle != IntPtr.Zero)
                //    {
                //        hwndHost = appProc.MainWindowHandle;
                //        break;
                //    }
                //    else if (waitTime == 10)
                //    {
                //        break;
                //    }
                //    Thread.Sleep(5000);
                //    waitTime++;
                //}
                hwndHost = appProc.MainWindowHandle;
                if (hwndHost == IntPtr.Zero)
                {
                    hwndHost = CreateEmptyWindow(hwndParent.Handle);
                }
                else
                {
                    oldStyle = WinApiHelper.GetWindowLong(hwndHost, WinApiHelper.WindowLong.GWL_STYLE);
                    uint newStyle = oldStyle;
                    newStyle |= ((uint)WinApiHelper.WindowStyle.WS_CHILD);
                    newStyle &= ~((uint)WinApiHelper.WindowStyle.WS_POPUP);
                    newStyle &= ~((uint)WinApiHelper.WindowStyle.WS_BORDER);
                    _ = WinApiHelper.SetWindowLong(hwndHost, WinApiHelper.WindowLong.GWL_STYLE, newStyle);
                    WinApiHelper.SetParent(hwndHost, hwndParent.Handle);
                }
            }
            else
            {
                hwndHost = CreateEmptyWindow(hwndParent.Handle);
            }

            return new HandleRef(this, hwndHost);

        }



        private IntPtr CreateEmptyWindow(IntPtr parentHandle)
        {
            var hwnd = WinApiHelper.CreateWindowEx(
                 0, "static", "",
                 (int)(WinApiHelper.WindowStyle.WS_CHILD | WinApiHelper.WindowStyle.LBS_NOTIFY),
                 0, 0,
                 1, 1,
                 parentHandle,
                 IntPtr.Zero,
                 IntPtr.Zero,
                 IntPtr.Zero);

            //WinApiHelper.ShowWindow(hwnd, WinApiHelper.WindowShowStyle.SW_SHOWDEFAULT);
            //WinApiHelper.UpdateWindow(hwnd);
            // Set WS_EX_LAYERED on this window 
            _ = WinApiHelper.SetWindowLong(hwnd,
                         WinApiHelper.WindowLong.GWL_EXSTYLE,
                       WinApiHelper.GetWindowLong(hwnd, WinApiHelper.WindowLong.GWL_EXSTYLE) | ((uint)WinApiHelper.WindowExtendStyle.WS_EX_LAYERED));

            // Make this window 70% alpha
            WinApiHelper.SetLayeredWindowAttributes(hwnd, 0, (255 * 0) / 100, 0x00000002);

            return hwnd;
        }


        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            if (!appProc.CloseMainWindow())
            {
                appProc.Kill();
            }

            User32Wrapper.DestroyWindow(hwnd.Handle);

        }




        /// <summary>
        /// 嵌入的资源或资源路径
        /// </summary>
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(EmbeddedApp), new PropertyMetadata(default(string)));


    }
}
