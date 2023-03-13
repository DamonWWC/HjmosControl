using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// Window12.xaml 的交互逻辑
    /// </summary>
    public partial class Window12 : Window
    {
        public Window12()
        {
            InitializeComponent();
            VideoInfos = new List<VideoInfo> { new VideoInfo {VideoType=2, Name="A出入口", VideoInfos=new List<VideoInfo> { new VideoInfo {Name="1摄像头",VideoType=0 },new VideoInfo {Name="2摄像头" , VideoType = 0 } } },
                new VideoInfo { Name="B出入口", VideoInfos=new List<VideoInfo> { new VideoInfo {Name="1摄像头" },new VideoInfo {Name="2摄像头" } } },
                new VideoInfo { Name="C出入口", VideoInfos=new List<VideoInfo> { new VideoInfo {Name="1摄像头" },new VideoInfo {Name="2摄像头" } } },
                new VideoInfo { Name="D出入口", VideoInfos=new List<VideoInfo> { new VideoInfo {Name="1摄像头" },new VideoInfo {Name="2摄像头" } } },
            };
            DataContext = this;
        }

        public List<VideoInfo> VideoInfos { get; set; }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            base.ArrangeOverride(arrangeSize);
            return arrangeSize;
        }
    }

    public class VideoInfo
    {
        public string Name { get; set; }
        public int VideoType { get; set; }
        public List<VideoInfo> VideoInfos { get; set; }
    }
}