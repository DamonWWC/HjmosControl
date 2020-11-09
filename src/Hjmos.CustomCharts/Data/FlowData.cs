using System;


namespace Hjmos.CustomCharts
{
    public class FlowData : ModelBase, IComparable<FlowData>
    {
        private double _Width;
        public double Width
        {
            get { return _Width; }
            set
            {
                SetProperty(ref _Width, value);
            }
        }


        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                SetProperty(ref _Title, value);
            }
        }


        private double _RealTimeValue;
        public double RealTimeValue
        {
            get { return _RealTimeValue; }
            set
            {
                SetProperty(ref _RealTimeValue, value);
            }
        }

        private double _PostValue;
        public double PostValue
        {
            get { return _PostValue; }
            set
            {
                SetProperty(ref _PostValue, value);
            }
        }

        public int CompareTo(FlowData other)
        {
            return this._RealTimeValue.CompareTo(other._RealTimeValue);
        }     
    }
}
