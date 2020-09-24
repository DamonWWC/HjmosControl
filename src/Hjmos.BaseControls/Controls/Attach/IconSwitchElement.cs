using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class IconSwitchElement : IconElement
    {

        public static readonly DependencyProperty GeometrySelectedProperty = DependencyProperty.RegisterAttached(
             "GeometrySelected", typeof(Geometry), typeof(IconSwitchElement), new PropertyMetadata(default(Geometry)));

        public static void SetGeometrySelected(DependencyObject element, Geometry value)
        {
            element.SetValue(GeometrySelectedProperty, value);
        }

        public static Geometry GetGeometrySelected(DependencyObject element)
        {
            return (Geometry)element.GetValue(GeometrySelectedProperty);
        }

    }
}
