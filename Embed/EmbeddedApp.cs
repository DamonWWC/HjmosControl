using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Embed
{
   public class EmbeddedApp : HwndHost, IKeyboardInputSink
    {
        [DllImport("user32.dll")]
        private static extern int SetParent(IntPtr hWndChild, IntPtr hWndParent);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint newLong);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int EnumWindows(CallBackPtr callPtr, ref WindowInfo WndInfoRef);
        [DllImport("User32.dll")]
        static extern int GetWindowText(IntPtr handle, StringBuilder text, int MaxLen);
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT rc);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam); //对外部软件窗口发送一些消息(如 窗口最大化、最小化等)

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nCmdShow);

        internal const int
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12);
        internal const uint
              WS_CHILD = 0x40000000,
              WS_VISIBLE = 0x10000000,
              LBS_NOTIFY = 0x00000001,
              HOST_ID = 0x00000002,
              LISTBOX_ID = 0x00000001,
              WS_VSCROLL = 0x00200000,
              WS_BORDER = 0x00800000,
              WS_POPUP = 0x80000000;
        private const int HWND_TOP = 0x0;
        private const int WM_COMMAND = 0x0112;
        private const int WM_QT_PAINT = 0xC2DC;
        private const int WM_PAINT = 0x000F;
        private const int WM_SIZE = 0x0005;
        private const int SWP_FRAMECHANGED = 0x0020;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const uint WM_LBUTTONDOWN = 0x0201;
        private const uint WM_LBUTTONUP = 0x0202;
        private const int BM_CLICK = 0xF5;

        private Border WndHoster;
        private double screenW, screenH;
        private System.Diagnostics.Process appProc;
        private uint oldStyle;
        public IntPtr hwndHost;
        private String appPath;
        public EmbeddedApp(Border b, double sW, double sH, String p, String f)
        {
            WndHoster = b;
            screenH = sH;
            screenW = sW;
            appPath = p;
            WinInfo = new WindowInfo();
            WinInfo.winTitle = f;
        }
        public EmbeddedApp()
        {
             
        }


        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(EmbeddedApp), new PropertyMetadata(default(string)));




        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            appProc = new System.Diagnostics.Process();
            appProc.StartInfo.FileName = Source;
            appProc.StartInfo.UseShellExecute = false;
            appProc.StartInfo.CreateNoWindow = true;
            appProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            appProc.StartInfo.Arguments = $"aaa bbb";
            appProc.Start();

            int waitTime = 0;
            while (true)
            {
                if (appProc.MainWindowHandle != IntPtr.Zero)
                {
                    hwndHost = appProc.MainWindowHandle;
                    break;
                }
                else if (waitTime == 10)
                {
                    return new HandleRef(this, hwndHost);
                }
                Thread.Sleep(100);
                waitTime++;
            }


            //ShowWindow(hwndHost, 0);

            //SendMessage(hwndHost, WM_SYSCOMMAND, SC_MINIMIZE, 0);

            oldStyle = GetWindowLong(hwndHost, GWL_STYLE);
            uint newStyle = oldStyle;

            newStyle |= WS_CHILD;
            newStyle &= ~WS_POPUP;
            newStyle &= ~WS_BORDER;
            newStyle |= 0x00080000;
            SetWindowLong(hwndHost, GWL_STYLE, newStyle);
            //将窗口居中，实际上是将窗口的容器居中   
            //RePosWindow(WndHoster, WndHoster.Width, WndHoster.Height);

            //将netterm的父窗口设置为HwndHost    

            SetParent(hwndHost, hwndParent.Handle);

            //var aa = WinApiHelper.MoveWindow(hwndHost, 0, 0, 1, 1, true);
            //ShowWindow(hwndHost, 3);



            #region 创建一个透明的窗口

            //var hwnd = CreateWindowEx(
            //     0, "static", "",
            //     WS_CHILD | LBS_NOTIFY,
            //     0, 0,
            //     100, 100,
            //     hwndParent.Handle,
            //     IntPtr.Zero,
            //     IntPtr.Zero,
            //     IntPtr.Zero);

            //ShowWindow(hwnd, SW_SHOWDEFAULT);
            //UpdateWindow(hwnd);
            //// Set WS_EX_LAYERED on this window 
            //SetWindowLong(hwnd,
            //              GWL_EXSTYLE,
            //              GetWindowLong(hwnd, GWL_EXSTYLE) | 0x00080000);

            //// Make this window 70% alpha
            //var aa = SetLayeredWindowAttributes(hwnd, 0, (255 * 50) / 100, 0x00000002);
            #endregion


            return new HandleRef(this, hwndHost);
        }

    

       


        protected override void DestroyWindowCore(System.Runtime.InteropServices.HandleRef hwnd)
        {
            //SetWindowLong(hwndHost, GWL_STYLE, oldStyle);
            //SetParent(hwndHost, (IntPtr)0);
            // ShowWindow(hwndHost, 9);
        }

        public void PopupWindow()
        {
            //SetWindowLong(hwndHost, GWL_STYLE, oldStyle);
            //SetParent(hwndHost, (IntPtr)0);
            //ShowWindow(hwndHost, 9);
            this.Dispose();
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct WindowInfo
        {
            public String winTitle;
            public RECT r;
            public IntPtr hwnd;
        }
        public delegate bool CallBackPtr(IntPtr hwnd, ref WindowInfo WndInfoRef);
        private static CallBackPtr callBackPtr;
        private WindowInfo WinInfo;
        public static bool CallBackProc(IntPtr hwnd, ref WindowInfo WndInfoRef)
        {
            StringBuilder str = new StringBuilder(512);
            GetWindowText(hwnd, str, str.Capacity);
            Console.WriteLine(str.ToString());
            if (str.ToString().IndexOf(WndInfoRef.winTitle, 0) >= 0)
            {
                WndInfoRef.hwnd = hwnd;
                GetWindowRect(hwnd, ref (WndInfoRef.r));
            }

            return true;
        }
        public IntPtr FindTheWindow()
        {
            callBackPtr = new CallBackPtr(CallBackProc);
            EnumWindows(callBackPtr, ref WinInfo);
            return WinInfo.hwnd;
        }
        public void RePosWindow(Border b, double screenW, double screenH)
        {
            double width = WinInfo.r.right - WinInfo.r.left;
            double height = WinInfo.r.bottom - WinInfo.r.top;

            double left = (screenW - width) / 2;
            double right = (screenW - width) / 2;
            double top = (screenH - height) / 2;
            double bottom = (screenH - height) / 2;

            //b.Margin = new Thickness(left, top, right, bottom);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr CreateWindowEx(
     uint dwExStyle, string lpClassName, string lpWindowName,
     uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent,
     IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
         static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
            int cxDesired, int cyDesired, uint fuLoad);

        const int SW_SHOWDEFAULT = 10;
        const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
        
        const uint WS_CLIPCHILDREN = 0x02000000;
        const uint WS_CLIPSIBLINGS = 0x04000000;
        const uint WS_EX_CONTROLPARENT = 0x00010000;
        const uint IMAGE_BITMAP = 0;
        const uint LR_LOADFROMFILE = 0x10;
    }
}

