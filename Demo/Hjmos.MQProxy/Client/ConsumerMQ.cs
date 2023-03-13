using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Hjmos.MQProxy
{
    /// <summary>
    /// 消息监听类
    /// </summary>
    public class MyMsgListener : MessageListener
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyMsgListener()
        {
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~MyMsgListener()
        {
        }
        /// <summary>
        /// 消费触发事件
        /// </summary>
        public Action<IMessage> OnConsume;
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="value">接收到的消息</param>
        /// <param name="context">消费上下文</param>
        /// <returns>处理消息的方式</returns>
        public override Hjmos.MQProxy.Action consume(Message value, ConsumeContext context)
        {          
            OnConsume?.Invoke(CreateMessage(value));
            return Hjmos.MQProxy.Action.CommitMessage;
        }
        /// <summary>
        /// 消息转换
        /// </summary>
        /// <param name="value">接收到的原始消息</param>
        /// <returns>转换后的消息</returns>
        private IMessage CreateMessage(Message value)
        {
            var bodymsg = value.getBody();
            var tag = value.getTag();
            var topic = value.getTopic();
            try
            {               
                if (bodymsg == null) return null;
                
                var result = JsonConvert.DeserializeObject(bodymsg);
                if (result is string str)
                {
                    TextMessage textMessage = new TextRocketMessage
                    {
                        Text = str,
                        BodyMsg= bodymsg,
                        Tag=tag,
                        Topic=topic
                        
                    };
                    return textMessage;
                }
                else if (result is JObject jObject)
                {
                    ObjectMessage objectMessage = new ObjectRocketMessage
                    {                        
                        JBody = jObject,
                        BodyMsg = bodymsg,
                        Tag=tag,
                        Topic = topic
                    };
                    return objectMessage;
                }
            }
            catch(Exception ex)
            {
                TextMessage textMessage = new TextRocketMessage
                {
                    Text = bodymsg,
                    BodyMsg = bodymsg,
                    Tag=tag,
                    Topic = topic
                };
                return textMessage;
            }           
            return null;
        }
    }
    /// <summary>
    /// 消费者类
    /// </summary>
    internal class ConsumerMQ : RocketMQPara,IConsumer
    {
        /// <summary> 消费组实例</summary>
        private PushConsumer _consumer;
        /// <summary> 监听消息实例</summary>
        private  MyMsgListener _listener;
        //public Action<MessageExt> OnConsume;
        /// <summary>
        /// 消息订阅方式
        /// </summary>
        private string _messagemodel;
        /// <summary> 消费触发事件</summary>      
        public Action<IMessage> OnConsume { get ; set ; }
       
        /// <summary> 消息订阅启动</summary>
        public Boolean Start()
        {
            try
            {
                _consumer=InitPushConsumer();
                // 启动客户端实例
                _consumer.start();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary> 初始化消息订阅</summary>
        private PushConsumer InitPushConsumer()
        {
            InitListener();
            _consumer = ONSFactory.getInstance().createPushConsumer(InitoNSFactoryProperty());
            // 订阅 Topics
            _consumer.subscribe(Topic, Tag, _listener);
            return _consumer;

        }
        /// <summary>
        /// 初始化工厂属性
        /// </summary>
        /// <returns>属性实例</returns>
        private ONSFactoryProperty InitoNSFactoryProperty()
        {
            ONSFactoryProperty oNSFactoryProperty = getFactoryProperty();
            oNSFactoryProperty.setFactoryProperty(ONSFactoryProperty.ConsumerId, ConsumerGroupID);
            _messagemodel = MessageModel == 0 ? ONSFactoryProperty.CLUSTERING : ONSFactoryProperty.BROADCASTING;
            oNSFactoryProperty.setFactoryProperty(ONSFactoryProperty.MessageModel, _messagemodel);
            return oNSFactoryProperty;
        }
        /// <summary>
        /// 获取工厂属性
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 初始化监听器
        /// </summary>
        private void InitListener()
        {
            _listener = new MyMsgListener
            {
                OnConsume = (m) =>
                {
                    OnConsume?.Invoke(m);
                }
            };
        }
        /// <summary>
        /// 停止接受消息
        /// </summary>
        public  void Stop()
        {
            _consumer?.shutdown();
        }

        public void Subscribe(string topic, string tag)
        {
            
        }
    }



    internal class PushConsumerMQ: RocketMQPara,IConsumer
    {

        /// <summary> 消费组实例</summary>
        private PushConsumer _consumer;
        /// <summary> 监听消息实例</summary>
        private MyMsgListener _listener;
        //public Action<MessageExt> OnConsume;
        /// <summary>
        /// 消息订阅方式
        /// </summary>
        private string _messagemodel;
        /// <summary> 消费触发事件</summary>      
        public Action<IMessage> OnConsume { get; set; }

        /// <summary> 消息订阅启动</summary>
        public Boolean Start()
        {
            try
            {              
                // 启动客户端实例
                _consumer.start();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary> 初始化消息订阅</summary>
        private void InitPushConsumer()
        {
            InitListener();
            _consumer = ONSFactory.getInstance().createPushConsumer(InitoNSFactoryProperty());
        }
        /// <summary>
        /// 初始化工厂属性
        /// </summary>
        /// <returns>属性实例</returns>
        private ONSFactoryProperty InitoNSFactoryProperty()
        {
            ONSFactoryProperty oNSFactoryProperty = getFactoryProperty();
            oNSFactoryProperty.setFactoryProperty(ONSFactoryProperty.ConsumerId, ConsumerGroupID);
            _messagemodel = MessageModel == 0 ? ONSFactoryProperty.CLUSTERING : ONSFactoryProperty.BROADCASTING;
            oNSFactoryProperty.setFactoryProperty(ONSFactoryProperty.MessageModel, _messagemodel);
            return oNSFactoryProperty;
        }
        /// <summary>
        /// 获取工厂属性
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 初始化监听器
        /// </summary>
        private void InitListener()
        {
            _listener = new MyMsgListener
            {
                OnConsume = (m) =>
                {
                    OnConsume?.Invoke(m);
                }
            };
        }
        /// <summary>
        /// 停止接受消息
        /// </summary>
        public void Stop()
        {
            _consumer?.shutdown();
        }

        public void Subscribe(string topic, string tag)
        {
            if(_consumer==null)
            {
                InitPushConsumer();
            }
            // 订阅 Topics
            _consumer.subscribe(topic, tag, _listener);
        }
    }

}
