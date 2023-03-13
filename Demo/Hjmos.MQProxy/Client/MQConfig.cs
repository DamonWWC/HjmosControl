using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Hjmos.MQProxy
{
    /// <summary>
    /// MQ配置类
    /// </summary>
    public class MQConfig : ConfigurationSection, IParameterConfig
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        [ConfigurationProperty("ip", IsRequired = true)]
        public string NameServerAddress
        {
            get 
            { 
                return (string)base["ip"]; 
            }
        }

        /// <summary>
        /// 访问令牌
        /// </summary>
        [ConfigurationProperty("accessKey", IsRequired = false, DefaultValue = "*")]
        public string AccessKey
        {
            get
            {
                return (string)base["accessKey"];
            }
        }
        /// <summary>
        /// 访问密钥
        /// </summary>
        [ConfigurationProperty("secretKey",IsRequired =false, DefaultValue = "*")]
        public string SecretKey
        {
            get
            {
                return (string)base["secretKey"];
            }
        }
        /// <summary>
        /// 客户端集合
        /// </summary>
        [ConfigurationProperty("MQClients",IsDefaultCollection =true)]
        [ConfigurationCollection(typeof(MQClientCollection),AddItemName ="MQClient")]
        public MQClientCollection MQClients
        {
            get
            {
                return (MQClientCollection)base["MQClients"];
            }
        }
        /// <summary>
        /// 订阅方式
        /// </summary>
        [ConfigurationProperty("messageModel",IsRequired =false,DefaultValue =SubscriptionMode.CLUSTERING)]
        public SubscriptionMode MessageModel
        {
            get
            {
                return (SubscriptionMode)base["messageModel"];
            }
        }
        /// <summary>
        /// 实例名称
        /// </summary>
        [ConfigurationProperty("instanceName",IsRequired =false,DefaultValue ="*")]
        public string InstanceName
        {
            get
            {
                return (string)base["instanceName"];
            }
        }
    }
    /// <summary>
    /// 客户端集合类
    /// </summary>
    public class MQClientCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
        }
        /// <summary>
        /// 创建客户端实例
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            
            return new MQClientType(); 
        }
        /// <summary>
        /// 获取客户端名称
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MQClientType)element).Name;
        }
        /// <summary>
        /// 获取所有键
        /// </summary>
        public IEnumerable<string> AllKeys { get { return BaseGetAllKeys().Cast<string>(); } }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public new MQClientType this[string name]
        {
            get { return (MQClientType)BaseGet(name); }
        }
    }
    /// <summary>
    /// MQ客户端类型实体类
    /// </summary>
    public class MQClientType : ConfigurationElement
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
        }
        /// <summary>
        /// 主题
        /// </summary>
        [ConfigurationProperty("topic", IsRequired = true)]
        public string Topic
        {
            get
            {
                return (string)base["topic"];
            }
        }
        /// <summary>
        /// 标签
        /// </summary>
        [ConfigurationProperty("tag", IsRequired = false, DefaultValue = "*")]
        public string Tag
        {
            get
            {
                return (string)base["tag"];
            }
        }
    }


}
