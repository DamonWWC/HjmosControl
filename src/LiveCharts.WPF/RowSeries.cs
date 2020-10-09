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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LiveCharts.Definitions.Points;
using LiveCharts.Definitions.Series;
using LiveCharts.Dtos;
using LiveCharts.SeriesAlgorithms;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf.Points;

namespace LiveCharts.Wpf
{
    /// <summary>
    /// The Row series plots horizontal bars in a cartesian chart
    /// </summary>
    public class RowSeries : Series, IRowSeriesView
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of RowSeries class
        /// </summary>
        public RowSeries()
        {
            Model = new RowAlgorithm(this);
            InitializeDefuaults();
        }

        /// <summary>
        /// Initializes a new instance of RowSeries class with a given mapper
        /// </summary>
        /// <param name="configuration"></param>
        public RowSeries(object configuration)
        {
            Model = new RowAlgorithm(this);
            Configuration = configuration;
            InitializeDefuaults();
        }

        #endregion

        #region Private Properties

        #endregion

        #region Properties

        /// <summary>
        /// 修改
        /// </summary>
        public List<double> Percentage
        {
            get { return (List<double>)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }
        /// <summary>
        /// 修改
        /// </summary>
        // Using a DependencyProperty as the backing store for Percentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(List<double>), typeof(RowSeries), new PropertyMetadata(default(List<double>), OnValuesInstanceChanged));


        /// <summary>
        /// 修改 尾部比例宽度
        /// </summary>
        public double PercentageWith
        {
            get { return (double)GetValue(PercentageWithProperty); }
            set { SetValue(PercentageWithProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for PercentageWith.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentageWithProperty =
            DependencyProperty.Register("PercentageWith", typeof(double), typeof(RowSeries), new PropertyMetadata(0d, OnValuesInstanceChanged));


        /// <summary>
        /// 
        /// </summary>
        public double AddHeight
        {
            get { return (double)GetValue(AddHeightProperty); }
            set { SetValue(AddHeightProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for AddHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddHeightProperty =
            DependencyProperty.Register("AddHeight", typeof(double), typeof(RowSeries), new PropertyMetadata(0d, OnValuesInstanceChanged));


        /// <summary>
        /// 修改
        /// </summary>
        public bool ShowShadow
        {
            get { return (bool)GetValue(ShowShadowProperty); }
            set { SetValue(ShowShadowProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for ShowShadow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowShadowProperty =
            DependencyProperty.Register("ShowShadow", typeof(bool), typeof(RowSeries), new PropertyMetadata(false));


        /// <summary>
        /// 
        /// </summary>
        public Brush ShadowFill
        {
            get { return (Brush)GetValue(ShadowFillProperty); }
            set { SetValue(ShadowFillProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for ShadowFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowFillProperty =
            DependencyProperty.Register("ShadowFill", typeof(Brush), typeof(RowSeries), new PropertyMetadata(Brushes.Blue, CallChartUpdater()));


        /// <summary>
        /// The maximum row heigth property
        /// </summary>
        public static readonly DependencyProperty MaxRowHeigthProperty = DependencyProperty.Register(
            "MaxRowHeigth", typeof (double), typeof (RowSeries), new PropertyMetadata(35d));
        /// <summary>
        /// Gets or sets the maximum row height, the height of a column will be capped at this value
        /// </summary>
        public double MaxRowHeigth
        {
            get { return (double) GetValue(MaxRowHeigthProperty); }
            set { SetValue(MaxRowHeigthProperty, value); }
        }

        /// <summary>
        /// The row padding property
        /// </summary>
        public static readonly DependencyProperty RowPaddingProperty = DependencyProperty.Register(
            "RowPadding", typeof (double), typeof (RowSeries), new PropertyMetadata(2d));
       
        /// <summary>
        /// Gets or sets the padding between rows in this series
        /// </summary>
        public double RowPadding
        {
            get { return (double) GetValue(RowPaddingProperty); }
            set { SetValue(RowPaddingProperty, value); }
        }

        /// <summary>
        /// The labels position property
        /// </summary>
        public static readonly DependencyProperty LabelsPositionProperty = DependencyProperty.Register(
            "LabelsPosition", typeof(BarLabelPosition), typeof(RowSeries), 
            new PropertyMetadata(default(BarLabelPosition), CallChartUpdater()));
        /// <summary>
        /// Gets or sets where the label is placed
        /// </summary>
        public BarLabelPosition LabelsPosition
        {
            get { return (BarLabelPosition)GetValue(LabelsPositionProperty); }
            set { SetValue(LabelsPositionProperty, value); }
        }

        /// <summary>
        /// The shares position property
        /// </summary>
        public static readonly DependencyProperty SharesPositionProperty = DependencyProperty.Register(
            "SharesPosition", typeof(bool), typeof(RowSeries), new PropertyMetadata(default(bool)));
        /// <summary>
        /// Gets or sets a value indicating whether this row shares space with all the row series in the same position
        /// </summary>
        /// <value>
        /// <c>true</c> if [shares position]; otherwise, <c>false</c>.
        /// </value>
        public bool SharesPosition
        {
            get { return (bool)GetValue(SharesPositionProperty); }
            set { SetValue(SharesPositionProperty, value); }
        }

        /// <summary>
        /// The shares position property修改
        /// </summary>
        public static readonly DependencyProperty DataLabelsEndProperty = DependencyProperty.Register(
            "DataLabelsEnd", typeof(bool), typeof(RowSeries), new PropertyMetadata(default(bool)));
        /// <summary>
        /// Gets or sets a value indicating whether this row shares space with all the row series in the same position
        /// </summary>
        /// <value>
        /// <c>true</c> if [shares position]; otherwise, <c>false</c>.
        /// </value>
        public bool DataLabelsEnd
        {
            get { return (bool)GetValue(DataLabelsEndProperty); }
            set { SetValue(DataLabelsEndProperty, value); }
        }



        /// <summary>
        /// 
        /// </summary>
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(RowSeries), new PropertyMetadata(0d, CallChartUpdater()));


        #endregion

        #region Overridden Methods

        /// <summary>
        /// Gets the point view.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public override IChartPointView GetPointView(ChartPoint point, string label)
        {
            var pbv = (RowPointView)point.View;

            if (pbv == null)
            {
                pbv = new RowPointView
                {
                    IsNew = true,
                    Rectangle = new Rectangle(),
                    Data = new CoreRectangle(),
                    RectangleShadow = new Rectangle()
                };

                Model.Chart.View.AddToDrawMargin(pbv.Rectangle);
                Model.Chart.View.AddToDrawMargin(pbv.RectangleShadow);
            }
            else
            {
                pbv.IsNew = false;
                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.Rectangle);
                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.HoverShape);
                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.DataLabel);

                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.DataLabelEnd);
                point.SeriesView.Model.Chart.View
                    .EnsureElementBelongsToCurrentDrawMargin(pbv.RectangleShadow);
            }
            //修改
            pbv.Rectangle.RadiusX = CornerRadius;
            pbv.Rectangle.RadiusY = CornerRadius;

            pbv.Rectangle.Fill = Fill;
            pbv.Rectangle.Stroke = Stroke;
            pbv.Rectangle.StrokeThickness = StrokeThickness;
            pbv.Rectangle.StrokeDashArray = StrokeDashArray;
            pbv.Rectangle.Visibility = Visibility;
            Panel.SetZIndex(pbv.Rectangle, Panel.GetZIndex(this));


            if(ShowShadow)
            {              
                pbv.RectangleShadow.RadiusX = CornerRadius;
                pbv.RectangleShadow.RadiusY = CornerRadius;

                pbv.RectangleShadow.Fill = ShadowFill;
                pbv.RectangleShadow.Opacity = 0.2;               
                pbv.RectangleShadow.Visibility = Visibility;
                Panel.SetZIndex(pbv.RectangleShadow, -1);              
            }

           
            if (Model.Chart.RequiresHoverShape && pbv.HoverShape == null)
            {
                pbv.HoverShape = new Rectangle
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
            //修改
            if (DataLabelsEnd)
            {
                if (Percentage != null)
                {
                    double per = Percentage[point.Key];
                    pbv.DataLabelEnd = UpdateLabelContent(new DataLabelViewModel
                    {
                        //FormattedText = string.Format("同比{0}%", per),
                        FormattedText = per <= 0 ? per.ToString() : string.Format("+{0}", per),
                        Point = point
                    }, pbv.DataLabelEnd, true);
                }

            }

            if (!DataLabels && pbv.DataLabel != null)
            {
                Model.Chart.View.RemoveFromDrawMargin(pbv.DataLabel);
                pbv.DataLabel = null;
            }
            //修改
            if (!DataLabelsEnd && pbv.DataLabelEnd != null)
            {
                Model.Chart.View.RemoveFromDrawMargin(pbv.DataLabelEnd);
                pbv.DataLabelEnd = null;
            }
         
            if (point.Stroke != null) pbv.Rectangle.Stroke = (Brush)point.Stroke;
            if (point.Fill != null) pbv.Rectangle.Fill = (Brush)point.Fill;

          

            pbv.LabelPosition = LabelsPosition;

            return pbv;
        }

        #endregion

        #region Private Methods

        private void InitializeDefuaults()
        {
            SetCurrentValue(StrokeThicknessProperty, 0d);
            SetCurrentValue(MaxRowHeigthProperty, 35d);
            SetCurrentValue(RowPaddingProperty, 2d);
            SetCurrentValue(LabelsPositionProperty, BarLabelPosition.Top);

            Func<ChartPoint, string> defaultLabel = x => x.EvaluatesGantt
                ? string.Format("starts {0}, ends {1}", Model.CurrentXAxis.GetFormatter()(x.XStart),
                    Model.CurrentXAxis.GetFormatter()(x.X))
                : Model.CurrentXAxis.GetFormatter()(x.X);
            SetCurrentValue(LabelPointProperty, defaultLabel);

            DefaultFillOpacity = 1;
        }

        #endregion
    }
}
