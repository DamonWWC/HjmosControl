﻿using Hjmos.BaseControls.Interactivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Hjmos.BaseControls.Controls
{
    /// <summary>
    ///     图片浏览器
    /// </summary>
    [TemplatePart(Name = ElementPanelTop, Type = typeof(Panel))]
    [TemplatePart(Name = ElementImageViewer, Type = typeof(ImageViewerPlus))]
    public class ImageBrowser : ExWindow
    {
        #region Constants

        private const string ElementPanelTop = "PART_PanelTop";

        private const string ElementImageViewer = "PART_ImageViewer";

        #endregion Constants

        #region Data

        private Panel _panelTop;

        private ImageViewerPlus _imageViewer;

        #endregion Data

        static ImageBrowser()
        {
            IsFullScreenProperty.AddOwner(typeof(ImageBrowser), new PropertyMetadata(false));
        }

        public ImageBrowser()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.Close, ButtonClose_OnClick));

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        /// <summary>
        ///     带一个图片Uri的构造函数
        /// </summary>
        /// <param name="uri"></param>
        public ImageBrowser(Uri uri) : this()
        {
            Loaded += (s, e) =>
            {
                try
                {
                    _imageViewer.ImageSource = BitmapFrame.Create(uri);
                    _imageViewer.ImgPath = uri.AbsolutePath;

                    if (File.Exists(_imageViewer.ImgPath))
                    {
                        var info = new FileInfo(_imageViewer.ImgPath);
                        _imageViewer.ImgSize = info.Length;
                    }
                }
                catch
                {
                    //MessageBox.Show(Properties.Langs.Lang.ErrorImgPath);
                }
            };
        }
        public static void Show(string path)
        {
            new ImageBrowser(path).Show();
        }
        /// <summary>
        ///     带一个图片路径的构造函数
        /// </summary>
        /// <param name="path"></param>
        public ImageBrowser(string path) : this(new Uri(path))
        {

        }

        public override void OnApplyTemplate()
        {
            if (_panelTop != null)
            {
                _panelTop.MouseLeftButtonDown -= PanelTopOnMouseLeftButtonDown;
            }

            if (_imageViewer != null)
            {
                _imageViewer.MouseLeftButtonDown -= ImageViewer_MouseLeftButtonDown;
            }

            base.OnApplyTemplate();

            _panelTop = GetTemplateChild(ElementPanelTop) as Panel;
            _imageViewer = GetTemplateChild(ElementImageViewer) as ImageViewerPlus;

            if (_panelTop != null)
            {
                _panelTop.MouseLeftButtonDown += PanelTopOnMouseLeftButtonDown;
            }

            if (_imageViewer != null)
            {
                _imageViewer.MouseLeftButtonDown += ImageViewer_MouseLeftButtonDown;
            }
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e) => Close();

        private void PanelTopOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ImageViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !(_imageViewer.ImageWidth > ActualWidth || _imageViewer.ImageHeight > ActualHeight))
            {
                DragMove();
            }
        }
    }
}
