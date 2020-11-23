using System.Windows;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class ListBoxAttach : BorderElement
    {
        public static CornerRadius GetItemCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(ItemCornerRadiusProperty);
        }

        public static void SetItemCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(ItemCornerRadiusProperty, value);
        }
        /// <summary>
        /// 子项的边角
        /// </summary>
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCornerRadiusProperty =
            DependencyProperty.RegisterAttached("ItemCornerRadius", typeof(CornerRadius), typeof(ListBoxAttach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));





        public static double GetListBoxItemWith(DependencyObject obj)
        {
            return (double)obj.GetValue(ListBoxItemWithProperty);
        }

        public static void SetListBoxItemWith(DependencyObject obj, double value)
        {
            obj.SetValue(ListBoxItemWithProperty, value);
        }

        // Using a DependencyProperty as the backing store for ListBoxItemWith.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListBoxItemWithProperty =
            DependencyProperty.RegisterAttached("ListBoxItemWith", typeof(double), typeof(ListBoxAttach), new PropertyMetadata(50d));



    }
}
