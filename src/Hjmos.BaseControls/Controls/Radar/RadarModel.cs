using Hjmos.BaseControls.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class RadarModel : ViewModelBase
    {
        public string Text { get; set; }

        private int _valueMax;

        public int ValueMax
        {
            get { return _valueMax; }
            set
            {
                _valueMax = value;
                this.NotifyPropertyChange("ValueMax");
            }
        }

        private Point _pointValue;

        public Point PointValue
        {
            get { return _pointValue; }
            set
            {
                _pointValue = value;
                this.NotifyPropertyChange("PointValue");
            }
        }
    }
}