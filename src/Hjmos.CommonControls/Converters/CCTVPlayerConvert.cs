using System;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;
using Hjmos.CommonControls.Commmon;

namespace Hjmos.CommonControls.Converters
{
    // <summary>
    /// 播放器状态转换为颜色
    /// </summary>
    internal class PlayStateToColorConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CCTVPlayState state)
            {
                switch (state)
                {
                    case CCTVPlayState.Uninitialized:
                        return Brushes.White;
                    case CCTVPlayState.Initializing:
                        return Brushes.LightGray;
                    case CCTVPlayState.Initialized:
                        return Brushes.Green;
                    case CCTVPlayState.Loading:
                        return Brushes.DeepSkyBlue;
                    case CCTVPlayState.Playing:
                        return Brushes.Orange;
                    case CCTVPlayState.Pause:
                        return Brushes.Yellow;
                    case CCTVPlayState.Stop:
                        return Brushes.Gray;
                    case CCTVPlayState.Error:
                    default:
                        return Brushes.Red;
                }
            }

            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 播放器状态转换为中文
    /// </summary>
    internal class PlayStateToTextConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CCTVPlayState state)
            {
                switch (state)
                {
                    case CCTVPlayState.Uninitialized:
                        return "未初始化";
                    case CCTVPlayState.Initializing:
                        return "正在初始化...";
                    case CCTVPlayState.Initialized:
                        return "准备就绪";
                    case CCTVPlayState.Loading:
                        return "正在接入视频...";
                    case CCTVPlayState.Playing:
                        return "正在播放视频...";
                    case CCTVPlayState.Pause:
                        return "暂停播放";
                    case CCTVPlayState.Stop:
                        return "停止播放";
                    case CCTVPlayState.Error:
                    default:
                        return "出现异常";
                }
            }

            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 导航栏图标转换器
    /// </summary>
    internal class NavigationImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var name = values?.Length > 1 && object.Equals(values[0], values[1]) ? "焦点项" : "非焦点项";

            Brush brush;
            if (name == "焦点项")
            {
                brush = Brushes.SkyBlue;
            }
            else
            {
                brush = Brushes.Black;
            }

            return brush; 
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
