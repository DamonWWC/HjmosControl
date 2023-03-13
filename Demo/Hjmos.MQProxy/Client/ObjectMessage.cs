
namespace Hjmos.MQProxy
{
    /// <summary>
    /// 消息内容抽象类
    /// </summary>
    public abstract class ObjectMessage : IMessage
    {
        /// <summary>
        /// 消息体
        /// </summary>
        public string BodyMsg { get ; set ; }
        public string Tag { get ; set ; }
        public string Topic { get; set; }

        /// <summary>
        /// 消息实体抽象类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>消息实例</returns>
        public abstract T Body<T>() where T : class;
    }
}
