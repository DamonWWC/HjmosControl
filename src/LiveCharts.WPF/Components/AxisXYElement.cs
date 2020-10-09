using LiveCharts.Charts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LiveCharts.Wpf.Components
{
    /// <summary>
    /// 
    /// </summary>
   public class AxisXYElement: IAxisXYElementView
    {
        private readonly AxisXYElementCore _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxisSeparatorElement"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public AxisXYElement(AxisXYElementCore model)
        {
            _model = model;
        }


        internal Line Line { get; set; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public AxisXYElementCore Model
        {
            get { return _model; }
        }


        /// <summary>
        /// Clears the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public void Clear(IChartView chart)
        {
            chart.RemoveFromView(Line);
            Line = null;
        }

        /// <summary>
        /// Places the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>      
        /// <param name="direction">The direction.</param>       
        public void Place(ChartCore chart, AxisOrientation direction)
        {
            if (direction == AxisOrientation.Y)
            {
                Line.X1 = chart.DrawMargin.Left;
                Line.X2 = chart.DrawMargin.Left;
                Line.Y1 = chart.DrawMargin.Top;
                Line.Y2 = chart.DrawMargin.Top + chart.DrawMargin.Height;
                //Line.Stroke = Brushes.Red;
            }
            else
            {
                Line.X1 = chart.DrawMargin.Left;
                Line.X2 = chart.DrawMargin.Left + chart.DrawMargin.Width;
                Line.Y1 = chart.DrawMargin.Top + chart.DrawMargin.Height;
                Line.Y2 = chart.DrawMargin.Top + chart.DrawMargin.Height;
                //Line.Stroke = Brushes.Blue;              
            }
        }

        /// <summary>
        /// Removes the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public void Remove(ChartCore chart)
        {

            chart.View.RemoveFromView(Line);

            Line = null;
        }

        /// <summary>
        /// Moves the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>  
        /// <param name="direction">The direction.</param>

        public void Move(ChartCore chart, AxisOrientation direction)
        {
            try
            {
                if (direction == AxisOrientation.Y)
                {
                    Line.BeginAnimation(Line.X1Property,
                  new DoubleAnimation(chart.DrawMargin.Left, chart.View.AnimationsSpeed));
                    Line.BeginAnimation(Line.X2Property,
                        new DoubleAnimation(chart.DrawMargin.Left, chart.View.AnimationsSpeed));
                    Line.BeginAnimation(Line.Y1Property,
                        new DoubleAnimation(chart.DrawMargin.Top, chart.View.AnimationsSpeed));
                    Line.BeginAnimation(Line.Y2Property,
                        new DoubleAnimation(chart.DrawMargin.Top + chart.DrawMargin.Height, chart.View.AnimationsSpeed));
                }
                else
                {

                    Line.BeginAnimation(Line.X1Property,
                  new DoubleAnimation(chart.DrawMargin.Left, chart.View.AnimationsSpeed));
                    Line.BeginAnimation(Line.X2Property,
                        new DoubleAnimation(chart.DrawMargin.Left + chart.DrawMargin.Width, chart.View.AnimationsSpeed));
                    Line.BeginAnimation(Line.Y1Property,
                        new DoubleAnimation(chart.DrawMargin.Top + chart.DrawMargin.Height, chart.View.AnimationsSpeed));
                    Line.BeginAnimation(Line.Y2Property,
                        new DoubleAnimation(chart.DrawMargin.Top + chart.DrawMargin.Height, chart.View.AnimationsSpeed));
                }
            }
            catch (Exception ex)
            {

            }


        }
        /// <summary>
        /// Fades the in.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="chart">The chart.</param>
        public void FadeIn(AxisCore axis, ChartCore chart)
        {
            try
            {
                if (Line.Visibility != Visibility.Collapsed)
                    Line.BeginAnimation(UIElement.OpacityProperty,
                        new DoubleAnimation(0, 1, chart.View.AnimationsSpeed));
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Fades the out and remove.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public void FadeOutAndRemove(ChartCore chart)
        {

            var anim = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = chart.View.AnimationsSpeed
            };

            var dispatcher = ((Chart)chart.View).Dispatcher;
            anim.Completed += (sender, args) =>
            {
                dispatcher.Invoke(new Action(() =>
                {

                    chart.View.RemoveFromView(Line);
                }));
            };
            Line.BeginAnimation(UIElement.OpacityProperty,
                new DoubleAnimation(1, 0, chart.View.AnimationsSpeed));
        }
    }
}
