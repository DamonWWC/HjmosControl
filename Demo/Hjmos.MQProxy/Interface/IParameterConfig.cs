namespace Hjmos.MQProxy
{
    /// <summary>
    /// 参数配置接口
    /// </summary>
    interface IParameterConfig
    {
        ///// <summary> 消费主题</summary>
        //string Topic { get; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        string NameServerAddress { get; }

        /// <summary>访问令牌</summary>
        string AccessKey { get;}

        /// <summary>访问密钥</summary>
        string SecretKey { get; }

        //public string ConsumerGroupID { get; }
        ///// <summary> 消息标签</summary>
        //public string Tag { get; }
        /// <summary> 消息订阅方式</summary>
        public SubscriptionMode MessageModel { get;  }
        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get;  }
        /// <summary>
        /// 客户端集合
        /// </summary>
        public MQClientCollection MQClients { get; }
    }
}
