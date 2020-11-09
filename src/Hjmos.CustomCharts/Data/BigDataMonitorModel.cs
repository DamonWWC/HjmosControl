using System.Collections.ObjectModel;

namespace Hjmos.CustomCharts.Data
{
    //public class BigDataMonitorModel : ModelBase
    //{
    //    private long _TotalQuantity;
    //    /// <summary>
    //    /// 数据总量
    //    /// </summary>
    //    public long TotalQuantity
    //    {
    //        get { return _TotalQuantity; }
    //        set 
    //        {

    //            SetProperty(ref _TotalQuantity, value);
    //        }
    //    }

    //    private ObservableCollection<BigDataItemModel>  _BigDataList;
    //    /// <summary>
    //    /// 数据集合
    //    /// </summary>
    //    public ObservableCollection<BigDataItemModel> BigDataList
    //    {
    //        get { return _BigDataList; }
    //        set { SetProperty(ref _BigDataList,value); }
    //    }


    //}

    public class BigDataItemModel : ModelBase
    {
        public BigDataItemModel(string bigdatatype,Unit unit,double quantity,string imageSource)
        {
            BigDataType = bigdatatype;
            Unit = unit;
            Quantity = quantity;
            ImageSource = imageSource;

        }

        private string _BigDataType;
        /// <summary>
        /// 数据类型
        /// </summary>
        public string BigDataType
        {
            get { return _BigDataType; }
            set { SetProperty(ref _BigDataType, value); }
        }

        private string _ImageSource;

        public string ImageSource
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource,value); }
        }


        private Unit  _Unit;
        /// <summary>
        /// 数据单位
        /// </summary>
        public Unit  Unit
        {
            get { return _Unit; }
            set { SetProperty(ref _Unit,value); }
        }

        private double _Quantity;
        /// <summary>
        /// 数据量
        /// </summary>
        public double Quantity
        {
            get { return _Quantity; }
            set { SetProperty(ref _Quantity,value); }
        }

    }
}
