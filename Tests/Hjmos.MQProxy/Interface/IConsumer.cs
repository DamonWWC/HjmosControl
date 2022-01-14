using System;


namespace Hjmos.MQProxy
{
    /// <summary>
    /// 消费者接口
    /// </summary>
    public interface IConsumer
    {
        /// <summary>
        /// 开始接受消息
        /// </summary>
        /// <returns>是否成功</returns>
        bool Start();
        /// <summary>
        /// 停止接受消息
        /// </summary>
        void Stop();
        /// <summary>
        /// 消费触发事件
        /// </summary>
        Action<IMessage> OnConsume { get; set; }
        /// <summary>
        /// 订阅方法
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="tag"></param>
        void Subscribe(string topic, string tag);
    }
}
