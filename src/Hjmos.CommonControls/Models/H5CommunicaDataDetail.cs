using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.CommonControls
{
    /// <summary>
    /// 传值车站详情的数据
    /// </summary>
    [JsonObject]
    public class StationDetailData
    {
        /// <summary>
        /// 车站Id
        /// </summary>
        [JsonProperty("stationId")]
        public string StationId
        {
            get; set;
        }

        /// <summary>
        /// 车站编号
        /// </summary>
        [JsonProperty("sortId")]
        public string SortId
        {
            get; set;
        }
    }

    /// <summary>
    /// 线网单条线的数据回调
    /// </summary>
    [JsonObject]
    public class LineData
    {
        /// <summary>
        /// 线路Id，没有则为Null
        /// </summary>
        [JsonProperty("lineId")]
        public string LineId { get; set; }

        /// <summary>
        /// 线路颜色
        /// </summary>
        [JsonProperty("lineColor")]
        public string LineColor { get; set; }

    }

    /// <summary>
    /// 线网图切换成站点，使能视频监控的浮框和切换车站的视频
    /// </summary>
    [JsonObject]
    public class StationVideoMonitorData
    {
        /// <summary>
        /// 车站的Id，没有为Null
        /// </summary>
        [JsonProperty("stationId")]
        public string StationId { get; set; }

        /// <summary>
        /// 车站名称
        /// </summary>
        [JsonProperty("stationName")]
        public string StationName { get; set; }

        /// <summary>
        /// 区间Id
        /// </summary>
        [JsonProperty("sectionId")]
        public string SectionId { get; set; }

        /// <summary>
        /// 区间标记
        /// </summary>
        [JsonProperty("sectionTag")]
        public string SectionTag { get; set; }

        /// <summary>
        /// 区间名称
        /// </summary>
        [JsonProperty("sectionName")]
        public string SectionName { get; set; }
    }

    /// <summary>
    /// 点击线网图的行车，回传的信息
    /// </summary>
    [JsonObject]
    public class MetroMonitorData
    {
        /// <summary>
        /// 线路的Id
        /// </summary>
        [JsonProperty("lineId")]
        public string LineId { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        [JsonProperty("lineName")]
        public string LineName { get; set; }

        /// <summary>
        /// 当前点击的车辆的Id
        /// </summary>
        [JsonProperty("metroId")]
        public string MetroId { get; set; }        
    }

    /// <summary>
    /// 线网图点击放大缩小的最值
    /// </summary>
    [JsonObject]
    public class ScrollMaximumData
    {
        [JsonProperty("max")]
        public bool Max { get; set; }

        [JsonProperty("min")]
        public bool Min { get; set; }
    }
}
