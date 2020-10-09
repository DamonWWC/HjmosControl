using LiveCharts.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveCharts.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
   public interface IAxisXYElementView
    {
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        AxisXYElementCore Model { get; }

        /// <summary>
        /// Clears the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void Clear(IChartView chart);

        //No animated methods
        /// <summary>
        /// Places the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>      
        void Place(ChartCore chart, AxisOrientation direction);

        /// <summary>
        /// Removes the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void Remove(ChartCore chart);

        //Animated methods
        /// <summary>
        /// Moves the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>      
        void Move(ChartCore chart, AxisOrientation direction);

        /// <summary>
        /// Fades the in.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="chart">The chart.</param>
        void FadeIn(AxisCore axis, ChartCore chart);
        /// <summary>
        /// Fades the out and remove.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void FadeOutAndRemove(ChartCore chart);
    }
}
