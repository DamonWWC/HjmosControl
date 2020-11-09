using SharpVectors.Converters;
using System;
using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class SvgBox : SvgViewbox
    {

        public Uri ImageSource
        {
            get { return (Uri)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(Uri), typeof(SvgBox), new PropertyMetadata(default(Uri),
                (o,args)=>
                {
                    try
                    {
                        var ct1 = (SvgBox)o;
                        var v = (Uri)args.NewValue;
                        if (v != null)
                        {
                            ct1.Source = v;
                        }
                    }
                    catch
                    {

                    }
                    
                }));
    }
}
