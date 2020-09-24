using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class BorderElement
    {
        //public static CornerRadius GetCornerRadius(DependencyObject obj)
        //{
        //    return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        //}

        //public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        //{
        //    obj.SetValue(CornerRadiusProperty, value);
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CornerRadiusProperty =
        //    DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(BorderElement), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));


        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
           "CornerRadius", typeof(CornerRadius), typeof(BorderElement), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);

        public static CornerRadius GetCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(CornerRadiusProperty);



     



    }
}
