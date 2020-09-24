using System.Windows;
using System.Windows.Controls;

namespace Hjmos.CommonControls.Commmon
{
   internal class CCTVPlayStateSelector : DataTemplateSelector
    {
        /// <summary>
        /// 状态为暂停和播放时，显示摄像头信息
        /// </summary>
        public DataTemplate Play { get; set; }

        /// <summary>
        /// 状态为其他时，显示状态信息
        /// </summary>
        public DataTemplate Other { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CCTVPlayState state)
            {
                switch (state)
                {
                    case CCTVPlayState.Pause:
                    case CCTVPlayState.Playing:
                        return Play;
                }
            }

            return Other;
        }
    }
}
