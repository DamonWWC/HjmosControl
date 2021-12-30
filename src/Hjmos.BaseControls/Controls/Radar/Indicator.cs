using Hjmos.BaseControls.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class Indicator : ModelBase
    {
        private string _Name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        private double _Max = 100;

        /// <summary>
        /// 最大值
        /// </summary>
        public double Max
        {
            get { return _Max; }
            set { SetProperty(ref _Max, value); }
        }

        internal Point PointValue { get; set; }
    }
}