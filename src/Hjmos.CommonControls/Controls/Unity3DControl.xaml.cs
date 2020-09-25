using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Hjmos.CommonControls.Controls
{
    /// <summary>
    /// Unity3DControl.xaml 的交互逻辑
    /// </summary>
    public partial class Unity3DControl : UserControl
    {
        public Unity3DControl()
        {
            InitializeComponent();
            this.Loaded += Unity3DControlExtent_Loaded;
            this.IsVisibleChanged += Unity3DControl_IsVisibleChanged;
        }
        private void Panel_SizeChanged(object sender, EventArgs e)
        {
            if (unityHWND != IntPtr.Zero)
            {
                WinApiHelper.MoveWindow(unityHWND, 0, 0, Panel.Width, Panel.Height, true);
            }
        }


        /// <summary>
        /// 承载的窗体关闭时，关闭当前3D的进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Unity3DControl_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Close3DProcess();
        }

        private void Unity3DControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Start3DProcess();
        }

        private bool IsFirstLoad = false;

        private void Unity3DControlExtent_Loaded(object sender, RoutedEventArgs e)
        {
            //注册监听窗体关闭的事件
            if (IsVisible)
            {
                if (!IsFirstLoad)
                {
                    Window.GetWindow(this).Closing -= Unity3DControl_Closing;
                    Window.GetWindow(this).Closing += Unity3DControl_Closing;
                    IsFirstLoad = true;
                }

                if (unityHWND != IntPtr.Zero)
                {
                    WinApiHelper.MoveWindow(unityHWND, 0, 0, Panel.Width, Panel.Height, true);
                }
            }
        }

        private Process process;
        private IntPtr unityHWND = IntPtr.Zero;
        private const int WM_ACTIVATE = 0x0006;
        private readonly IntPtr WA_ACTIVE = new IntPtr(1);
        private readonly IntPtr WA_INACTIVE = new IntPtr(0);

        /// <summary>
        /// 当前3D的句柄
        /// </summary>
        public IntPtr CurrentUnity3DIntPtr
        {
            get { return unityHWND; }
        }

        /// <summary>
        /// 寄宿3D的主容器句柄
        /// </summary>
        public IntPtr BaseUnity3dParentHost
        {
            get { return Panel.Handle; }
        }

        /// <summary>
        /// 是否终止界面焦点检测轮训
        /// </summary>
        internal bool isStopCheckWindowActive
        {
            set
            {
                if (value)
                {
                    actionTime?.Stop();
                }
                else
                {
                    if (actionTime == null)
                    {
                        CheckWindowsActive();
                    }
                    actionTime?.Stop();
                    actionTime?.Start();
                }
            }
        }


        /// <summary>
        /// Unity3D的进程参数，用于给3D程序传递参数
        /// </summary>
        public string Unity3DProcessArges
        {
            get { return (string)GetValue(Unity3DProcessArgesProperty); }
            set { SetValue(Unity3DProcessArgesProperty, value); }
        }

        /// <summary>
        /// 3D进程参数的依赖属性,默认值为空字符串
        /// </summary>
        public static readonly DependencyProperty Unity3DProcessArgesProperty = DependencyProperty.Register("Unity3DProcessArges", typeof(string), typeof(Unity3DControl), new PropertyMetadata(string.Empty));


        /// <summary>
        /// 3D进程地址依赖属性
        /// </summary>
        public static readonly DependencyProperty Unity3DProcessUriProperty = DependencyProperty.Register("Unity3DProcessUri", typeof(string), typeof(Unity3DControl), new PropertyMetadata(null, new PropertyChangedCallback(Unity3DProcessUriPropertyChange)));


        /// <summary>
        /// 3D的进程路径改变触发回调
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <param name="e"></param>
        private static void Unity3DProcessUriPropertyChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is Unity3DControl control)
            {
                string OldProcessUri = (string)e.OldValue;
                string NewProcessUri = (string)e.NewValue;

                control.Old3DProcessName = OldProcessUri;
                if (OldProcessUri != NewProcessUri)
                {
                    control.unityHWND = IntPtr.Zero;
                    control.Current3DProcessName = NewProcessUri;
                    control.CheckProcessByName(OldProcessUri);  //先把就的3D进程关闭
                    control.Start3DProcess();
                }
            }
        }

        /// <summary>
        /// 当前创建3D的进程名称
        /// </summary>
        public string Unity3DProcessUri
        {
            get { return (string)GetValue(Unity3DProcessUriProperty); }
            set { SetValue(Unity3DProcessUriProperty, value); }
        }

        /// <summary>
        /// 设置当前播放的3D的进程URL
        /// </summary>
        public string Current3DProcessName { get; set; }

        /// <summary>
        /// 记录上一次播放的3d的进程URL
        /// </summary>
        public string Old3DProcessName { get; set; }


        /// <summary>
        /// 开启3D进程
        /// </summary>
        private void Start3DProcess()
        {
            if (IsVisible)
            {
                isStopCheckWindowActive = false;
                if ((!string.IsNullOrWhiteSpace(Current3DProcessName) && Old3DProcessName != Current3DProcessName) || (process != null && process.HasExited))
                {
                    Old3DProcessName = Current3DProcessName;
                    Create3DProcess(Current3DProcessName);
                }
            }
            else
            {
                isStopCheckWindowActive = true;
            }
        }

        /// <summary>
        /// 关闭当前3D进程
        /// </summary>
        internal void Close3DProcess()
        {
            try
            {
                if (actionTime != null)
                {
                    actionTime.Stop();
                    actionTime.Tick -= ActionTime_Tick;
                }

                if (process == null)
                {
                    return;
                }
                Task.Run(() =>
                {
                    // WinApiHelper.PostMessage(unityHWND, 0x0010, IntPtr.Zero, IntPtr.Zero);
                    if (!process.HasExited)
                    {
                        process?.CloseMainWindow();
                        Thread.Sleep(1000);
                        while (!process.HasExited)
                        {
                            process.Kill();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
               // LogHelper.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 开启检测鼠标在窗体上的运动
        /// </summary>
        DispatcherTimer actionTime;

        /// <summary>
        ///检测窗体焦点
        /// </summary>
        private void CheckWindowsActive()
        {
            actionTime = new DispatcherTimer();
            actionTime.Interval = new TimeSpan(TimeSpan.TicksPerMillisecond * 300);
            actionTime.Tick += ActionTime_Tick;
            actionTime.Start();
        }

        IntPtr lastIntprt = IntPtr.Zero; //获取上一次的鼠标指向的句柄,为了指向相同的位置，重复给窗体发送消息
        private void ActionTime_Tick(object sender, EventArgs e)
        {
            try
            {
                WinApiHelper.POINT pOINT;
                bool isSuccess = WinApiHelper.GetCursorPos(out pOINT);
                var intptr = WinApiHelper.WindowFromPoint(pOINT);
                if (isSuccess && unityHWND != IntPtr.Zero && lastIntprt != intptr)
                {
                    if (intptr == unityHWND)
                    {
                        ActiveWindows();
                    }
                    else
                    {
                        UnActiveWindows();
                    }

                }
                lastIntprt = intptr;

            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.ToString());
            }
        }


        /// <summary>
        /// 检测现在需要开启的3D进程是否还在运行，如果是，先关闭当前所运行的3D进程
        /// </summary>
        /// <param name="path">需要开启的3D的路径</param>
        internal void CheckProcessByName(string path)
        {
            var ProcessName = System.IO.Path.GetFileNameWithoutExtension(path);
            try
            {
                //精确进程名  用GetProcessesByName
                foreach (Process p in Process.GetProcessesByName(ProcessName))
                {
                    if (!p.CloseMainWindow())
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.ToString());
            }
        }

        //开启3D进程
        internal void Create3DProcess(string processUri)
        {
            if (string.IsNullOrWhiteSpace(processUri) || !File.Exists(processUri))
            {
                //LogHelper.Error("Unable to find Unity window,File was not exit");
                return;
                //throw new Exception("Unable to find Unity window,File was not exit");
            }
            var handle = Panel.Handle;

            // CheckWindowsActive();

            this.Dispatcher.InvokeAsync(() =>
            {
                try
                {
                    //WaitDialogWindow = new WaitDialogWindow("加载3D模型数据，请稍后！");
                    // WaitDialogWindow.Show();
                    //判断当前要启动的进程是否还在启动，如果还在启动，先关闭进程再创建进程
                    CheckProcessByName(processUri);

                    if (process != null)
                    {
                        process.Close();
                    }

                    //var arg = Unity3DProcessArges;
                    //var guid = Guid.NewGuid().ToString();
                    process = new Process();
                    //process.StartInfo.Arguments = "-parentHWND " + handle.ToInt32() + " " + Environment.CommandLine +" " + $"{guid}" + "," + "1920" + "," + "1080" +","+ "ws://172.23.127.60:8089/webim-server-zyf/websocket/"+ guid;
                    process.StartInfo.Arguments = "-parentHWND " + handle.ToInt32() + " " + Environment.CommandLine + " " + Unity3DProcessArges.Replace("AutoWidth", Panel.Width.ToString()).Replace("AutoHeight", Panel.Height.ToString());

                    process.StartInfo.FileName = processUri;
                    process.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(processUri);
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForInputIdle();
                    WinApiHelper.EnumChildWindows(handle, WindowEnum, IntPtr.Zero);

                }
                catch (Exception ex)
                {
                   // LogHelper.Error(ex.ToString());
                }
            });

        }

        private int WindowEnum(IntPtr hwnd, IntPtr lparam)
        {
            unityHWND = hwnd;
            //ActiveWindows();
            StartActiveUnitt3D();
            return 0;
        }

        /// <summary>
        /// 启动的瞬间开始激活3D的状态
        /// </summary>
        private void StartActiveUnitt3D()
        {
            Task.Run(() =>
            {
                Thread.Sleep(1500);
                ActiveWindows();

                //Dispatcher.Invoke(() =>
                //{
                //    WaitDialogWindow?.CloseWaitDialog(1500);
                //});
            });
        }

        /// <summary>
        /// 发送消息，触发激活当前窗体
        /// </summary>
        internal void ActiveWindows()
        {
            // WinApiHelper.SendMessage(unityHWND, WM_ACTIVATE, WA_ACTIVE, IntPtr.Zero);
            if (unityHWND != IntPtr.Zero)
            {
                WinApiHelper.SendMessage(unityHWND, WM_ACTIVATE, WA_ACTIVE, IntPtr.Zero);
            }
        }

        /// <summary>
        /// 解除激活窗体
        /// </summary>
        internal void UnActiveWindows()
        {
            if (unityHWND != IntPtr.Zero)
            {
                WinApiHelper.SendMessage(unityHWND, WM_ACTIVATE, WA_INACTIVE, IntPtr.Zero);
            }
        }
    }
}
