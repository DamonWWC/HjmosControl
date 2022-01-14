//using Hjmos.Log;
using Newtonsoft.Json;
using System;

namespace Hjmos.MQProxy
{
    /// <summary>
    /// 消息拓展
    /// </summary>
    public class MessageExt
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        public string MsgID { get; set; }

        /// <summary>
        /// 起始接收时间
        /// </summary>
        public long StartDeliverTime { get; set; }

        /// <summary>消息体</summary>
        public string Body { get; set; }

        /// <summary>
        /// 消息类型转换
        /// </summary>
        /// <returns>转换后的消息</returns>
        public object BodyObject()
        {
            try
            {
                //LogHelper.Info(Body);
                return Body == null ? default : JsonConvert.DeserializeObject<object>(Body);
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.ToString());
                return default;
            }
        }

        /// <summary>
        /// 消息体
        /// </summary>
        public string MsgBody { get; set; }

        /// <summary>重新消费次数</summary>
        public int ReconsumeTimes { get; set; }

        /// <summary>存储时间</summary>
        public long StoreTimestamp { get; set; }

        /// <summary>队列偏移</summary>
        public long QueueOffset { get; set; }
    }
}