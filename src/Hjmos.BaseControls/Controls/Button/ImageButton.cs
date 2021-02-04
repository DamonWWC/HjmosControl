using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hjmos.BaseControls.Controls
{
    public class ImageButton : Button
    {
        public ImageButton()
        {
        }

        private void ShowImage()
        {
            SetValue(UriPropertyKey, new Uri(FileName, UriKind.RelativeOrAbsolute));
            SetValue(PreviewBrushPropertyKey, new ImageBrush(BitmapFrame.Create(Uri, BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None))
            {
                Stretch = Stretch.UniformToFill
            });
            SetCurrentValue(ToolTipProperty, FileName);
        }

        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(ImageButton), new PropertyMetadata(default(string), (o, args) =>
            {
                var ct1 = o as ImageButton;
                var value = args.NewValue;
                if (value == null)
                    return;
                ct1.ShowImage();
            }));

        public static readonly DependencyPropertyKey UriPropertyKey = DependencyProperty.RegisterReadOnly(
             "Uri", typeof(Uri), typeof(ImageButton), new PropertyMetadata(default(Uri)));

        public static readonly DependencyProperty UriProperty = UriPropertyKey.DependencyProperty;

        public Uri Uri
        {
            get => (Uri)GetValue(UriProperty);
            set => SetValue(UriProperty, value);
        }

        public static readonly DependencyPropertyKey PreviewBrushPropertyKey = DependencyProperty.RegisterReadOnly(
            "PreviewBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty PreviewBrushProperty = PreviewBrushPropertyKey.DependencyProperty;

        public Brush PreviewBrush
        {
            get => (Brush)GetValue(PreviewBrushProperty);
            set => SetValue(PreviewBrushProperty, value);
        }


        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
           "StrokeThickness", typeof(double), typeof(ImageButton), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
            "StrokeDashArray", typeof(DoubleCollection), typeof(ImageButton), new FrameworkPropertyMetadata(default(DoubleCollection), FrameworkPropertyMetadataOptions.AffectsRender));

        public DoubleCollection StrokeDashArray
        {
            get => (DoubleCollection)GetValue(StrokeDashArrayProperty);
            set => SetValue(StrokeDashArrayProperty, value);
        }
    }
}
