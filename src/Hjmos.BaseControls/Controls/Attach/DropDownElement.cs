using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class DropDownElement
    {
        public static readonly DependencyProperty ConsistentWidthProperty = DependencyProperty.RegisterAttached(
            "ConsistentWidth", typeof(bool), typeof(DropDownElement),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetConsistentWidth(DependencyObject element, bool value)
            => element.SetValue(ConsistentWidthProperty, value ? true : false);

        public static bool GetConsistentWidth(DependencyObject element)
            => (bool)element.GetValue(ConsistentWidthProperty);
    }
}
