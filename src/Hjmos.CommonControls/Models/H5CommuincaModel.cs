using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.CommonControls
{
    /// <summary>
    /// H5与WPF的通讯类
    /// </summary>
    [JsonObject]
    public class H5CommunicaModel
    {
        /// <summary>
        /// 命令
        /// </summary>
        [JsonProperty("command")]
        public string Command {   get; set;} = "C01";

        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty("param")]
        public Param Param { get; set; } = new Param();


        /// <summary>
        /// 当前的时间
        /// </summary>
        [JsonIgnore]
        public DateTime CurrentDateTime = DateTime.Now;
      
    }

    /// <summary>
    /// 参数的实例
    /// </summary>
    [JsonObject]
    public class Param
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        [JsonProperty("type")]
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// H5传递的参数
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; } = null;

    }
}
