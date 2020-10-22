using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Hjmos.CustomCharts
{
    public class FlowData : INotifyPropertyChanged, IComparable<FlowData>
    {
        private double _Width;
        public double Width
        {
            get { return _Width; }
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged();
                }
            }
        }


        private double _RealTimeValue;
        public double RealTimeValue
        {
            get { return _RealTimeValue; }
            set
            {
                if (_RealTimeValue != value)
                {
                    _RealTimeValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _PostValue;
        public double PostValue
        {
            get { return _PostValue; }
            set
            {
                if (_PostValue != value)
                {
                    _PostValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CompareTo(FlowData other)
        {
            return this._RealTimeValue.CompareTo(other._RealTimeValue);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
