using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = "PART_ContentElement", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Panel", Type = typeof(Panel))]
    [TemplatePart(Name = "PART_Title",Type =typeof(FrameworkElement))]
    public class RunningBlock : ContentControl
    {    
        protected Storyboard _storyboard;

        private FrameworkElement _elementContent;
        private FrameworkElement _elementPanel;
        private FrameworkElement _elementTitle;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
     
            _elementContent = GetTemplateChild("PART_ContentElement") as FrameworkElement;
            _elementPanel = GetTemplateChild("PART_Panel") as Panel;
            _elementTitle = GetTemplateChild("PART_Title") as FrameworkElement;
        }


        public bool Runaway
        {
            get { return (bool)GetValue(RunawayProperty); }
            set { SetValue(RunawayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Runaway.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RunawayProperty =
            DependencyProperty.Register("Runaway", typeof(bool), typeof(RunningBlock), new FrameworkPropertyMetadata(true,FrameworkPropertyMetadataOptions.AffectsRender));



        public bool AutoRun
        {
            get { return (bool)GetValue(AutoRunProperty); }
            set { SetValue(AutoRunProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoRun.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoRunProperty =
            DependencyProperty.Register("AutoRun", typeof(bool), typeof(RunningBlock), new FrameworkPropertyMetadata(false,FrameworkPropertyMetadataOptions.AffectsRender));

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



        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRunning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register("IsRunning", typeof(bool), typeof(RunningBlock), new PropertyMetadata(true,(o,args)=> 
            {
                var ct1 = (RunningBlock)o;
                var v = (bool)args.NewValue;
                if(v)
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



        public Object Title
        {
            get { return (Object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(Object), typeof(RunningBlock), new PropertyMetadata(default(object)));

        [Bindable(true),Category("Content")]
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(RunningBlock), new PropertyMetadata(default(DataTemplate)));

        [Bindable(true), Category("Content")]
        public DataTemplateSelector TitleTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(TitleTemplateSelectorProperty); }
            set { SetValue(TitleTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTemplateSelectorProperty =
            DependencyProperty.Register("TitleTemplateSelector", typeof(DataTemplateSelector), typeof(RunningBlock), new PropertyMetadata(default(DataTemplateSelector)));


        [Bindable(true),Category("Content")]
        public string TitleStringFormat
        {
            get { return (string)GetValue(TitleStringFormatProperty); }
            set { SetValue(TitleStringFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleStringFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleStringFormatProperty =
            DependencyProperty.Register("TitleStringFormat", typeof(string), typeof(RunningBlock), new PropertyMetadata(default(string)));





        private void UpdateContent()
        {
           
            if (_elementContent == null || _elementPanel == null) return;
            _storyboard?.Stop();

            _elementPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            _elementPanel.Width = _elementPanel.DesiredSize.Width;
            _elementPanel.Height = _elementPanel.DesiredSize.Height;

            _elementTitle.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            _elementTitle.Width = _elementTitle.DesiredSize.Width;
            _elementTitle.Height = _elementTitle.DesiredSize.Height;

            double from;
            double to;
            PropertyPath propertyPath;

            if(Orientation==Orientation.Horizontal)
            {
                double actualWidth = ActualWidth - _elementTitle.Width;
                if (AutoRun&&_elementPanel.Width< actualWidth)
                {
                    return;
                }
                if(Runaway)
                {
                    from = -_elementPanel.Width;
                    to = actualWidth;
                }
                else
                {
                    from = 0;
                    to = actualWidth - _elementPanel.Width;
                    SetCurrentValue(AutoReverseProperty, true);
                }
                propertyPath = new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)");
            }
            else
            {
                double actualHeigh = ActualHeight - _elementTitle.Height;
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
            var animation = new DoubleAnimation(from, to, Duration)
            {
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = AutoReverse
            };
            Storyboard.SetTargetProperty(animation, propertyPath);
            Storyboard.SetTarget(animation, _elementContent);

            _storyboard = new Storyboard();
            _storyboard.Children.Add(animation);
            _storyboard.Begin();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            UpdateContent();
        }

    }
}
