

using System.Windows;

namespace HjmosControl.Tools.Extension
{
    public static class UIElementExtension
    {
        /// <summary>
        /// 显示元素
        /// </summary>
        /// <param name="element"></param>
        public static void Show(this UIElement element) => element.Visibility = Visibility.Visible;
        /// <summary>
        /// 显示元素
        /// </summary>
        /// <param name="element"></param>
        /// <param name="show"></param>
        public static void Show(this UIElement element, bool show) => element.Visibility = show ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public static void Hide(this UIElement element) => element.Visibility = Visibility.Hidden;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public static void Collapse(this UIElement element) => element.Visibility = Visibility.Collapsed;
    }
}
