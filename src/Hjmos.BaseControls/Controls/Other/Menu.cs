using Hjmos.BaseControls.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hjmos.BaseControls.Controls
{
    public class Menu : Expander
    {
        private double _animationLength;
        private ContentControl _content;

        public Menu()
        {
            Loaded += Menu_Loaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnExpanded()
        {
            var animation = AnimationHelper.CreateAnimation(_animationLength, 500);
            BeginAnimation(HeightProperty, animation);
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            _content = (ContentControl)Content;
            _content.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var size = _content.DesiredSize;
            _animationLength = size.Height;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            var animation = AnimationHelper.CreateAnimation(16, 500);
            animation.Completed += Animation_Completed;
            BeginAnimation(HeightProperty, animation);
        }

        private void Animation_Completed(object sender, System.EventArgs e)
        {
            IsExpanded = false;
        }
    }
}