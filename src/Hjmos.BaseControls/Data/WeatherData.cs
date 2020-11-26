using Hjmos.BaseControls.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.BaseControls
{
    public class WeatherData : ModelBase
    {

       
        private string _Condition;
        /// <summary>
        /// 天气情况
        /// </summary>
        public string Condition
        {
            get { return _Condition; }
            set { SetProperty(ref _Condition, value); }
        }

        
        private double _Temperature;
        /// <summary>
        /// 温度
        /// </summary>
        public double Temperature
        {
            get { return _Temperature; }
            set { SetProperty(ref _Temperature, value); }
        }


       
        private string _WindDirection;
        /// <summary>
        /// 风向
        /// </summary>
        public string WindDirection
        {
            get { return _WindDirection; }
            set { SetProperty(ref _WindDirection, value); }
        }

       
        private string _WindPower;
        /// <summary>
        /// 风力
        /// </summary>
        public string WindPower
        {
            get { return _WindPower; }
            set { SetProperty(ref _WindPower, value); }
        }

        
        private string _Precipitation;
        /// <summary>
        /// 降水量
        /// </summary>
        public string Precipitation
        {
            get { return _Precipitation; }
            set { SetProperty(ref _Precipitation, value); }
        }

        private string _Humidity;
        /// <summary>
        /// 湿度
        /// </summary>
        public string Humidity
        {
            get { return _Humidity; }
            set { SetProperty(ref _Humidity, value); }
        }

        private string _Pressure;
        /// <summary>
        /// 气压
        /// </summary>
        public string Pressure
        {
            get { return _Pressure; }
            set { SetProperty(ref _Pressure, value); }
        }




    }
}
