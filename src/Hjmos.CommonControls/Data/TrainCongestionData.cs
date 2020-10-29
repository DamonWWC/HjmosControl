using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Hjmos.CommonControls
{
    public class TrainCongestionData: INotifyPropertyChanged,IComparable<TrainCongestionData>
    {

        private int _Index;

        public int Index
        {
            get { return _Index; }
            set { _Index = value; OnPropertyChanged(); }
        }

        private ConfestionStatus _ConfestionStatus;

        public ConfestionStatus ConfestionStatus
        {
            get { return _ConfestionStatus; }
            set { _ConfestionStatus = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int CompareTo(TrainCongestionData other)
        {
            return this._Index.CompareTo(other._Index);
        }
    }
    
}
