using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LiveCharts.Wpf
{
    /// <summary>
    /// 
    /// </summary>
   public class PartiallyRoundedRectangle:Shape
    {
        public int RadiusX
        {
            get { return (int)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadiusX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusXProperty;
            


        public int RadiusY
        {
            get { return (int)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadiusY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusYProperty;



        public bool RoundTopLeft
        {
            get { return (bool)GetValue(RoundTopLeftProperty); }
            set { SetValue(RoundTopLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoundTopLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoundTopLeftProperty;



        public bool RoundTopRight
        {
            get { return (bool)GetValue(RoundTopRightProperty); }
            set { SetValue(RoundTopRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoundTopLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoundTopRightProperty;

        public bool RoundBottomRight
        {
            get { return (bool)GetValue(RoundBottomRightProperty); }
            set { SetValue(RoundBottomRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoundTopLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoundBottomRightProperty;


        public bool RoundBottomLeft
        {
            get { return (bool)GetValue(RoundBottomLeftProperty); }
            set { SetValue(RoundBottomLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoundTopLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoundBottomLeftProperty;

        static PartiallyRoundedRectangle()
        {
            RadiusXProperty = DependencyProperty.Register
                ("RadiusX", typeof(int), typeof(PartiallyRoundedRectangle));
            RadiusYProperty = DependencyProperty.Register
                ("RadiusY", typeof(int), typeof(PartiallyRoundedRectangle));

            RoundTopLeftProperty = DependencyProperty.Register
                ("RoundTopLeft", typeof(bool), typeof(PartiallyRoundedRectangle));
            RoundTopRightProperty = DependencyProperty.Register
                ("RoundTopRight", typeof(bool), typeof(PartiallyRoundedRectangle));
            RoundBottomLeftProperty = DependencyProperty.Register
                ("RoundBottomLeft", typeof(bool), typeof(PartiallyRoundedRectangle));
            RoundBottomRightProperty = DependencyProperty.Register
                ("RoundBottomRight", typeof(bool), typeof(PartiallyRoundedRectangle));
        }

        public PartiallyRoundedRectangle()
        {
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                Geometry result = new RectangleGeometry
                (new Rect(0, 0, base.Width, base.Height), RadiusX, RadiusY);
                double halfWidth = base.Width / 2;
                double halfHeight = base.Height / 2;

                if (!RoundTopLeft)
                    result = new CombinedGeometry
                (GeometryCombineMode.Union, result, new RectangleGeometry
                (new Rect(0, 0, halfWidth, halfHeight)));
                if (!RoundTopRight)
                    result = new CombinedGeometry
                (GeometryCombineMode.Union, result, new RectangleGeometry
                (new Rect(halfWidth, 0, halfWidth, halfHeight)));
                if (!RoundBottomLeft)
                    result = new CombinedGeometry
                (GeometryCombineMode.Union, result, new RectangleGeometry
                (new Rect(0, halfHeight, halfWidth, halfHeight)));
                if (!RoundBottomRight)
                    result = new CombinedGeometry
                (GeometryCombineMode.Union, result, new RectangleGeometry
                (new Rect(halfWidth, halfHeight, halfWidth, halfHeight)));

                return result;
            }
        }
    }
}
