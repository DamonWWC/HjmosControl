using System;

namespace Hjmos.MQProxy
{
    /// <summary>
    /// MQ基类
    /// </summary>
    public  class MQBase
    {
        #region 属性
        /// <summary>
        /// 服务器地址
        /// </summary>
        public String NameServerAddress { get; set; }
      
        /// <summary>
        /// 主题
        /// </summary>
        //public String Topic { get; set; }

        /// <summary>访问令牌</summary>
        public String AccessKey { get; set; } = "*";

        /// <summary>访问密钥</summary>
        public String SecretKey { get; set; } = "*";
        /// <summary>
        /// 实例名称
        /// </summary>
        public String InstanceName { get; set; } = "*";
        #endregion
      
        /// <summary>
        /// 获取工厂属性值
        /// </summary>
        /// <returns>属性实体</returns>
        protected ONSFactoryProperty getFactoryProperty()
        {
            ONSFactoryProperty factoryInfo = new ONSFactoryProperty();
            factoryInfo.setFactoryProperty(ONSFactoryProperty.AccessKey, AccessKey);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.SecretKey, SecretKey);         
            factoryInfo.setFactoryProperty(ONSFactoryProperty.NAMESRV_ADDR, NameServerAddress);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.ConsumerInstanceName, InstanceName);
            //factoryInfo.setFactoryProperty(ONSFactoryProperty.LogPath, "C://log");


            //factoryInfo.setFactoryProperty(ONSFactoryProperty.ConsumerId, GroupID);
            //factoryInfo.setFactoryProperty(ONSFactoryProperty.ProducerId, GroupID);
            //factoryInfo.setFactoryProperty(ONSFactoryProperty.PublishTopics, Topic);

            return factoryInfo;
        }
    }
}
