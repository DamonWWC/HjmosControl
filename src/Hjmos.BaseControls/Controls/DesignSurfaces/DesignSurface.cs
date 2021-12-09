using System;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = "PART_MainContent", Type = typeof(ContentControl))]
    [TemplatePart(Name = "PART_ScrollViewer", Type = typeof(ZoomScrollViewer))]
    public class DesignSurface : ContentControl
    {
        #region Property

        private const string MainContentName = "PART_MainContent";
        private const string ScrollViewerName = "PART_ScrollViewer";

        /// <summary>
        /// 主内容区域
        /// </summary>
        public ContentControl MainContent
        {
            get => (ContentControl)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register("MainContent", typeof(ContentControl), typeof(DesignSurface));

        public ZoomScrollViewer ScrollViewer
        {
            get => (ZoomScrollViewer)GetValue(ScrollViewerProperty);
            set => SetValue(ScrollViewerProperty, value);
        }

        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ZoomScrollViewer), typeof(DesignSurface));

        #endregion Property

        // static DesignSurface() => DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignSurface), new FrameworkPropertyMetadata(typeof(DesignSurface)));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MainContent = GetTemplateChild(MainContentName) as ContentControl;
            ScrollViewer = GetTemplateChild(ScrollViewerName) as ZoomScrollViewer;
        }
    }
}