
using System;
using System.Configuration;
using System.Linq;

namespace Hjmos.MQProxy
{
    /// <summary>
    /// 连接工厂类
    /// </summary>
    public static class ConnectionFactory
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public static IConsumer Consumer(IParameter parameter)
        {
            IConsumer consumer = null ;
            
            if(parameter is RocketMQPara para)
            {
                consumer = new ConsumerMQ
                {
                    Topic = para.Topic,
                    Tag = para.Tag,
                    ConsumerGroupID = para.ConsumerGroupID,
                    MessageModel = para.MessageModel,
                    NameServerAddress = para.NameServerAddress,
                    AccessKey = para.AccessKey,
                    SecretKey = para.SecretKey,
                    InstanceName = para.InstanceName
                };
            }
         
            return consumer;
        }

        /// <summary>
        /// 获取消费者实体
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static IConsumer PushConsumer(IParameter parameter)
        {
            IConsumer consumer = null;

            if (parameter is RocketMQPara para)
            {
                consumer = new PushConsumerMQ
                {                   
                    ConsumerGroupID = para.ConsumerGroupID,
                    MessageModel = para.MessageModel,
                    NameServerAddress = para.NameServerAddress,
                    AccessKey = para.AccessKey,
                    SecretKey = para.SecretKey,
                    InstanceName = para.InstanceName
                };               
            }
            return consumer;
        }
        /// <summary>
        /// 实例化消费者对象
        /// </summary>
        /// <param name="mQInstanceName">MQ实例名称</param>
        /// <param name="consumerGroupID">消费者GroupID</param>
        /// <returns></returns>
        public static IConsumer Consumer(string mQInstanceName, string consumerGroupID="*")
        {          
            try
            {
                IConsumer consumer = null;

                object section = ConfigurationManager.GetSection("MQ");

                if (section == null)
                {
                    throw new ConfigurationErrorsException("Missing 'MQ' configuartion section");
                }

                IParameterConfig parameterConfig = section as IParameterConfig;

                var mQClient = parameterConfig.MQClients[mQInstanceName];
                if(mQClient==null)
                {
                    throw new ConfigurationErrorsException("No Find Client");
                }
                //var aa = parameterConfig.MQClients.OfType<MQClientType>().FirstOrDefault((p) => p.Name == mQInstanceName);

                consumer = new ConsumerMQ
                {
                    Topic = mQClient.Topic,
                    Tag = mQClient.Tag,
                    ConsumerGroupID = consumerGroupID,
                    MessageModel = parameterConfig.MessageModel,
                    NameServerAddress = parameterConfig.NameServerAddress,
                    AccessKey = parameterConfig.AccessKey,
                    SecretKey = parameterConfig.SecretKey,
                    InstanceName = parameterConfig.InstanceName
                };

                return consumer;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

    }
}
