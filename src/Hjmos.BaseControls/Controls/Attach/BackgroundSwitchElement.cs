using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class BackgroundSwitchElement
    {
        /// <summary>
        /// 鼠标置于控件上时背景颜色
        /// </summary>
        public static readonly DependencyProperty MouseHoverBackgroundProperty = DependencyProperty.RegisterAttached(
          "MouseHoverBackground", typeof(Brush), typeof(BackgroundSwitchElement), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseHoverBackground(DependencyObject element, Brush value) => element.SetValue(MouseHoverBackgroundProperty, value);

        public static Brush GetMouseHoverBackground(DependencyObject element) => (Brush)element.GetValue(MouseHoverBackgroundProperty);

        /// <summary>
        /// 鼠标按下时背景颜色
        /// </summary>
        public static readonly DependencyProperty MouseDownBackgroundProperty = DependencyProperty.RegisterAttached(
            "MouseDownBackground", typeof(Brush), typeof(BackgroundSwitchElement), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseDownBackground(DependencyObject element, Brush value) => element.SetValue(MouseDownBackgroundProperty, value);

        public static Brush GetMouseDownBackground(DependencyObject element) => (Brush)element.GetValue(MouseDownBackgroundProperty);

        /// <summary>
        /// 鼠标选择某子项时候的颜色
        /// </summary>
        public static readonly DependencyProperty ItemSelectedBackgroundProperty = DependencyProperty.RegisterAttached(
           "ItemSelectedBackground", typeof(Brush), typeof(BackgroundSwitchElement), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetItemSelectedBackground(DependencyObject element, Brush value) => element.SetValue(ItemSelectedBackgroundProperty, value);

        public static Brush GetItemSelectedBackground(DependencyObject element) => (Brush)element.GetValue(ItemSelectedBackgroundProperty);
    }
}
