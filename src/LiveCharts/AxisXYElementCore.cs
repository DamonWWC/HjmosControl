using LiveCharts.Definitions.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveCharts
{
    /// <summary>
    /// 
    /// </summary>
  public  class AxisXYElementCore
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is new.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        public bool IsNew { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public AxisXYState State { get; set; }
        /// <summary>
        /// Gets or sets the index of the garbage collector.
        /// </summary>
        /// <value>
        /// The index of the garbage collector.
        /// </value>
        public int GarbageCollectorIndex { get; set; }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public double Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public IAxisXYElementView View { get; set; }
    }
}
