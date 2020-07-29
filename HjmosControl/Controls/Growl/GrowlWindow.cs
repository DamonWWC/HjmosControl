using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HjmosControl.Controls
{
    public class GrowlWindow : Window
    {
        internal Panel GrowlPanel { get; set; }

        internal GrowlWindow()
        {
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;

            GrowlPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 10, 10)
            };

            Content = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                //IsInertiaEnabled = true,
                Content = GrowlPanel
            };
        }

        internal void Init()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Height = desktopWorkingArea.Height;
            Left = desktopWorkingArea.Right - Width;
            Top = 0;
        }
    }
}
