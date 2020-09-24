using Newtonsoft.Json;
using Prism.Mvvm;

namespace Hjmos.CommonControls
{
    /// <summary>
    /// 摄像头实体类
    /// </summary>
    public class Camera : BindableBase
    {
        private string _CameraCode;
        /// <summary>
        /// 摄像头编号
        /// </summary>
        [JsonProperty("cameraCode")]
        public string CameraCode
        {
            get
            {
                return _CameraCode;
            }
            set
            {
                SetProperty(ref _CameraCode, value);
            }
        }

        private string _CameraName;
        /// <summary>
        /// 摄像头名称
        /// </summary>
        [JsonProperty("cameraName")]
        public string CameraName
        {
            get
            {
                return _CameraName;
            }
            set
            {
                SetProperty(ref _CameraName, value);
            }
        }

        private string _CameraType;
        /// <summary>
        /// 摄像头类型
        /// </summary>
        [JsonProperty("cameraType")]
        public string CameraType
        {
            get
            {
                return _CameraType;
            }
            set
            {
                SetProperty(ref _CameraType, value);
            }
        }

        private string _CameraLocation;
        /// <summary>
        /// 摄像头位置
        /// </summary>
        [JsonProperty("cameraLocation")]
        public string CameraLocation
        {
            get
            {
                return _CameraLocation;
            }
            set
            {
                SetProperty(ref _CameraLocation, value);
            }
        }

        private string _CameraChannel;
        /// <summary>
        /// 摄像头通道口
        /// </summary>
        [JsonProperty("cameraChannel")]
        public string CameraChannel
        {
            get
            {
                return _CameraChannel;
            }
            set
            {
                SetProperty(ref _CameraChannel, value);
            }
        }

        private string _CameraModule;
        /// <summary>
        /// 摄像头品牌
        /// </summary>
        [JsonProperty("cameraModule")]
        public string CameraModule
        {
            get
            {
                return _CameraModule;
            }
            set
            {
                SetProperty(ref _CameraModule, value);
            }
        }

        private string _CameraDesc;
        /// <summary>
        /// 摄像头描述
        /// </summary>
        [JsonProperty("cameraDesc")]
        public string CameraDesc
        {
            get
            {
                return _CameraDesc;
            }
            set
            {
                SetProperty(ref _CameraDesc, value);
            }
        }

        private string _Url;
        /// <summary>
        /// 摄像头的Url
        /// </summary>
        [JsonProperty("url")]
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                SetProperty(ref _Url, value);
            }
        }

        private string _MonitorPlace;
        /// <summary>
        /// 当前摄像头所属的位置
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
    }
}
