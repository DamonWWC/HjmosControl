using System;


namespace Hjmos.MQProxy
{
    /// <summary>
    /// MQ参数类
    /// </summary>
    public class RocketMQPara : IParameter
    {
        /// <summary>
        /// 消费者分组ID
        /// </summary>
        public string ConsumerGroupID { get; set; } = "*";
        /// <summary> 消息标签</summary>
        public string Tag { get; set; } = "*";
        /// <summary> 消息订阅方式</summary>
        public SubscriptionMode MessageModel { get; set; } = SubscriptionMode.CLUSTERING;
        /// <summary> 消费主题</summary>
        public string Topic { get; set; }     
        /// <summary>
        /// 服务器地址
        /// </summary>
        public String NameServerAddress { get; set; }
      
        /// <summary>访问令牌</summary>
        public String AccessKey { get; set; } = "*";

        /// <summary>访问密钥</summary>
        public String SecretKey { get; set; } = "*";
        /// <summary>
        /// 实例名称
        /// </summary>
        public String InstanceName { get; set; } = "*";
    }
}
