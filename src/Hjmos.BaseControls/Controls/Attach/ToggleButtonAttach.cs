using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class ToggleButtonAttach
    {

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(ToggleButtonAttach), new PropertyMetadata(default(string)));

        public static string GetCheckText(DependencyObject obj)
        {
            return (string)obj.GetValue(CheckTextProperty);
        }

        public static void SetCheckText(DependencyObject obj, string value)
        {
            obj.SetValue(CheckTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckTextProperty =
            DependencyProperty.RegisterAttached("CheckText", typeof(string), typeof(ToggleButtonAttach), new PropertyMetadata(default(string)));



        public static string GetUnCheckText(DependencyObject obj)
        {
            return (string)obj.GetValue(UnCheckTextProperty);
        }

        public static void SetUnCheckText(DependencyObject obj, string value)
        {
            obj.SetValue(UnCheckTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for UnCheckText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnCheckTextProperty =
            DependencyProperty.RegisterAttached("UnCheckText", typeof(string), typeof(ToggleButtonAttach), new PropertyMetadata(default(string)));

       
    }
}
