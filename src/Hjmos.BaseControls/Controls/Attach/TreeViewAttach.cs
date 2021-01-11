using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    internal class TreeViewAttach
    {
        internal static readonly DependencyProperty IsCheckTreeViewProperty = DependencyProperty.RegisterAttached(
          "IsCheckTreeView", typeof(bool), typeof(TreeViewAttach), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetIsCheckTreeView(DependencyObject element, bool value) => element.SetValue(IsCheckTreeViewProperty, value);

        public static bool GetIsCheckTreeView(DependencyObject element) => (bool)element.GetValue(IsCheckTreeViewProperty);
    }
}
