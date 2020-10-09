//The MIT License(MIT)

//Copyright(c) 2016 Alberto Rodriguez & LiveCharts Contributors

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using LiveCharts.Definitions.Points;
using LiveCharts.Definitions.Series;
using LiveCharts.SeriesAlgorithms;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf.Points;

namespace LiveCharts.Wpf
{
    /// <summary>
    /// The pie series should be added only in a pie chart.
    /// </summary>
    public class PieSeries : Series, IPieSeriesView
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of PieSeries class
        /// </summary>
        public PieSeries()
        {
            Model = new PieAlgorithm(this);
            InitializeDefuaults();
        }

        /// <summary>
        /// Initializes a new instance of PieSeries class with a given mapper.
        /// </summary>
        /// <param name="configuration"></param>
        public PieSeries(object configuration)
        {
            Model = new PieAlgorithm(this);
            Configuration = configuration;
            InitializeDefuaults();
        }

        #endregion

        #region Private Properties

        #endregion

        #region Properties

        /// <summary>
        /// The push out property
        /// </summary>
        public static readonly DependencyProperty PushOutProperty = DependencyProperty.Register(
            "PushOut", typeof (double), typeof (PieSeries), new PropertyMetadata(default(double), CallChartUpdater()));
        /// <summary>
        /// Gets or sets the slice push out, this property highlights the slice
        /// </summary>
        public double PushOut
        {
            get { return (double) GetValue(PushOutProperty); }
            set { SetValue(PushOutProperty, value); }
        }

        /// <summary>
        /// The label position property
        /// </summary>
        public static readonly DependencyProperty LabelPositionProperty = DependencyProperty.Register(
            "LabelPosition", typeof(PieLabelPosition), typeof(PieSeries), 
            new PropertyMetadata(PieLabelPosition.InsideSlice, CallChartUpdater()));
        /// <summary>
        /// Gets or sets the label position.
        /// </summary>
        /// <value>
        /// The label position.
        /// </value>
        public PieLabelPosition LabelPosition
        {
            get { return (PieLabelPosition) GetValue(LabelPositionProperty); }
            set { SetValue(LabelPositionProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TextPieNum
        {
            get { return (string)GetValue(TextPieNumProperty); }
            set { SetValue(TextPieNumProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for TextPie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPieNumProperty =
            DependencyProperty.Register("TextPieNum", typeof(string), typeof(PieSeries), new PropertyMetadata(default(string), OnValuesInstanceChanged));
        /// <summary>
        /// 
        /// </summary>
        public string TextPieDescribe
        {
            get { return (string)GetValue(TextPieDescribeProperty); }
            set { SetValue(TextPieDescribeProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for TextPie.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPieDescribeProperty =
            DependencyProperty.Register("TextPieDescribe", typeof(string), typeof(PieSeries), new PropertyMetadata(default(string), OnValuesInstanceChanged));


        /// <summary>
        /// 统计数字字体大小
        /// </summary>
        public double TextPieNumSize
        {
            get { return (double)GetValue(TextPieNumSizeProperty); }
            set { SetValue(TextPieNumSizeProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for TextPieNumSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPieNumSizeProperty =
            DependencyProperty.Register("TextPieNumSize", typeof(double), typeof(PieSeries), new PropertyMetadata(20d, OnValuesInstanceChanged));


        /// <summary>
        /// 统计描述字体大小
        /// </summary>
        public double TextPieDescribeSize
        {
            get { return (double)GetValue(TextPieDescribeSizeProperty); }
            set { SetValue(TextPieDescribeSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextPieDescribeSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPieDescribeSizeProperty =
            DependencyProperty.Register("TextPieDescribeSize", typeof(double), typeof(PieSeries), new PropertyMetadata(14d, OnValuesInstanceChanged));


        /// <summary>
        /// 统计数字前景色
        /// </summary>
        public Brush TextPieNumForground
        {
            get { return (Brush)GetValue(TextPieNumForgroundProperty); }
            set { SetValue(TextPieNumForgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextPieNumForground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPieNumForgroundProperty =
            DependencyProperty.Register("TextPieNumForground", typeof(Brush), typeof(PieSeries), new PropertyMetadata(Brushes.White, OnValuesInstanceChanged));


        /// <summary>
        /// 统计描述前景色
        /// </summary>
        public Brush TextPieDescribeForground
        {
            get { return (Brush)GetValue(TextPieDescribeForgroundProperty); }
            set { SetValue(TextPieDescribeForgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextPieDescribeForground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPieDescribeForgroundProperty =
            DependencyProperty.Register("TextPieDescribeForground", typeof(Brush), typeof(PieSeries), new PropertyMetadata(Brushes.White, OnValuesInstanceChanged));


        #endregion

        #region Overridden Methods

        /// <summary>
        /// Gets the view of a given point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public override IChartPointView GetPointView(ChartPoint point, string label)
        {
            var pbv = (PiePointView) point.View;

            if (pbv == null)
            {
                pbv = new PiePointView
                {
                    IsNew = true,
                    Slice = new PieSlice(),
                    TextPie = new TextBlock()
                    {
                        TextAlignment = TextAlignment.Center,                       
                    }
                };
                
                Model.Chart.View.AddToDrawMargin(pbv.Slice);
                if (TextPieNum != null)
                {
                    Model.Chart.View.AddToDrawMargin(pbv.TextPie);
                }

            }
            else
            {
                pbv.IsNew = false;
                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.Slice);
                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.HoverShape);
                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.DataLabel);
                point.SeriesView.Model.Chart.View
                   .EnsureElementBelongsToCurrentDrawMargin(pbv.TextPie);
            }

            pbv.Slice.Fill = Fill;
            pbv.Slice.Stroke = Stroke;
            pbv.Slice.StrokeThickness = StrokeThickness;
            pbv.Slice.StrokeDashArray = StrokeDashArray;
            pbv.Slice.PushOut = PushOut;
            pbv.Slice.Visibility = Visibility;
            Panel.SetZIndex(pbv.Slice, Panel.GetZIndex(this));

            if (!string.IsNullOrEmpty(TextPieNum))
            {
                //var size = ((PieChart)Model.Chart.View).InnerRadius *0.44;
                //pbv.TextPie.Inlines.Clear();

                //double sizechanged = TextPieNum.Length > 7 ? 7 * size / TextPieNum.Length : size ;
                //Run r1 = new Run()
                //{
                //    Foreground = Brushes.White,
                //    FontWeight = FontWeights.Bold,
                //    FontSize = sizechanged,
                //    //Text = string.Format("{0:N0}" ,Convert.ToDouble(TextPieNum))
                //    Text = TextPieNum
                //};
                //pbv.TextPie.Inlines.Add(r1);
                //if(TextPieDescribe!=null)
                //{
                //    Run r2 = new Run()
                //    {                        
                //        Foreground = new SolidColorBrush(Color.FromArgb(255, 155, 156, 158)),
                //        FontSize = size*0.6,
                //        Text = "\n"+TextPieDescribe
                //    };

                //    pbv.TextPie.Inlines.Add(r2);
                //}

                pbv.TextPie.Inlines.Clear();

                Run r1 = new Run()
                {
                    Foreground = TextPieNumForground,
                    FontWeight = FontWeights.Bold,
                    FontSize = TextPieNumSize,
                    Text = TextPieNum
                };
                pbv.TextPie.Inlines.Add(r1);
                if (TextPieDescribe != null)
                {
                    Run r2 = new Run()
                    {
                        Foreground = TextPieDescribeForground,
                        FontSize = TextPieDescribeSize,
                        Text = "\n" + TextPieDescribe
                    };

                    pbv.TextPie.Inlines.Add(r2);
                }
            }
           
            if (Model.Chart.RequiresHoverShape && pbv.HoverShape == null)
            {
                pbv.HoverShape = new PieSlice
                {
                    Fill = Brushes.Transparent,
                    StrokeThickness = 0
                };

                Panel.SetZIndex(pbv.HoverShape, int.MaxValue);

                var wpfChart = (Chart)Model.Chart.View;
                wpfChart.AttachHoverableEventTo(pbv.HoverShape);

                Model.Chart.View.AddToDrawMargin(pbv.HoverShape);
            }

            if (pbv.HoverShape != null) pbv.HoverShape.Visibility = Visibility;

            if (DataLabels)
            {
                pbv.DataLabel = UpdateLabelContent(new DataLabelViewModel
                {
                    FormattedText = label,
                    Point = point
                }, pbv.DataLabel);
            }

            if (!DataLabels && pbv.DataLabel != null)
            {
                Model.Chart.View.RemoveFromDrawMargin(pbv.DataLabel);
                pbv.DataLabel = null;
            }

          
            if (point.Stroke != null) pbv.Slice.Stroke = (Brush)point.Stroke;
            if (point.Fill != null) pbv.Slice.Fill = (Brush)point.Fill;

            pbv.OriginalPushOut  = PushOut;

            return pbv;
        }

        #endregion

        #region Private Methods

        private void InitializeDefuaults()
        {
            SetCurrentValue(StrokeThicknessProperty, 2d);
            SetCurrentValue(StrokeProperty, Brushes.White);
            SetCurrentValue(ForegroundProperty, Brushes.White);

            Func<ChartPoint, string> defaultLabel = x => Model.CurrentYAxis.GetFormatter()(x.Y);

            SetCurrentValue(LabelPointProperty, defaultLabel);

            DefaultFillOpacity = 1;
        }

        #endregion
    }
}
