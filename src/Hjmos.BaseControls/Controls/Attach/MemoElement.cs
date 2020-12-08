using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class MemoElement
    {
          /// <summary>
        ///     占位符
        /// </summary>
        public static readonly DependencyProperty MemoTextProperty = DependencyProperty.RegisterAttached(
            "MemoText", typeof(string), typeof(MemoElement), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMemoText(DependencyObject element, string value) => element.SetValue(MemoTextProperty, value);

        public static string GetMemoText(DependencyObject element) => (string)element.GetValue(MemoTextProperty);
    }
}
