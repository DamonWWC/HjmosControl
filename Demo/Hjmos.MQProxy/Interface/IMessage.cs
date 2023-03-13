namespace Hjmos.MQProxy
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 消息体
        /// </summary>
        string BodyMsg { get; set; }
        /// <summary>
        /// MQ标签
        /// </summary>
        string Tag { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        string Topic { get; set; }
    }
}
