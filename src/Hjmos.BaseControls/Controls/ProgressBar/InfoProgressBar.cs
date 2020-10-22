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
    public class InfoProgressBar : ProgressBar
    {



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(InfoProgressBar), new PropertyMetadata(default(string)));

        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(InfoProgressBar), new PropertyMetadata(Brushes.Black));




        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(InfoProgressBar), new PropertyMetadata(20d));



        public double ActualValue
        {
            get { return (double)GetValue(ActualValueProperty); }
            set { SetValue(ActualValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActualValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActualValueProperty =
            DependencyProperty.Register("ActualValue", typeof(double), typeof(InfoProgressBar), new PropertyMetadata(0d,
                (o,args)=>
                {
                    var v = (double)args.NewValue;
                    var ct1 = (InfoProgressBar)o;
                    ct1.Value = v;

                }));




        public double AWidth
        {
            get { return (double)GetValue(AWidthProperty); }
            set { SetValue(AWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AWidthProperty =
            DependencyProperty.Register("AWidth", typeof(double), typeof(InfoProgressBar), new PropertyMetadata(0d));





    }
}
