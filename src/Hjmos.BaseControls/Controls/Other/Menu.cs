using Hjmos.BaseControls.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Hjmos.BaseControls.Controls
{
    public class Menu : ContentControl
    {
        private double _animationLength;

        public Menu()
        {
            Loaded += Menu_Loaded;
            Unloaded += Menu_Unloaded;
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Menu), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (Menu)d;
            ctl.OnIsOpenChanged((bool)e.NewValue);
        }

        private void OnIsOpenChanged(bool isOpen)
        {
            if (Content == null) return;
        }

        private void Menu_Unloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Menu_Loaded;
            if (storyboard != null)
            {
                storyboard.Completed -= Storyboard_Completed;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        //protected override void OnExpanded()
        //{
        //    var animation = AnimationHelper.CreateAnimation(_animationLength, 500);
        //    BeginAnimation(HeightProperty, animation);
        //}

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsOpen)
            {
                OnIsOpenChanged(true);
            }

            if (Content == null)
            {
                _animationLength = 16;
                return;
            }
            var _content = (UIElement)Content;
            _content.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var size = _content.DesiredSize;
            _animationLength = size.Height;
            storyboard = new Storyboard();
            storyboard.Completed += Storyboard_Completed;
            var animation = AnimationHelper.CreateAnimation(16, 3000);
            //animation.BeginTime = System.TimeSpan.FromSeconds(2);
            animation.From = _animationLength;
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
            storyboard.Children.Add(animation);
        }

        private Storyboard storyboard;

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!IsExpanded) return;

            //storyboard.BeginTime = System.TimeSpan.FromSeconds(2);
            storyboard.Begin();
            //animation.Completed += Animation_Completed;
            //BeginAnimation(HeightProperty, animation);
        }

        private void Storyboard_Completed(object sender, System.EventArgs e)
        {
            IsExpanded = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
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