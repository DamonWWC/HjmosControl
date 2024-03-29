﻿using Hjmos.BaseControls.Data;
using System.Windows;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class TitleElement
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
            "Title", typeof(string), typeof(TitleElement), new PropertyMetadata(default(string)));

        public static void SetTitle(DependencyObject element, string value)
            => element.SetValue(TitleProperty, value);

        public static string GetTitle(DependencyObject element)
            => (string)element.GetValue(TitleProperty);

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
            "Background", typeof(Brush), typeof(TitleElement), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetBackground(DependencyObject element, Brush value)
            => element.SetValue(BackgroundProperty, value);

        public static Brush GetBackground(DependencyObject element)
            => (Brush)element.GetValue(BackgroundProperty);

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
            "Foreground", typeof(Brush), typeof(TitleElement), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetForeground(DependencyObject element, Brush value)
            => element.SetValue(ForegroundProperty, value);

        public static Brush GetForeground(DependencyObject element)
            => (Brush)element.GetValue(ForegroundProperty);

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached(
            "BorderBrush", typeof(Brush), typeof(TitleElement), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetBorderBrush(DependencyObject element, Brush value)
            => element.SetValue(BorderBrushProperty, value);

        public static Brush GetBorderBrush(DependencyObject element)
            => (Brush)element.GetValue(BorderBrushProperty);

        /// <summary>
        ///     标题对齐方式
        /// </summary>
        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.RegisterAttached(
            "TitleAlignment", typeof(TitleAlignment), typeof(TitleElement), new FrameworkPropertyMetadata(TitleAlignment.Top, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetTitleAlignment(DependencyObject element, TitleAlignment value)
            => element.SetValue(TitleAlignmentProperty, value);

        public static TitleAlignment GetTitleAlignment(DependencyObject element)
            => (TitleAlignment)element.GetValue(TitleAlignmentProperty);

        /// <summary>
        ///     标题宽度
        /// </summary>
        public static readonly DependencyProperty TitleWidthProperty = DependencyProperty.RegisterAttached(
            "TitleWidth", typeof(GridLength), typeof(TitleElement), new FrameworkPropertyMetadata(new GridLength(120.0), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetTitleWidth(DependencyObject element, GridLength value) => element.SetValue(TitleWidthProperty, value);


        public static GridLength GetTitleWidth(DependencyObject element) => (GridLength)element.GetValue(TitleWidthProperty);
        public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.RegisterAttached(
            "HorizontalAlignment", typeof(HorizontalAlignment), typeof(TitleElement), new FrameworkPropertyMetadata(default(HorizontalAlignment), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHorizontalAlignment(DependencyObject element, HorizontalAlignment value)
            => element.SetValue(HorizontalAlignmentProperty, value);

        public static HorizontalAlignment GetHorizontalAlignment(DependencyObject element)
            => (HorizontalAlignment)element.GetValue(HorizontalAlignmentProperty);

        public static readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.RegisterAttached(
            "VerticalAlignment", typeof(VerticalAlignment), typeof(TitleElement), new FrameworkPropertyMetadata(default(VerticalAlignment), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetVerticalAlignment(DependencyObject element, VerticalAlignment value)
            => element.SetValue(VerticalAlignmentProperty, value);

        public static VerticalAlignment GetVerticalAlignment(DependencyObject element)
            => (VerticalAlignment)element.GetValue(VerticalAlignmentProperty);
    }
}
