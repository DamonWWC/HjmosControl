

namespace Hjmos.MQProxy
{
    /// <summary>
    /// 消息内容类
    /// </summary>
    public abstract class TextMessage : IMessage
    {
        /// <summary>
        /// 消息体
        /// </summary>
        public string BodyMsg { get ; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Text { get; set; }
        public string Tag { get; set; }
        public string Topic { get; set; }
    }
}
