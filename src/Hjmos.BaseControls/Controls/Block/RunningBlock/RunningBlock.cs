using Hjmos.BaseControls.Expression.Drawing;
using Hjmos.BaseControls.Tools;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Hjmos.BaseControls.Controls
{

    [TemplatePart(Name = "PART_Panel", Type = typeof(Panel))]

    public class RunningBlock : Control
    {
        protected Storyboard _storyboard;

        //private TextBlock _elementContent;
        private Panel _elementPanel;

        public RunningBlock()
        {
            this.SizeChanged += RunningBlock_SizeChanged;
            this.Loaded += RunningBlock_Loaded;
        }
        bool _isFirstUpdata = true;
        private void RunningBlock_Loaded(object sender, RoutedEventArgs e)
        {
            _isFirstUpdata = false;
        }

        private void RunningBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateContent();
        }

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            _elementPanel = GetTemplateChild("PART_Panel") as Panel;
            //UpdateContent();
        }


        public bool Runaway
        {
            get { return (bool)GetValue(RunawayProperty); }
            set { SetValue(RunawayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Runaway.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RunawayProperty =
            DependencyProperty.Register("Runaway", typeof(bool), typeof(RunningBlock), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));



        public bool AutoRun
        {
            get { return (bool)GetValue(AutoRunProperty); }
            set { SetValue(AutoRunProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoRun.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoRunProperty =
            DependencyProperty.Register("AutoRun", typeof(bool), typeof(RunningBlock), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
           "Orientation", typeof(Orientation), typeof(RunningBlock), new FrameworkPropertyMetadata(default(Orientation), FrameworkPropertyMetadataOptions.AffectsRender));

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            "Duration", typeof(Duration), typeof(RunningBlock), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(5)), FrameworkPropertyMetadataOptions.AffectsRender));

        public Duration Duration
        {
            get => (Duration)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register(
          "Speed", typeof(double), typeof(RunningBlock), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Speed
        {
            get => (double)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }



        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRunning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register("IsRunning", typeof(bool), typeof(RunningBlock), new PropertyMetadata(true, (o, args) =>
            {
                var ct1 = (RunningBlock)o;
                var v = (bool)args.NewValue;
                if (v)
                {
                    ct1._storyboard?.Resume();
                }
                else
                {
                    ct1._storyboard?.Pause();
                }
            }));

        public static readonly DependencyProperty AutoReverseProperty = DependencyProperty.Register(
          "AutoReverse", typeof(bool), typeof(RunningBlock), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool AutoReverse
        {
            get => (bool)GetValue(AutoReverseProperty);
            set => SetValue(AutoReverseProperty, value);
        }



        public bool IsForward
        {
            get { return (bool)GetValue(IsForwardProperty); }
            set { SetValue(IsForwardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsForward.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsForwardProperty =
            DependencyProperty.Register("IsForward", typeof(bool), typeof(RunningBlock), new PropertyMetadata(true));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(RunningBlock), new PropertyMetadata(default(string), (o, args) =>
            {
                if (o is RunningBlock runningBlock && args.NewValue is string value)
                {
                    runningBlock.UpdateContent();
                }
            }));

        private TextBlock CreateTextBlock()
        {

            var transform = new TransformGroup();
            transform.Children.Add(new TranslateTransform());

            return new TextBlock
            {
                Style = ResourceHelper.GetResource<Style>("RunningTextBlock"),
                Text = Text,
                FontSize = FontSize,
                Foreground = Foreground
            };

        }

        private void UpdateContent()
        {
            if (_elementPanel == null) return;
            _storyboard?.Stop();
            var offsetx = _elementPanel.RenderTransform.Value.OffsetX;

            _elementPanel.Children.Clear();
            var textblock = CreateTextBlock();
            _elementPanel.Children.Add(textblock);
            textblock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));


            _elementPanel.Width = textblock.DesiredSize.Width;
            _elementPanel.Height = textblock.DesiredSize.Height;

            double from;
            double to;
            PropertyPath propertyPath;

            if (Orientation == Orientation.Horizontal)
            {
                double actualWidth = ActualWidth;
                if (AutoRun && _elementPanel.Width < actualWidth)
                {
                    return;
                }
                if (Runaway)
                {

                    from = -_elementPanel.Width;
                    to = actualWidth;
                }
                else
                {
                    to = 0;
                    from = actualWidth - _elementPanel.Width;
                    SetCurrentValue(AutoReverseProperty, true);
                }
                propertyPath = new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)");
            }
            else
            {
                double actualHeigh = ActualHeight;
                if (AutoRun && _elementPanel.Height < actualHeigh)
                {
                    return;
                }

                if (Runaway)
                {
                    from = -_elementPanel.Height;
                    to = actualHeigh;
                }
                else
                {
                    from = 0;
                    to = actualHeigh - _elementPanel.Height;
                    SetCurrentValue(AutoReverseProperty, true);
                }
                propertyPath = new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)");
            }


            if (IsForward)
            {
                double temp;
                temp = from;
                from = to;
                to = temp;
            }

            var duration = double.IsNaN(Speed)
                ? Duration
                : !MathHelper.IsVerySmall(Speed)
                    ? TimeSpan.FromSeconds(Math.Abs(to - from) / Speed)
                    : Duration;

            var animation = new DoubleAnimation(from, to, duration)
            {
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = AutoReverse
            };

            Storyboard.SetTargetProperty(animation, propertyPath);
            Storyboard.SetTarget(animation, _elementPanel);

            _storyboard = new Storyboard();
            _storyboard.Children.Add(animation);
            _storyboard.Begin();

            if (_isFirstUpdata) return;
            var offset = from - offsetx;
            var proportion = offset / Math.Abs(to - from);
            var seekduration = proportion > 1 ? 0d : proportion * duration.TimeSpan.TotalSeconds;
            _storyboard.Seek(TimeSpan.FromSeconds(seekduration));

        }

    }
}
