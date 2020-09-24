using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class Divider:Control
    {


        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(Divider), new PropertyMetadata(default(object)));



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Divider), new PropertyMetadata(default(Orientation)));



        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(Divider), new PropertyMetadata(default(DataTemplate)));



        public string ContentStringFormat
        {
            get { return (string)GetValue(ContentStringFormatProperty); }
            set { SetValue(ContentStringFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentStringFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentStringFormatProperty =
            DependencyProperty.Register("ContentStringFormat", typeof(string), typeof(Divider), new PropertyMetadata(default(string)));



        public DataTemplateSelector ContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentTemplateSelectorProperty =
            DependencyProperty.Register("ContentTemplateSelector", typeof(DataTemplateSelector), typeof(Divider), new PropertyMetadata(default(DataTemplateSelector)));





        public Brush LineStroke
        {
            get { return (Brush)GetValue(LineStrokeProperty); }
            set { SetValue(LineStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStrokeProperty =
            DependencyProperty.Register("LineStroke", typeof(Brush), typeof(Divider), new PropertyMetadata(default(Brush)));


        public double LineStrokeThickness
        {
            get { return (double)GetValue(LineStrokeThicknessProperty); }
            set { SetValue(LineStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStrokeThicknessProperty =
            DependencyProperty.Register("LineStrokeThickness", typeof(double), typeof(Divider), new PropertyMetadata(1d));


        public DoubleCollection LineStrokeDashArray
        {
            get { return (DoubleCollection)GetValue(LineStrokeDashArrayProperty); }
            set { SetValue(LineStrokeDashArrayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineStrokeDashArray.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStrokeDashArrayProperty =
            DependencyProperty.Register("LineStrokeDashArray", typeof(DoubleCollection), typeof(Divider), new PropertyMetadata(new DoubleCollection(0)));







    }
}
