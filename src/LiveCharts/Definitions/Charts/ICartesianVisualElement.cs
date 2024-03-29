﻿//The MIT License(MIT)

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

using LiveCharts.Charts;
using System.Windows;

namespace LiveCharts.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICartesianVisualElement
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        double X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        double Y { get; set; }
        /// <summary>
        /// Gets or sets the axis x.
        /// </summary>
        /// <value>
        /// The axis x.
        /// </value>
        int AxisX { get; set; }
        /// <summary>
        /// Gets or sets the axis y.
        /// </summary>
        /// <value>
        /// The axis y.
        /// </value>
        int AxisY { get; set; }
        /// <summary>
        /// Adds the or move.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void AddOrMove(ChartCore chart);
        /// <summary>
        /// Removes the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void Remove(ChartCore chart);
        /// <summary>
        /// 
        /// </summary>
        FrameworkElement UIElement { get; set; }
    }
}