using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class RadarChart : Control, IRadarChart
    {
        public RadarChart()
        {
            Loaded += RadarChart_Loaded;
        }

        private void RadarChart_Loaded(object sender, RoutedEventArgs e)
        {
            InvalidateVisual();
        }

        /// <summary>
        /// Series
        /// </summary>
        public IList<RadarSeries> RadarSeries
        {
            get { return (IList<RadarSeries>)GetValue(RadarSeriesProperty); }
            set { SetValue(RadarSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadarSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadarSeriesProperty =
            DependencyProperty.Register("RadarSeries", typeof(IList<RadarSeries>), typeof(RadarChart), new PropertyMetadata(default, (d, e) =>
            {
                RadarChart radarChart = d as RadarChart;
                if (e.NewValue != null)
                {
                    if (e.NewValue is IList<RadarSeries> radarseries)
                    {
                        foreach (var item in radarseries)
                        {
                            item.RadarChartModel = radarChart;
                        }
                    }
                }
            }));

        /// <summary>
        /// 标识信息
        /// </summary>
        public IList<Indicator> Indicator
        {
            get { return (IList<Indicator>)GetValue(IndicatorProperty); }
            set { SetValue(IndicatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Indicator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndicatorProperty =
            DependencyProperty.Register("Indicator", typeof(IList<Indicator>), typeof(RadarChart), new PropertyMetadata(default, (d, e) =>
            {
                RadarChart radarChart = d as RadarChart;
                if (e.NewValue != null)
                {
                }
            }));

        static RadarChart()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(RadarChart), new FrameworkPropertyMetadata(typeof(RadarChart)));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawPoints(drawingContext);
            DrawIndicator(drawingContext);
            DrawContent(drawingContext);
        }

        private void DrawContent(DrawingContext drawingContext)
        {
            if (RadarSeries == null || RadarSeries.Count == 0) return;
            var myPen = new Pen
            {
                Thickness = 4,
                Brush = Brushes.DodgerBlue
            };
            myPen.Freeze();

            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                var h = ActualHeight / 2;
                var w = ActualWidth / 2;
                PointCollection points = new PointCollection();

                foreach (var item in RadarSeries)
                {
                    for (int i = 0; i < item.Values.Count; i++)
                    {
                        var x = (Indicator[i].PointValue.X - w) / Indicator[i].Max * item.Values[i] + w;
                        var y = (Indicator[i].PointValue.Y - h) / Indicator[i].Max * item.Values[i] + h;
                        points.Add(new Point(x, y));
                    }
                }
                geometryContext.BeginFigure(points[points.Count - 1], true, true);
                geometryContext.PolyLineTo(points, true, true);
            }
            streamGeometry.Freeze();
            SolidColorBrush rectBrush = new SolidColorBrush(Colors.LightSkyBlue);
            rectBrush.Opacity = 0.5;
            drawingContext.DrawGeometry(rectBrush, myPen, streamGeometry);
        }

        private DrawingVisual drawingVisual;

        private void DrawPoints(DrawingContext drawingContext)
        {
            var myPen = new Pen
            {
                Thickness = 2,
                Brush = Brushes.Gainsboro
            };

            var h = this.ActualHeight / 2;
            var w = this.ActualWidth / 2;

            var r = Math.Min(ActualHeight, ActualWidth) / 2.0;

            for (int i = 0; i < 4; i++)
            {
                var rr = r * (i + 1) / 4;
                drawingContext.DrawEllipse(Brushes.Red, myPen, new Point(w, h), rr, rr);
            }

            drawingContext.DrawRectangle(Brushes.Yellow, new Pen(Brushes.SteelBlue, 3), new Rect(new Point(0, 0), new Size(50, 50)));
        }

        private void DrawIndicator(DrawingContext drawingContext)
        {
            if (Indicator != null)
            {
                double g = 0;
                double num = Indicator.Count;
                var myPen = new Pen
                {
                    Thickness = 2,
                    Brush = Brushes.Gainsboro
                };
                myPen.Freeze();
                if (num > 0)
                {
                    Point center = new Point(ActualWidth / 2, ActualHeight / 2);
                    double perangle = 360 / num;
                    double pi = Math.PI;
                    var r = Math.Min(ActualHeight, ActualWidth) / 2.0;

                    for (int i = 0; i < num; i++)
                    {
                        Point p2 = new Point(r * Math.Sin(g * pi / 180) + center.X, -r * Math.Cos(g * pi / 180) + center.Y);
                        var formattedText = GetFormattedText(Indicator[i].Name, flowDirection: FlowDirection.LeftToRight, textSize: 20.001D);

                        if (p2.Y > center.Y && p2.X < center.X)
                            drawingContext.DrawText(formattedText, new Point(p2.X - formattedText.Width - 5, p2.Y - formattedText.Height / 2));
                        else if (p2.Y < center.Y && p2.X > center.X)
                            drawingContext.DrawText(formattedText, new Point(p2.X, p2.Y - formattedText.Height));
                        else if (p2.Y < center.Y && p2.X < center.X)
                            drawingContext.DrawText(formattedText, new Point(p2.X - formattedText.Width - 5, p2.Y - formattedText.Height));
                        else if (p2.Y < center.Y && p2.X == center.X)
                            drawingContext.DrawText(formattedText, new Point(p2.X - formattedText.Width, p2.Y - formattedText.Height));
                        else
                            drawingContext.DrawText(formattedText, new Point(p2.X, p2.Y));

                        drawingContext.DrawLine(myPen, center, p2);
                        drawingContext.DrawEllipse(Brushes.Yellow, null, p2, 2, 2);
                        g += perangle;
                        Indicator[i].PointValue = p2;
                    }
                }
            }
        }

        /// <summary>
        /// 字体资源
        /// </summary>
        private FontFamily fontFamily = Application.Current.Resources["NormalFontFamily"] as FontFamily;

        /// <summary>
        /// 颜色转换
        /// </summary>
        private BrushConverter brushConverter = new BrushConverter();

        // [Obsolete]
        public FormattedText GetFormattedText(string text, string color = null, FlowDirection flowDirection = FlowDirection.RightToLeft, double textSize = 12.0D)
        {
            return new FormattedText(
                  text,
                  CultureInfo.CurrentCulture,
                  flowDirection,
                  new Typeface(new FontFamily("Helvetica Neue For Number"), FontStyles.Normal, FontWeights.Thin, FontStretches.Normal),
                  textSize, color == null ? Brushes.Black : (Brush)brushConverter.ConvertFromString(color), VisualTreeHelper.GetDpi(this).PixelsPerDip)
            {
                MaxLineCount = 1,
                TextAlignment = TextAlignment.Justify,
                Trimming = TextTrimming.CharacterEllipsis
            };
        }

        public void Updater()
        {
            this.InvalidateVisual();
        }
    }
}