﻿using Hjmos.BaseControls.Tools.Converter;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace Hjmos.BaseControls.Controls
{
    public class RectangleAttach
    {
        public static readonly DependencyProperty CircularProperty = DependencyProperty.RegisterAttached(
            "Circular", typeof(bool), typeof(RectangleAttach), new PropertyMetadata(false, OnCircularChanged));

        private static void OnCircularChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Rectangle rectangle)
            {
                if ((bool)e.NewValue)
                {
                    var binding = new MultiBinding
                    {
                        Converter = new RectangleCircularConverter()
                    };
                    binding.Bindings.Add(new Binding(FrameworkElement.ActualWidthProperty.Name) { Source = rectangle });
                    binding.Bindings.Add(new Binding(FrameworkElement.ActualHeightProperty.Name) { Source = rectangle });
                    rectangle.SetBinding(Rectangle.RadiusXProperty, binding);
                    rectangle.SetBinding(Rectangle.RadiusYProperty, binding);
                }
                else
                {
                    BindingOperations.ClearBinding(rectangle, FrameworkElement.ActualWidthProperty);
                    BindingOperations.ClearBinding(rectangle, FrameworkElement.ActualHeightProperty);
                    BindingOperations.ClearBinding(rectangle, Rectangle.RadiusXProperty);
                    BindingOperations.ClearBinding(rectangle, Rectangle.RadiusYProperty);
                }
            }
        }

        public static void SetCircular(DependencyObject element, bool value)
            => element.SetValue(CircularProperty, value);

        public static bool GetCircular(DependencyObject element)
            => (bool)element.GetValue(CircularProperty);
    }
}
