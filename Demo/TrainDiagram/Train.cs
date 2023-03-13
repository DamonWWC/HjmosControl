using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace TrainDiagram
{
    public class Train: Decorator
    {
        Canvas canvas = new Canvas();
        public Train()
        {
            this.Child = canvas;
           
        }
        string start = "06:00";
        string end = "23:00";
        DateTime starttime, endtime;
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Rect bounds = new Rect(0, 0, base.ActualWidth, base.ActualHeight);
            Pen pen = new Pen(Brushes.Black, 2);
            drawingContext.DrawRectangle(Brushes.Transparent, pen, bounds);
            Pen pen1 = new Pen(Brushes.Black, 1);
            var perheight = base.ActualHeight / 10.0;
            Typeface typeface = new Typeface(new FontFamily("Segoe UI"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            for (int i=0;i<10;i++)
            {
                Point point1 = new Point(0, (i + 1) * perheight);
                Point point2 = new Point(ActualWidth, (i + 1) * perheight);
                drawingContext.DrawLine(pen1, point1, point2);            
                FormattedText formattedText = new FormattedText("朝阳村", CultureInfo.CurrentUICulture, FlowDirection.RightToLeft, typeface, 14, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
                drawingContext.DrawText(formattedText, new Point(0, (i + 1) * perheight-formattedText.Height/2.0));
            }

            
            DateTime.TryParse(start, out starttime);
            DateTime.TryParse(end, out endtime);
            var pertime = endtime.Hour - starttime.Hour;
            
            var perwidth = base.ActualWidth / pertime;

            Pen pen2 = new Pen(Brushes.Red, 0.5);
            for (int i=1;i<pertime; i++)
            {
                var start1 = (i - 1) * perwidth;
                for (int j=1;j<6;j++)
                {                
                    Point point3 = new Point((i-1) * perwidth+perwidth*j/6, 0);
                    Point point4 = new Point((i-1) * perwidth+perwidth*j/6, ActualHeight);
                    drawingContext.DrawLine(pen2, point3, point4);
                }
                Point point1 = new Point(i * perwidth, 0);
                Point point2 = new Point( i * perwidth, ActualHeight);
                drawingContext.DrawLine(pen1, point1, point2);              
                FormattedText formattedText = new FormattedText(starttime.AddHours(i).ToShortTimeString(), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, typeface, 14, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
                drawingContext.DrawText(formattedText, new Point((i) * perwidth - formattedText.Width / 2.0,-formattedText.Height));
            }
            DrawPath();
        }



        public List<List<TrainData>> ItemSource
        {
            get { return (List<List<TrainData>>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<List<TrainData>>), typeof(Train), new PropertyMetadata(default, (d,e)=>
            {
                var ct1 = (Train)d;
                ct1.DrawPath();
            }));

        private static void ItemSourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }






        private void DrawPath()
        {
            if (ItemSource == null) return;
            canvas.Children.Clear();
            var alltick = endtime.Ticks - starttime.Ticks;
            StringBuilder str = new StringBuilder();
            foreach (List<TrainData> item in ItemSource)
            {
               foreach(var item1 in item)
                {
                    var perx = base.ActualWidth / alltick;
                    var x = perx * (item1.Datetime.Ticks - starttime.Ticks);
                    var pery = base.ActualHeight / 10.0;
                    var y = pery * item1.Value;
                    if (str.Length == 0)
                    {
                        str.Append($"m{x},{y}");
                    }
                    else
                    {
                        str.Append($" L{x},{y}");
                    }
                }
              
            }

            Path path = new Path();
            //path.Data = Geometry.Parse("m0,0 L100,100 L125,100 L200,200");
            string ss = str.ToString();
            path.Data = Geometry.Parse(str.ToString());
            path.Stroke = Brushes.Blue;

            canvas.Children.Add(path);


        }


    }

    public class TrainData
    {
        public DateTime Datetime { get; set; }
        public int Value { get; set; }
    }
}
