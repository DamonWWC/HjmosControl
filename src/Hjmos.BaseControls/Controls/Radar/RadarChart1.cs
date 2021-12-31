using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = "PART_canvas", Type = typeof(Canvas))]
    public class RadarChart1 : Control
    {
        public RadarChart1()
        {
            this.SizeChanged += RadarChart1_SizeChanged;
        }

        /// <summary>
        /// 被选择的位置
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(RadarChart1), new PropertyMetadata(-1, (d, e) =>
             {
                 RadarChart1 radarChart = d as RadarChart1;
                 radarChart.SetTextForeground();
             }));

        public IList<RadarSeries> RadarSeries
        {
            get { return (IList<RadarSeries>)GetValue(RadarSeriesProperty); }
            set { SetValue(RadarSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadarSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadarSeriesProperty =
            DependencyProperty.Register("RadarSeries", typeof(IList<RadarSeries>), typeof(RadarChart1), new PropertyMetadata(default, (d, e) =>
             {
                 RadarChart1 radarChart = d as RadarChart1;
                 if (e.NewValue != null)
                 {
                     radarChart.DrawPoints();
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
            DependencyProperty.Register("Indicator", typeof(IList<Indicator>), typeof(RadarChart1), new PropertyMetadata(default, (d, e) =>
            {
                RadarChart1 radarChart = d as RadarChart1;
                if (e.NewValue != null)
                {
                    var values = e.NewValue as IList<Indicator>;
                    radarChart._textBlocks = new List<TextBlock>();
                    foreach (var item in values)
                    {
                        radarChart._textBlocks.Add(new TextBlock { Text = item.Name, FontSize = radarChart.FontSize });
                    }
                    radarChart.SetTextForeground();
                    radarChart.DrawIndicator();
                }
            }));

        private void SetTextForeground()
        {
            if (_textBlocks == null || _textBlocks.Count == 0) return;

            for (int i = 0; i < _textBlocks.Count; i++)
            {
                Brush solidColorBrush = Foreground;
                if (i == SelectedIndex)
                {
                    solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF13FFF5"));
                }
                _textBlocks[i].Foreground = solidColorBrush;
            }
        }

        private void RadarChart1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawIndicator();
            DrawPoints();
        }

        private void DrawPoints()
        {
            if (RadarSeries == null || Indicator == null) return;

            foreach (var item in _canvas.Children)
            {
                if (item is Polygon polygon1)
                {
                    _canvas.Children.Remove(polygon1);
                }
            }

            var h = ActualHeight / 2;
            var w = ActualWidth / 2;
            PointCollection points = new PointCollection();
            foreach (var item in RadarSeries)
            {
                if (item.Values == null) continue;
                var num = Math.Min(item.Values.Count, Indicator.Count);

                for (int i = 0; i < num; i++)
                {
                    var x = (Indicator[i].PointValue.X - w) / Indicator[i].Max * item.Values[i] + w;
                    var y = (Indicator[i].PointValue.Y - h) / Indicator[i].Max * item.Values[i] + h;
                    points.Add(new Point(x, y));
                }
                SolidColorBrush FillColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2613FFF5"));
                SolidColorBrush StrokeColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF13FFF5"));
                Polygon polygon = new Polygon
                {
                    Points = points,
                    Fill = FillColor,
                    Stroke = StrokeColor,
                    StrokeThickness = 1,
                };
                _canvas.Children.Add(polygon);
            }
        }

        private List<TextBlock> _textBlocks = new List<TextBlock>();

        private void DrawIndicator()
        {
            if (Indicator == null || Indicator.Count == 0) return;
            foreach (var item in _canvas.Children)
            {
                if (item is Line || item is Ellipse || item is TextBlock)
                {
                    _canvas.Children.Remove(item as UIElement);
                }
            }
            double g = 0;
            double num = Indicator.Count;

            if (num > 0)
            {
                Point center = new Point(ActualWidth / 2, ActualHeight / 2);
                double perangle = 360 / num;
                double pi = Math.PI;
                var r = Math.Min(ActualHeight, ActualWidth) / 2.0;

                for (int i = 0; i < num; i++)
                {
                    Point p2 = new Point(r * Math.Sin(g * pi / 180) + center.X, -r * Math.Cos(g * pi / 180) + center.Y);
                    TextBlock textBlock = _textBlocks[i];
                    textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    var w = textBlock.DesiredSize.Width;
                    var h = textBlock.DesiredSize.Height;
                    Point p = new Point();

                    if (p2.X >= center.X && p2.Y < center.Y)
                    {
                        p = p2.X == center.X ? new Point(p2.X - w / 2, p2.Y - h) : new Point(p2.X, p2.Y - h);
                    }
                    else if (p2.X > center.X && p2.Y >= center.Y)
                    {
                        p = p.Y == center.Y ? new Point(p2.X, p2.Y - h / 2) : p2;
                    }
                    else if (p2.X <= center.X && p2.Y > center.Y)
                    {
                        p = p2.X == center.X ? new Point(p2.X - w / 2, p2.Y) : new Point(p2.X - w, p2.Y);
                    }
                    else if (p2.X < center.X && p2.Y <= center.Y)
                    {
                        p = p2.Y == center.Y ? new Point(p2.X - w, p2.Y - h / 2) : new Point(p2.X - w, p2.Y - h);
                    }

                    Line line = new Line { X1 = center.X, X2 = p2.X, Y1 = center.Y, Y2 = p2.Y, Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1A4868")), StrokeThickness = 1 };
                    Ellipse ellipse = new Ellipse { Width = 3, Height = 3, Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0196A3")) };
                    Canvas.SetLeft(ellipse, p2.X - 1.5);
                    Canvas.SetTop(ellipse, p2.Y - 1.5);

                    _canvas.Children.Add(line);
                    _canvas.Children.Add(ellipse);
                    Canvas.SetLeft(textBlock, p.X);
                    Canvas.SetTop(textBlock, p.Y);
                    _canvas.Children.Add(textBlock);

                    g += perangle;
                    Indicator[i].PointValue = p2;
                }
            }
        }

        private Canvas _canvas;

        //private void CanvasChildrenRemove()
        //{
        //    _canvas.Children.RemoveRange(1, _canvas.Children.Count - 1);
        //}

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _canvas = GetTemplateChild("PART_canvas") as Canvas;
        }
    }
}