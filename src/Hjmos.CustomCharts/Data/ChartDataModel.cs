using Newtonsoft.Json;
using System;


namespace Hjmos.CustomCharts
{
    public class ChartDataModel
    {
        [JsonProperty("time")]
        public DateTime DateTime { get; set; }
        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
