using Hjmos.BaseControls.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = "PART_Icon", Type = typeof(SvgBox))]
    public class Menu : Expander
    {
        private double _animationLength;
        private double _defaultLength;
        private Storyboard storyboard;
        private SvgBox PART_Icon;

        public Menu()
        {
            Loaded += Menu_Loaded;
            Unloaded += Menu_Unloaded;
        }

        private void Menu_Unloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Menu_Loaded;
            //if (storyboard != null)
            //{
            //    storyboard.Completed -= Storyboard_Completed;
            //}
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Icon = GetTemplateChild("PART_Icon") as SvgBox;
            _defaultLength = PART_Icon.Height;
        }

        protected override void OnExpanded()
        {
            var animation = AnimationHelper.CreateAnimation(_animationLength, 500);
            BeginAnimation(HeightProperty, animation);
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            if (Content == null)
            {
                _animationLength = _defaultLength;
                return;
            }
            var _content = (UIElement)Content;
            _content.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var size = _content.DesiredSize;
            _animationLength = size.Height;

            storyboard = new Storyboard();
            storyboard.Completed += Storyboard_Completed;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!IsExpanded) return;
            var animation = AnimationHelper.CreateAnimation(_defaultLength, 500);
            animation.BeginTime = System.TimeSpan.FromSeconds(5);
            animation.From = _animationLength;
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.HeightProperty));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void Storyboard_Completed(object sender, System.EventArgs e)
        {
            IsExpanded = false;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (IsExpanded)
            {
                storyboard?.Stop();
                this.Height = _animationLength;
            }
        }
    }
}