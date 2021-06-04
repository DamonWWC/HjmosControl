﻿using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Hjmos.BaseControls.Controls
{
    [ContentProperty("Content")]
    public class Drawer : FrameworkElement
    {
        private Storyboard _storyboard;

        private AdornerContainer _container;

        private ContentControl _animationControl;

        private TranslateTransform _translateTransform;

        private double _animationLength;

        private string _animationPropertyName;

        private FrameworkElement _maskElement;

        private AdornerLayer _layer;

        private System.Windows.Window _window;

        private UIElement _windowContentElement;

        private Point _contentRenderTransformOrigin;

        public Drawer()
        {
            Loaded += Drawer_Loaded;
            Unloaded += Drawer_Unloaded;

            CommandBindings.Add(new CommandBinding(ControlCommands.Close, (s, e) => SetCurrentValue(IsOpenProperty, false)));
        }

        private void Drawer_Unloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Drawer_Loaded;
            if (_maskElement != null)
            {
                _maskElement.PreviewMouseLeftButtonDown -= _maskElement_PreviewMouseLeftButtonDown;
            }
            if(_storyboard!=null)
            {
                _storyboard.Completed -= _storyboard_Completed;
            }


        }

        private void Drawer_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsOpen)
            {
                OnIsOpenChanged(true);
            }
        }


        public static readonly RoutedEvent OpenedEvent =
            EventManager.RegisterRoutedEvent("Opened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Drawer));


        public event RoutedEventHandler Opened
        {
            add => AddHandler(OpenedEvent, value);
            remove => RemoveHandler(OpenedEvent, value);
        }

        public static readonly RoutedEvent ClosedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(Drawer));

        public event RoutedEventHandler Closed
        {
            add => AddHandler(ClosedEvent, value);
            remove => RemoveHandler(ClosedEvent, value);
        }


   
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Drawer), new FrameworkPropertyMetadata(false,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,OnIsOpenChanged));
        private static void OnIsOpenChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var ctl = (Drawer)d;
            ctl.OnIsOpenChanged((bool)e.NewValue);
        }


        public bool MaskCanClose
        {
            get { return (bool)GetValue(MaskCanCloseProperty); }
            set { SetValue(MaskCanCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaskCanClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaskCanCloseProperty =
            DependencyProperty.Register("MaskCanClose", typeof(bool), typeof(Drawer), new PropertyMetadata(true));



        public bool ShowMask
        {
            get { return (bool)GetValue(ShowMaskProperty); }
            set { SetValue(ShowMaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowMask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowMaskProperty =
            DependencyProperty.Register("ShowMask", typeof(bool), typeof(Drawer), new PropertyMetadata(true));



        public Dock Dock
        {
            get { return (Dock)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DockProperty =
            DependencyProperty.Register("Dock", typeof(Dock), typeof(Drawer), new PropertyMetadata(default(Dock)));



        public DrawerShowMode ShowMode
        {
            get { return (DrawerShowMode)GetValue(ShowModeProperty); }
            set { SetValue(ShowModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowModeProperty =
            DependencyProperty.Register("ShowMode", typeof(DrawerShowMode), typeof(Drawer), new PropertyMetadata(default(DrawerShowMode)));



        public Brush MaskBrush
        {
            get { return (Brush)GetValue(MaskBrushProperty); }
            set { SetValue(MaskBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaskBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaskBrushProperty =
            DependencyProperty.Register("MaskBrush", typeof(Brush), typeof(Drawer), new PropertyMetadata(default(Brush)));




        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(Drawer), new PropertyMetadata(default(Object)));

        private void CreateContainer()
        {
            _storyboard = new Storyboard();
            _storyboard.Completed += _storyboard_Completed;

            _translateTransform = new TranslateTransform();
            _animationControl = new ContentControl
            {
                Content = Content,
                RenderTransform = _translateTransform,
                DataContext = this
            };
            var panel = new SimplePanel
            {
                ClipToBounds = true
            };

            if(ShowMask)
            {
                _maskElement = new Border
                {
                    Background = MaskBrush,
                    Opacity = 0
                };
                _maskElement.PreviewMouseLeftButtonDown += _maskElement_PreviewMouseLeftButtonDown;
                panel.Children.Add(_maskElement);
            }

            _animationControl.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var size = _animationControl.DesiredSize;

            switch (Dock)
            {
                case Dock.Left:
                    _animationControl.HorizontalAlignment = HorizontalAlignment.Left;
                    _animationControl.VerticalAlignment = VerticalAlignment.Stretch;
                    _translateTransform.X = -size.Width;
                    _animationLength = -size.Width;
                    _animationPropertyName = "(UIElement.RenderTransform).(TranslateTransform.X)";
                    break;
                case Dock.Top:
                    _animationControl.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _animationControl.VerticalAlignment = VerticalAlignment.Top;
                    _translateTransform.Y = -size.Height;
                    _animationLength = -size.Height;
                    _animationPropertyName = "(UIElement.RenderTransform).(TranslateTransform.Y)";
                    break;
                case Dock.Right:
                    _animationControl.HorizontalAlignment = HorizontalAlignment.Right;
                    _animationControl.VerticalAlignment = VerticalAlignment.Stretch;
                    _translateTransform.X = size.Width;
                    _animationLength = size.Width;
                    _animationPropertyName = "(UIElement.RenderTransform).(TranslateTransform.X)";
                    break;
                case Dock.Bottom:
                    _animationControl.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _animationControl.VerticalAlignment = VerticalAlignment.Bottom;
                    _translateTransform.Y = size.Height;
                    _animationLength = size.Height;
                    _animationPropertyName = "(UIElement.RenderTransform).(TranslateTransform.Y)";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            panel.Children.Add(_animationControl);
            _container = new AdornerContainer(_layer)
            {
                Child = panel,
                ClipToBounds = true,
            };

        }

        private void _maskElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           if(MaskCanClose)
            {
                SetCurrentValue(IsOpenProperty, false);
            }
        }

        private void _storyboard_Completed(object sender, EventArgs e)
        {
            if(!IsOpen)
            {
                _windowContentElement.SetCurrentValue(RenderTransformOriginProperty, _contentRenderTransformOrigin);
                _layer.Remove(_container);
                RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
            }
            else
            {
                RaiseEvent(new RoutedEventArgs(OpenedEvent, this));
            }
        }

        private void OnIsOpenChanged(bool isOpen)
        {
            if (Content == null) return;

            _window = WindowHelper.GetActiveWindow();
            if (_window == null) return;

            _windowContentElement = _window.Content as UIElement;
            if (_windowContentElement == null) return;
            _contentRenderTransformOrigin = _windowContentElement.RenderTransformOrigin;

            var decorator = VisualHelper.GetChild<AdornerDecorator>(_window);
            _layer = decorator?.AdornerLayer;
            if (_layer == null) return;

            if(_container==null)
            {
                CreateContainer();
            }

            switch (ShowMode)
            {
                case DrawerShowMode.Push:
                    ShowByPush(isOpen);
                    break;
                case DrawerShowMode.Press:
                    _windowContentElement.SetCurrentValue(RenderTransformOriginProperty, new Point(0.5, 0.5));
                    ShowByPress(isOpen);
                    break;
            }

            if(isOpen)
            {
                if(_maskElement!=null)
                {
                    var maskAnimation = AnimationHelper.CreateAnimation(1);
                    Storyboard.SetTarget(maskAnimation, _maskElement);
                    Storyboard.SetTargetProperty(maskAnimation, new PropertyPath(OpacityProperty.Name));
                    _storyboard.Children.Add(maskAnimation);
                }

                var drawerAnimation = AnimationHelper.CreateAnimation(0);
                Storyboard.SetTarget(drawerAnimation, _animationControl);
                Storyboard.SetTargetProperty(drawerAnimation, new PropertyPath(_animationPropertyName));
                _storyboard.Children.Add(drawerAnimation);
                _layer.Remove(_container);
                _layer.Add(_container);
            }
            else
            {
                if (_maskElement != null)
                {
                    var maskAnimation = AnimationHelper.CreateAnimation(0);
                    Storyboard.SetTarget(maskAnimation, _maskElement);
                    Storyboard.SetTargetProperty(maskAnimation, new PropertyPath(OpacityProperty.Name));
                    _storyboard.Children.Add(maskAnimation);
                }

                var drawerAnimation = AnimationHelper.CreateAnimation(_animationLength);
                Storyboard.SetTarget(drawerAnimation, _animationControl);
                Storyboard.SetTargetProperty(drawerAnimation, new PropertyPath(_animationPropertyName));
                _storyboard.Children.Add(drawerAnimation);
            }
            _storyboard.Begin();
        }

        private void ShowByPush(bool isOpen)
        {
            string animationPropertyName;
            switch (Dock)
            {
                case Dock.Left:
                case Dock.Right:
                    animationPropertyName = "(UIElement.RenderTransform).(TranslateTransform.X)";
                    _windowContentElement.RenderTransform = new TranslateTransform
                    {
                        X = isOpen ? 0 : -_animationLength
                    };
                    break;
                case Dock.Top:                                  
                case Dock.Bottom:
                    animationPropertyName = "(UIElement.RenderTransform).(TranslateTransform.Y)";
                    _windowContentElement.RenderTransform = new TranslateTransform
                    {
                        Y = isOpen ? 0 : -_animationLength
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var animation = isOpen ? AnimationHelper.CreateAnimation(-_animationLength)
                : AnimationHelper.CreateAnimation(0);
            Storyboard.SetTarget(animation, _windowContentElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath(animationPropertyName));

            _storyboard.Children.Add(animation);
        }

        private void ShowByPress(bool isOpen)
        {
            _windowContentElement.RenderTransform = IsOpen
                ? new ScaleTransform()
                : new ScaleTransform
                {
                    ScaleX = 0.9,
                    ScaleY = 0.9
                };
            var animationX = IsOpen
                ? AnimationHelper.CreateAnimation(.9)
                : AnimationHelper.CreateAnimation(1);
            Storyboard.SetTarget(animationX, _windowContentElement);
            Storyboard.SetTargetProperty(animationX, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            _storyboard.Children.Add(animationX);

            var animationY = IsOpen
                ? AnimationHelper.CreateAnimation(.9)
                : AnimationHelper.CreateAnimation(1);
            Storyboard.SetTarget(animationY, _windowContentElement);
            Storyboard.SetTargetProperty(animationY, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            _storyboard.Children.Add(animationY);
        }
    }
}
