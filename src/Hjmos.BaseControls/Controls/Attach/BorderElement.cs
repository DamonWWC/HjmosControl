using Hjmos.BaseControls.Tools.Converter;
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



        public static readonly DependencyProperty GeometryProperty = DependencyProperty.RegisterAttached(
               "Geometry", typeof(Geometry), typeof(BorderElement), new PropertyMetadata(default(Geometry)));

        public static void SetGeometry(DependencyObject element, Geometry value)
            => element.SetValue(GeometryProperty, value);

        public static Geometry GetGeometry(DependencyObject element)
            => (Geometry)element.GetValue(GeometryProperty);

        public static readonly DependencyProperty CircularProperty = DependencyProperty.RegisterAttached(
           "Circular", typeof(bool), typeof(BorderElement), new PropertyMetadata(false, OnCircularChanged));

        private static void OnCircularChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Border border)
            {
                if ((bool)e.NewValue)
                {
                    var binding = new MultiBinding
                    {
                        Converter = new BorderCircularConverter()
                    };
                    binding.Bindings.Add(new Binding(FrameworkElement.ActualWidthProperty.Name) { Source = border });
                    binding.Bindings.Add(new Binding(FrameworkElement.ActualHeightProperty.Name) { Source = border });
                    border.SetBinding(Border.CornerRadiusProperty, binding);
                }
                else
                {
                    BindingOperations.ClearBinding(border, FrameworkElement.ActualWidthProperty);
                    BindingOperations.ClearBinding(border, FrameworkElement.ActualHeightProperty);
                    BindingOperations.ClearBinding(border, Border.CornerRadiusProperty);
                }
            }
        }

        public static void SetCircular(DependencyObject element, bool value)
            => element.SetValue(CircularProperty, value);

        public static bool GetCircular(DependencyObject element)
            => (bool)element.GetValue(CircularProperty);

    }
}
