using Hjmos.BaseControls.Interactivity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hjmos.BaseControls.Controls
{
    public class ImageSelector : Control
    {
        public ImageSelector() => CommandBindings.Add(new CommandBinding(ControlCommands.Switch, SwitchImage));

        private void SwitchImage(object sender, ExecutedRoutedEventArgs e)
        {
            ShowImage();
        }


        private void ShowImage(string fileName=null)
        {
            if (!HasValue)
            {
                if(fileName!=null)
                {
                    SetValue(UriPropertyKey, new Uri(fileName, UriKind.RelativeOrAbsolute));
                    SetValue(PreviewBrushPropertyKey, new ImageBrush(BitmapFrame.Create(Uri, BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None))
                    {
                        Stretch = Stretch
                    });
                    SetValue(HasValuePropertyKey, true);
                    SetCurrentValue(ToolTipProperty, fileName);
                    return;
                }
                var dialog = new OpenFileDialog
                {
                    RestoreDirectory = true,
                    Filter = Filter,
                    DefaultExt = DefaultExt
                };

                if (dialog.ShowDialog() == true)
                {
                    SetValue(UriPropertyKey, new Uri(dialog.FileName, UriKind.RelativeOrAbsolute));
                    SetValue(PreviewBrushPropertyKey, new ImageBrush(BitmapFrame.Create(Uri, BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None))
                    {
                        Stretch = Stretch
                    });
                    SetValue(HasValuePropertyKey, true);
                    SetCurrentValue(ToolTipProperty, dialog.FileName);
                }
            }
            else
            {
                FileName = null;
                SetValue(UriPropertyKey, default(Uri));
                SetValue(PreviewBrushPropertyKey, default(Brush));
                SetValue(HasValuePropertyKey, false);
                SetCurrentValue(ToolTipProperty, default);
                RaiseEvent(new RoutedEventArgs(DeleteEvent));
            }
        }


        public static readonly RoutedEvent DeleteEvent =
            EventManager.RegisterRoutedEvent("Delete", RoutingStrategy.Bubble,
                typeof(EventHandler), typeof(ImageSelector));
        /// <summary>
        /// 删除事件
        /// </summary>
        public event EventHandler Delete
        {
            add => AddHandler(DeleteEvent, value);
            remove => RemoveHandler(DeleteEvent, value);
        }


        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(ImageSelector), new PropertyMetadata(default(string), (o, args) =>
            {
                var ct1 = o as ImageSelector;
                var value = args.NewValue;
                if (value == null)
                    return;
                ct1.ShowImage(value.ToString());
            }));



        


        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            "Stretch", typeof(Stretch), typeof(ImageSelector), new PropertyMetadata(Stretch.UniformToFill));

        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }


    

        public static readonly DependencyPropertyKey UriPropertyKey = DependencyProperty.RegisterReadOnly(
            "Uri", typeof(Uri), typeof(ImageSelector), new PropertyMetadata(default(Uri)));

        public static readonly DependencyProperty UriProperty = UriPropertyKey.DependencyProperty;

        public Uri Uri
        {
            get => (Uri)GetValue(UriProperty);
            set => SetValue(UriProperty, value);
        }

        public static readonly DependencyPropertyKey PreviewBrushPropertyKey = DependencyProperty.RegisterReadOnly(
            "PreviewBrush", typeof(Brush), typeof(ImageSelector), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty PreviewBrushProperty = PreviewBrushPropertyKey.DependencyProperty;

        public Brush PreviewBrush
        {
            get => (Brush)GetValue(PreviewBrushProperty);
            set => SetValue(PreviewBrushProperty, value);
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof(double), typeof(ImageSelector), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
            "StrokeDashArray", typeof(DoubleCollection), typeof(ImageSelector), new FrameworkPropertyMetadata(default(DoubleCollection), FrameworkPropertyMetadataOptions.AffectsRender));

        public DoubleCollection StrokeDashArray
        {
            get => (DoubleCollection)GetValue(StrokeDashArrayProperty);
            set => SetValue(StrokeDashArrayProperty, value);
        }

        public static readonly DependencyProperty DefaultExtProperty = DependencyProperty.Register(
            "DefaultExt", typeof(string), typeof(ImageSelector), new PropertyMetadata(".jpg"));

        public string DefaultExt
        {
            get => (string)GetValue(DefaultExtProperty);
            set => SetValue(DefaultExtProperty, value);
        }

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(
            "Filter", typeof(string), typeof(ImageSelector), new PropertyMetadata("(.jpg)|*.jpg|(.png)|*.png"));

        public string Filter
        {
            get => (string)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public static readonly DependencyPropertyKey HasValuePropertyKey = DependencyProperty.RegisterReadOnly(
            "HasValue", typeof(bool), typeof(ImageSelector), new PropertyMetadata(false));

        public static readonly DependencyProperty HasValueProperty = HasValuePropertyKey.DependencyProperty;

        public bool HasValue
        {
            get => (bool)GetValue(HasValueProperty);
            set => SetValue(HasValueProperty, value);
        }
    }
}

