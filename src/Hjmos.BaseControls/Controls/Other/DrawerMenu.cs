﻿using Hjmos.BaseControls.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;
namespace Hjmos.BaseControls.Controls
{

    [TemplatePart(Name = "PART_Presenter", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_ToggleButton", Type = typeof(ToggleButton))]
    public class DrawerMenu : ContentControl
    {
        private const string PresentertName = "PART_Presenter";
        private const string ToggleButtonName = "PART_ToggleButton";
        internal ContentPresenter _presenter;
        internal ToggleButton _toggleButton;
        public DrawerMenu()
        {
            Loaded += DrawerMenu_Loaded;
        }

        private void DrawerMenu_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(DrawerMenu), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsOpenChanged));
        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (DrawerMenu)d;
            ctl.OnIsOpenChanged((bool)e.NewValue);
        }  
        private double _animationLength;
     
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _presenter = GetTemplateChild(PresentertName) as ContentPresenter;
            _toggleButton = GetTemplateChild(ToggleButtonName) as ToggleButton;
            _toggleButton.MouseEnter += _toggleButton_MouseEnter;
            _toggleButton.MouseLeave += _toggleButton_MouseLeave;
            _presenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var size = _presenter.DesiredSize;
            _animationLength = size.Width;
            if(IsOpen)
            {
                _presenter.Width = _animationLength;
            }
            else
            {
                _presenter.Width = 0;
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

        }
        protected override async void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            await Task.Delay(2000);
            IsOpen = false;

        }
        private void _toggleButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //IsOpen = false;
            
        }

        private void _toggleButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

            IsOpen = true;
        }

        private void OnIsOpenChanged(bool isOpen)
        {
            if (Content == null) return;
                    
            DoubleAnimation drawerAnimation;
            if (isOpen)
            {
                drawerAnimation = AnimationHelper.CreateAnimation(_animationLength);         
            }
            else
            {
                 drawerAnimation = AnimationHelper.CreateAnimation(0);         
            }

            _presenter.BeginAnimation(FrameworkElement.WidthProperty, drawerAnimation);
        }

        
      
    }
}
