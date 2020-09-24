using Newtonsoft.Json;
using Prism.Mvvm;
using System.Collections.Generic;

namespace Hjmos.CommonControls
{
    /// <summary>
    /// 视频监控实体类
    /// </summary>
    internal class VideoMonitor : BindableBase
    {
        private string _MonitorPlace;
        /// <summary>
        /// 监控位置（车站/车站区间名称）
        /// </summary>
        [JsonProperty("monitorPlace")]
        public string MonitorPlace
        {
            get
            {
                return _MonitorPlace;
            }
            set
            {
                SetProperty(ref _MonitorPlace, value);
            }
        }

        private List<Camera> _CameraList;
        /// <summary>
        /// 摄像头列表
        /// </summary>
        [JsonProperty("cameraList")]
        public List<Camera> CameraList
        {
            get
            {
                return _CameraList;
            }
            set
            {
                SetProperty(ref _CameraList, value);
            }
        }
    }
}
