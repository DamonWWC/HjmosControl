using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HjmosControl.Controls.Attach
{
    public class InfoElement:TitleElement
    {
        /// <summary>
        ///     是否必填
        /// </summary>
        public static readonly DependencyProperty NecessaryProperty = DependencyProperty.RegisterAttached(
            "Necessary", typeof(bool), typeof(InfoElement), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetNecessary(DependencyObject element, bool value) => element.SetValue(NecessaryProperty, value);

        public static bool GetNecessary(DependencyObject element) => (bool)element.GetValue(NecessaryProperty);
    }
}
