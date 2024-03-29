﻿using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
   public class Empty: ContentControl
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
               "Description", typeof(object), typeof(Empty), new PropertyMetadata(default(object)));

        public object Description
        {
            get => GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register(
            "Logo", typeof(object), typeof(Empty), new PropertyMetadata(default(object)));

        public object Logo
        {
            get => GetValue(LogoProperty);
            set => SetValue(LogoProperty, value);
        }

        public static readonly DependencyProperty ShowEmptyProperty = DependencyProperty.RegisterAttached(
            "ShowEmpty", typeof(bool), typeof(Empty), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetShowEmpty(DependencyObject element, bool value)
            => element.SetValue(ShowEmptyProperty, value);

        public static bool GetShowEmpty(DependencyObject element)
            => (bool)element.GetValue(ShowEmptyProperty);
    }
}

