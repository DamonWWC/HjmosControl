using Hjmos.CommonControls.Commmon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using Vlc.DotNet.Forms;


namespace Hjmos.CommonControls.Controls
{
    /// <summary>
    /// CCTVControl.xaml 的交互逻辑
    /// </summary>
    public partial class CCTVControl : UserControl, INotifyPropertyChanged
    {
        public CCTVControl()
        {
            InitializeComponent();
            InitPlayer();
        }


        /// <summary>
        /// 判断初始化是否成功
        /// </summary>
        private bool IsInitSucess = false;

        /// <summary>
        /// 全局判断当前控件是否执行关闭
        /// </summary>
        private static bool IsControlClose = false;

        /// <summary>
        /// 订阅关闭窗体的事件。用于释放当前的播放器的资源
        /// </summary>
        private void CommomEvent_CloseEvent()
        {
            IsControlClose = true;
            if (_SwitchCameraTimer != null)
            {
                _SwitchCameraTimer.Stop();
                _SwitchCameraTimer.Tick -= _SwitchCameraTimer_Tick;
            }

            UnregisterPlayerEvents();
            // VlcProvider?.Dispose();
            VlcProvider = null;
        }


        private List<Camera> _UserSelfCameraList = new List<Camera>();
        /// <summary>
        /// 控件内专用的摄像头集合,防止推送重复的数据，导致影响部分效果
        /// </summary>
        public List<Camera> UserSelfCameraList
        {
            get { return _UserSelfCameraList; }
            set
            {
                _UserSelfCameraList = value;
                NotifyPropertyChanged(nameof(UserSelfCameraList));
            }
        }

        /// <summary>
        /// 摄像机列表
        /// </summary>
        public List<Camera> CameraList
        {
            get { return (List<Camera>)GetValue(CameraListProperty); }
            set { SetValue(CameraListProperty, value); }
        }

        public static readonly DependencyProperty CameraListProperty =
            DependencyProperty.RegisterAttached("CameraList", typeof(List<Camera>), typeof(CCTVControl), new PropertyMetadata(new List<Camera>(), new PropertyChangedCallback(OnCameraListChanged)));


        /// <summary>
        /// 下方文字的可见性
        /// </summary>
        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(CCTVControl), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// 底部导航栏的可见性
        /// </summary>
        public Visibility NavigationVisibility
        {
            get { return (Visibility)GetValue(NavigationVisibilityProperty); }
            set { SetValue(NavigationVisibilityProperty, value); }
        }

        public static readonly DependencyProperty NavigationVisibilityProperty =
            DependencyProperty.Register("NavigationVisibility", typeof(Visibility), typeof(CCTVControl), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 注册是否停止播放视频的以来属性
        /// </summary>
        public static readonly DependencyProperty IsStopVlcVideoProperty = DependencyProperty.Register("IsStopVlcVideo", typeof(bool), typeof(CCTVControl), new PropertyMetadata(false, new PropertyChangedCallback(IsStopVlcVideoPropertyChange)));

        private static void IsStopVlcVideoPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CCTVControl control)
            {
                bool isstop = (bool)e.NewValue;
                if (isstop)
                {
                    Task.Run(() =>
                    {
                        control.VlcProvider?.Stop();
                    });
                }
                else
                {
                    //control.VlcProvider?.Stop();
                    //control.VlcProvider?.ResetMedia();
                    control.VlcProvider?.Play();
                }
            }
        }

        /// <summary>
        /// 是否停止播放视频
        /// </summary>
        public bool IsStopVlcVideo
        {
            get { return (bool)GetValue(IsStopVlcVideoProperty); }
            set { SetValue(IsStopVlcVideoProperty, value); }
        }

        /// <summary>
        /// 底部导航栏点击按钮的形状是否为圆角显示的依赖属性，默认是false，不修改为正方形
        /// </summary>
        public static readonly DependencyProperty IsNavigationSharpRadiusProperty = DependencyProperty.Register("IsNavigationSharpRadius", typeof(bool), typeof(CCTVControl), new PropertyMetadata(false, new PropertyChangedCallback(NavigationSharpRadiusPropertyChange)));

        public bool IsNavigationSharpRadius
        {
            get { return (bool)GetValue(IsNavigationSharpRadiusProperty); }
            set { SetValue(IsNavigationSharpRadiusProperty, value); }
        }

        /// <summary>
        /// 触发回调修改底部导航栏按钮形状为圆角显示
        /// </summary>
        /// <param name="dependencyObject">当前修改的依赖属性主体</param>
        /// <param name="e">触发的改变值</param>
        private static void NavigationSharpRadiusPropertyChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is CCTVControl control)
            {
                bool isChangeSharp = (bool)e.NewValue;
                if (isChangeSharp)
                {
                    control.RadiusX = 3.5;
                    control.RadiusY = 3.5;
                }
                else
                {
                    control.RadiusX = 0;
                    control.RadiusY = 0;
                }
            }
        }

        private double _RadiusX;
        /// <summary>
        /// 用于绑定底部点击切换摄像头按钮的圆角X轴参数
        /// </summary>
        public double RadiusX
        {
            get
            {
                return _RadiusX;
            }

            set
            {
                _RadiusX = value;
            }
        }

        private double _RadiusY;
        /// <summary>
        /// 用于绑定底部点击切换摄像头按钮的圆角Y轴参数
        /// </summary>
        public double RadiusY
        {
            get
            {
                return _RadiusY;
            }

            set
            {
                _RadiusY = value;
            }
        }

        private bool _isSwitchInternal = false;
        /// <summary>
        /// 是否需要轮播（默认不需要轮播）
        /// </summary>
        public bool isNeedSwitchInternal
        {
            get { return _isSwitchInternal; }
            set { _isSwitchInternal = value; }
        }

        /// <summary>
        /// 设置当前的需要轮播的时间间隔（毫秒）
        /// </summary>
        public int SwitchInternal { get; set; } = 60 * 1000;

        private int _SelectIndex = 0;
        public int SelectIndex
        {
            get { return _SelectIndex; }
            set
            {
                if (CameraList.Count == 0) return;
                //if (_SelectIndex == value) return;

                if (value < 0)
                    _SelectIndex = CameraList.Count - 1;
                else if (value >= CameraList.Count)
                    _SelectIndex = 0;
                else
                    _SelectIndex = value;
                CurrentCamera = CameraList[_SelectIndex];
            }
        }


        private Camera _CurrentCamera;
        /// <summary>
        /// 当前播放的摄像机
        /// </summary>
        public Camera CurrentCamera
        {
            get
            {
                return _CurrentCamera;
            }
            set
            {
                if (value != _CurrentCamera)
                {
                    _CurrentCamera = value;
                    if (value != null)
                    {
                        NotifyPropertyChanged(nameof(CurrentCamera));
                        ResetUri(value);
                    }
                }
            }
        }

        /// <summary>
        /// 设置播放地址
        /// </summary>
        /// <param name="camera">摄像机</param>
        private void ResetUri(Camera camera)
        {
            if (IsControlClose)
            {
                return;
            }


            // lock (_Locker)
            {
                try
                {
                    if (string.IsNullOrEmpty(camera?.Url))
                    {
                        State = CCTVPlayState.Error;
                        return;
                    }

                    if (VlcProvider == null)
                    {
                        return;
                    }


                    ThreadPool.QueueUserWorkItem((o) =>
                    {

                        lock (_Locker)
                        {
                            try
                            {
                                int TickCount = 0;
                                while (!IsInitSucess && TickCount < 5)
                                {
                                    TickCount++;
                                    Thread.Sleep(1000);
                                }

                                //播放之前先执行关闭播放器
                                VlcProvider?.Stop();
                                var options = new string[] { "avcodec-hw=any" };
                                //VlcProvider?.ResetMedia();
                                VlcProvider?.Play(new Uri(camera.Url), options);
                                if (VlcProvider.Video != null)
                                {
                                    VlcProvider.Video.AspectRatio = $"{VlcProvider.Width}:{VlcProvider.Height}";
                                }
                            }
                            catch (Exception ex)
                            {
                                //LogHelper.Error(ex.ToString());
                            }
                        }
                    });


                }
                catch (Exception ex)
                {
                    State = CCTVPlayState.Error;
                    //LogHelper.Error(ex.ToString());
                }
            }
        }


        /// <summary>
        /// 注册播放器事件
        /// </summary>
        private void RegisterPlayerEvents()
        {
            if (VlcProvider == null)
            {
                return;
            }

            //先进行注销再注册
            UnregisterPlayerEvents();

            VlcProvider.MediaChanged += MediaPlayer_MediaChanged;
            VlcProvider.Playing += MediaPlayer_Playing;
            VlcProvider.Stopped += _Player_Stopped;
            VlcProvider.Paused += _Player_Paused;
        }

        /// <summary>
        /// 正在加载视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_MediaChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerMediaChangedEventArgs e)
        {
            State = CCTVPlayState.Loading;
        }

        /// <summary>
        /// 正在播放视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
        {
            State = CCTVPlayState.Playing;
            // ResetAspectRatio();
        }

        /// <summary>
        /// 暂停播放视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Player_Paused(object sender, Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs e)
        {
            State = CCTVPlayState.Pause;
        }

        /// <summary>
        /// 停止播放视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Player_Stopped(object sender, Vlc.DotNet.Core.VlcMediaPlayerStoppedEventArgs e)
        {
            State = CCTVPlayState.Stop;
        }

        /// <summary>
        /// 自动控制摄像机播放或停止
        /// </summary>
        private void AutoControlVideo()
        {
            if (IsVisible == true)
            {
                StartPlayVideo();
            }
            else
            {
                StopPlayVideo();
            }
        }

        /// <summary>
        /// 开始播放
        /// </summary>
        private void StartPlayVideo()
        {
            if (UserSelfCameraList?.Count > 0)
            {
                if (CurrentCamera == null)
                {
                    CurrentCamera = UserSelfCameraList[0];
                }


                if (UserSelfCameraList.Count > 1)
                {
                    //轮播
                    StartSwitchCameraTimer();
                }
            }
            else
            {
                CurrentCamera = null;
            }
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        private void StopPlayVideo()
        {
            _SwitchCameraTimer.Stop();
        }


        /// <summary>
        /// 启动多摄像头轮播的定时器
        /// </summary>
        private void StartSwitchCameraTimer()
        {
            if (!isNeedSwitchInternal)
            {
                return;
            }
            _TickCount = 0;
            if (_SwitchCameraTimer != null)
            {
                _SwitchCameraTimer.Stop();
                _SwitchCameraTimer.Tick -= _SwitchCameraTimer_Tick;
            }
            _SwitchCameraTimer = new DispatcherTimer();
            _SwitchCameraTimer.Interval = TimeSpan.FromMilliseconds(SwitchInternal);

            _SwitchCameraTimer.Tick += _SwitchCameraTimer_Tick;
            _SwitchCameraTimer.Start();
        }

        private void _SwitchCameraTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsControlClose)
                {
                    return;
                }
                // lock (_Locker)
                {
                    //判断是否需要轮播
                    if (_isSwitchInternal)
                    {
                        if (UserSelfCameraList == null || UserSelfCameraList.Count == 0)
                            return;

                        if (_TickCount < UserSelfCameraList.Count - 1)
                        {
                            _TickCount++;
                        }
                        else
                        {
                            _TickCount = 0;
                        }
                        CurrentCamera = UserSelfCameraList[_TickCount];
                        // CurrentCamera = UserSelfCameraList[++_TickCount % UserSelfCameraList.Count];
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 设置视频宽高比
        /// </summary>
        private void ResetAspectRatio()
        {
            if (IsPlaying)
            {
                try
                {
                    if (VlcProvider != null && VlcProvider.Video != null)
                    {
                        VlcProvider.Video.AspectRatio = $"{VlcProvider.Width}:{VlcProvider.Height}";
                    }
                }
                catch (Exception ex)
                {
                    //LogHelper.Error(ex.ToString());
                }
            }
        }


        /// <summary>
        /// 摄像机列表改变回调事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnCameraListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var oldList = e.OldValue as List<Camera>;
            var newList = e.NewValue as List<Camera>;

            if (!CameraIsEqual(newList,oldList))
            {
                if (d is CCTVControl vlcPlayerControl)
                {
                    if (IsControlClose)
                    {
                        return;
                    }
                    vlcPlayerControl.UserSelfCameraList = vlcPlayerControl.GetNewCameras(newList);
                    vlcPlayerControl.CurrentCamera = null;
                    vlcPlayerControl.AutoControlVideo();
                }
            }
        }
        /// <summary>
        /// 判断两个摄像头列表是否相等
        /// </summary>
        /// <param name="lstA"></param>
        /// <param name="lstB"></param>
        /// <returns></returns>
        private static bool CameraIsEqual(List<Camera> lstA,List<Camera> lstB)
        {
            if (lstA == lstB)
            {
                return true;
            }
            else if (lstA != null && lstB != null && lstA.Count != lstB.Count)
            {
                return false;
            }

            var lstCodeA = lstA?.Select(x => x.CameraCode).ToList() ?? new List<string>();
            var lstCodeB = lstB?.Select(x => x.CameraCode).ToList() ?? new List<string>();

            if (lstCodeA.Except(lstCodeB)?.Count() <= 0 && lstCodeB.Except(lstCodeA)?.Count() <= 0)
            {
                lstCodeA = lstA?.Select(x => x.MonitorPlace).ToList() ?? new List<string>();
                lstCodeB = lstB?.Select(x => x.MonitorPlace).ToList() ?? new List<string>();
            }

            return lstCodeA.Except(lstCodeB)?.Count() <= 0 && lstCodeB.Except(lstCodeA)?.Count() <= 0;
        }

        /// <summary>
        /// 数据转化
        /// </summary>
        /// <param name="cameras"></param>
        /// <returns></returns>
        private List<Camera> GetNewCameras(List<Camera> cameras)
        {
            List<Camera> list = new List<Camera>();
            if (cameras != null && cameras.Count > 0)
            {
                foreach (var item in cameras)
                {
                    Camera camera = new Camera();
                    camera.CameraChannel = item.CameraChannel;
                    camera.CameraCode = item.CameraCode;
                    camera.CameraDesc = item.CameraDesc;
                    camera.CameraLocation = item.CameraLocation;
                    camera.CameraModule = item.CameraModule;
                    camera.CameraName = item.CameraName;
                    camera.CameraType = item.CameraType;
                    camera.MonitorPlace = item.MonitorPlace;
                    camera.Url = item.Url;
                    list.Add(camera);
                }
            }
            return list;
        }
        /// <summary>
        /// 判断当前是否注册了关闭窗体顺带关闭当前控件的事件
        /// </summary>
        private bool isRegistedCloseEvent = false;

        private void VlcPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            ResetAspectRatio();

            if (!isRegistedCloseEvent && IsVisible)
            {
                Window.GetWindow(this).Closing -= VlcPlayerClosing;
                Window.GetWindow(this).Closing += VlcPlayerClosing;
                isRegistedCloseEvent = true;
            }
        }

        /// <summary>
        /// 关闭窗体，顺带关闭控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VlcPlayerClosing(object sender, CancelEventArgs e)
        {
            CommomEvent_CloseEvent();
        }

        private void VlcPlayer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResetAspectRatio();
        }

        /// <summary>
        /// 控件可见状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VlcPlayer_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AutoControlVideo();
        }

        /// <summary>
        /// 点击导航栏图标切换摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Rectangle img && img.DataContext is Camera camera)
            {
                CurrentCamera = camera;
                if (isNeedSwitchInternal)
                {
                    var index = UserSelfCameraList.IndexOf(CurrentCamera);
                    _TickCount = index < 0 ? 0 : index;
                    _SwitchCameraTimer.Stop();
                    _SwitchCameraTimer.Start();
                }
            }
        }

        #region 字段，属性
        private const int INITPLAYER_INTERVAL = 30 * 1000;       //播放器初始化失败后重试的事件间隔（毫秒）
                                                                 // private const int SWITCH_INTERNAL = 60 * 1000;   //摄像头轮播的时间间隔（毫秒）
        private int _TickCount = 0;
        private DispatcherTimer _SwitchCameraTimer = new DispatcherTimer();   //用于摄像头轮播的定时器
        private static readonly object _Locker = new object();   //锁

        private CCTVPlayState _State = CCTVPlayState.Uninitialized;
        /// <summary>
        /// 播放器状态
        /// </summary>
        public CCTVPlayState State
        {
            get
            {
                return _State;
            }
            protected set
            {
                if (value != _State)
                {
                    _State = value;
                    NotifyPropertyChanged(nameof(State));
                    NotifyPropertyChanged(nameof(ImageVisible));
                    NotifyPropertyChanged(nameof(PlayerVisible));
                    NotifyPropertyChanged(nameof(IsInitialized));
                    NotifyPropertyChanged(nameof(IsPlaying));
                }
            }
        }

        /// <summary>
        /// 播放器是否初始化成功
        /// </summary>
        private bool IsInitPlayerSuccess => !new CCTVPlayState[] { CCTVPlayState.Uninitialized, CCTVPlayState.Error }.Contains(State);

        /// <summary>
        /// 播放器是否已初始化
        /// </summary>
        public new bool IsInitialized => State == CCTVPlayState.Initialized;

        /// <summary>
        /// 是否正在播放视频
        /// </summary>
        public bool IsPlaying => State == CCTVPlayState.Playing;

        /// <summary>
        /// 是否在播放的过程中
        /// </summary>
        private bool _IsPlay => new CCTVPlayState[] { CCTVPlayState.Playing, CCTVPlayState.Pause }.Contains(State);

        /// <summary>
        /// 默认图片的可见性
        /// </summary>
        public Visibility ImageVisible => _IsPlay ? Visibility.Collapsed : Visibility.Visible;

        /// <summary>
        /// 播放器可见性
        /// </summary>
        public Visibility PlayerVisible => _IsPlay ? Visibility.Visible : Visibility.Hidden;

        public event PropertyChangedEventHandler PropertyChanged;


        private VlcControl _VlcProvider;
        /// <summary>
        /// VLC播放器提供者
        /// </summary>
        public VlcControl VlcProvider
        {
            get
            {
                return _VlcProvider;
            }
            set
            {
                _VlcProvider = value;
                NotifyPropertyChanged(nameof(this.VlcProvider));
            }
        }

        #endregion

        private void InitPlayer()
        {
            try
            {
                State = CCTVPlayState.Initializing;

                VlcProvider = new VlcControl { Spu = -1, Dock = System.Windows.Forms.DockStyle.Fill };
                this.windowsFormsHost.Child = VlcProvider;

                DirectoryInfo vlcLibDirectory;

                //根据当前的程序的处理位数来获取不同的vlcLib  
                if (Environment.Is64BitProcess)
                {
                    vlcLibDirectory = new DirectoryInfo($@"VLC\VLC_x64");
                }
                else
                {
                    vlcLibDirectory = new DirectoryInfo($@"VLC\VLC_x86");
                }

                //判断当前的文件夹是否存在
                if (!vlcLibDirectory.Exists)
                {
                    //LogHelper.Error($"the Vlc's dll Path:{vlcLibDirectory.FullName} is not Exist,Please Reload and try again......");
                }

                //参数配置
                string[] options;
                options = new string[] { "-vvv", "--avcodec-hw=any" };

                VlcProvider.BeginInit();
                VlcProvider.VlcLibDirectory = vlcLibDirectory;
                VlcProvider.VlcMediaplayerOptions = options;

                VlcProvider.EndInit();

                State = CCTVPlayState.Initialized;

                //注册播放器事件
                RegisterPlayerEvents();

                IsInitSucess = true;
            }
            catch (Exception ex)
            {
                State = CCTVPlayState.Error;
                throw ex;
            }
        }

        /// <summary>
        /// 设置当前的联动刷新界面
        /// </summary>
        /// <param name="PropertyName"></param>
        private void NotifyPropertyChanged(string PropertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 注销播放器事件
        /// </summary>
        private void UnregisterPlayerEvents()
        {
            if (VlcProvider == null)
            {
                return;
            }

            VlcProvider.MediaChanged -= MediaPlayer_MediaChanged;
            VlcProvider.Playing -= MediaPlayer_Playing;
            VlcProvider.Stopped -= _Player_Stopped;
            VlcProvider.Paused -= _Player_Paused;
        }

        private void ButtonPrev_OnClick(object sender, RoutedEventArgs e) => SelectIndex--;


        private void ButtonNext_OnClick(object sender, RoutedEventArgs e) => SelectIndex++;
    }
}
