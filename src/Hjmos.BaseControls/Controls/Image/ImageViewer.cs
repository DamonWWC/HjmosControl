using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = PART_Border, Type =typeof(Border))]
    public class ImageViewer : Control
    {

        private const string PART_Border = "PART_Border";
        public ImageViewer()
        {
                
        }

        Border border;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            border = GetTemplateChild(PART_Border) as Border;

            if(border!=null)
            {              
                SelectImageType();
            }

        }

        private void SelectImageType()
        {
            var index = ImageSource.LastIndexOf('.');
            var type = ImageSource.Substring(index + 1);

            try
            {
                if (type == "svg")
                {
                    SvgBox svgBox = new SvgBox()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = this.Width,
                        Height = this.Height,
                        ImageSource = new Uri(ImageSource)
                    };
                    border.Child = svgBox;
                }
                else
                {
                    Image image = new Image()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = this.Width,
                        Height = this.Height,
                        Stretch=Stretch.Fill,
                        Source = new BitmapImage(new Uri(ImageSource, UriKind.Relative))
                    };
                    border.Child = image;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(ImageViewer), new PropertyMetadata(default(string)));




       



    }
}
