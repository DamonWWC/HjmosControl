using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class InfoElement : TitleElement
    {

        /// <summary>
        ///     占位符
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached(
            "Placeholder", typeof(string), typeof(InfoElement), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetPlaceholder(DependencyObject element, string value) => element.SetValue(PlaceholderProperty, value);

        public static string GetPlaceholder(DependencyObject element) => (string)element.GetValue(PlaceholderProperty);

        /// <summary>
        ///     是否必填
        /// </summary>
        public static readonly DependencyProperty NecessaryProperty = DependencyProperty.RegisterAttached(
            "Necessary", typeof(bool), typeof(InfoElement), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetNecessary(DependencyObject element, bool value) => element.SetValue(NecessaryProperty, value);

        public static bool GetNecessary(DependencyObject element) => (bool)element.GetValue(NecessaryProperty);
    }
}
