using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Hjmos.MQProxy;

namespace test
{
 
    public class MyMsgListener : MessageListener
    {
        public MyMsgListener()
        {
        }

        ~MyMsgListener()
        {
        }

        public override Hjmos.MQProxy.Action consume(Message value, ConsumeContext context)
        {
            Console.WriteLine("consume:");
            Console.WriteLine(value.getBody());
            return Hjmos.MQProxy.Action.CommitMessage;
        }
    }

    public class MyLocalTransactionChecker : LocalTransactionChecker
    {
        public MyLocalTransactionChecker()
        {

        }
        ~MyLocalTransactionChecker()
        {

        }
        public override TransactionStatus check(Message value)
        {
            return TransactionStatus.CommitTransaction;
        }


    }




    public class MyMsgOrderListener : MessageOrderListener
    {
        public MyMsgOrderListener()
        {

        }

        ~MyMsgOrderListener()
        {
        }

        public override Hjmos.MQProxy.OrderAction consume(Message value, ConsumeOrderContext context)
        {
            Byte[] text = Encoding.Default.GetBytes(value.getBody());
            Console.WriteLine(Encoding.UTF8.GetString(text));
            //Console.WriteLine(value.getBody());
            return Hjmos.MQProxy.OrderAction.Success;
        }
    }


    public class OnscSharp
    {
        private static Producer _producer;
        private static PushConsumer _consumer;
        private static OrderConsumer _orderconsumer;
        private static OrderProducer _orderproducer;
        private static MyMsgListener _listen;
        private static object s_SyncLock = new Object();
        private static string Ons_Topic = "linuxmsgtrace";
        private static string Ons_ProducerID = "PID_msgtracelinux";
        private static string Ons_AccessKey = "vgo1Qta8XpptpG3O";
        private static string Ons_SecretKey = "Nx8HKLYj3Bicykqs3RgGmLZQAgtlYq";
        private static string Ons_ConsumerId = "CID_TESTLINUX";

        public static void SendMessage(byte[] msgBody) {
            Message msg = new Message(Ons_Topic, "", "");
            msg.setBody(msgBody, msgBody.Length);
            try
            {
                SendResultONS sendResult = _producer.send(msg);
                Console.WriteLine("send success {0}", sendResult.getMessageId());
            }
            catch (Exception ex)
            {
                Console.WriteLine("send failure{0}", ex.ToString());
            }
        }
        public static void SendMessage(string msgBody, String tag = "RegisterLog")
        {
            // Message msg = new Message(Ons_Topic, tag, msgBody);
            Message msg = new Message(Ons_Topic, tag, msgBody);
            msg.setKey(Guid.NewGuid().ToString());
            try
            {
                SendResultONS sendResult = _producer.send(msg);
                Console.WriteLine("send success {0}", sendResult.getMessageId());
            }
            catch (Exception ex)
            {
                Console.WriteLine("send failure{0}", ex.ToString());
            }
        }

        public static void SendOrderMessage(string msgBody, String tag = "RegisterLog", String key = "test")
        {
            Message msg = new Message(Ons_Topic, tag, msgBody);
            byte[] data = new byte[10];
            msg.setBody(data, 10);
            msg.setKey(Guid.NewGuid().ToString());
            try
            {
                SendResultONS sendResult = _orderproducer.send(msg, key);
                Console.WriteLine("send success {0}", sendResult.getMessageId());
            }
            catch (Exception ex)
            {
                Console.WriteLine("send failure{0}", ex.ToString());
            }
        }

        public static void StartPushConsumer()
        {
            MyMsgListener listen = new MyMsgListener();
            _consumer.subscribe(Ons_Topic, "*", listen);
            _consumer.start();
        }

        public static void StartOrderConsumer()
        {
            MyMsgOrderListener listen = new MyMsgOrderListener();
            _orderconsumer.subscribe(Ons_Topic, "*", listen);
            _orderconsumer.start();
        }

        public static void shutdownPushConsumer()
        {
            _consumer.shutdown();
        }

        public static void shutdownOrderConsumer()
        {
            _orderconsumer.shutdown();
        }

        public static void StartProducer()
        {
            _producer.start();
        }

        public static void ShutdownProducer()
        {
            _producer.shutdown();
        }


        public static void StartOrderProducer()
        {
            _orderproducer.start();
        }

        public static void ShutdownOrderProducer()
        {
            _orderproducer.shutdown();
        }

        private static ONSFactoryProperty getFactoryProperty()
        {
            ONSFactoryProperty factoryInfo = new ONSFactoryProperty();
            factoryInfo.setFactoryProperty(ONSFactoryProperty.AccessKey, Ons_AccessKey);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.SecretKey, Ons_SecretKey);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.ConsumerId, Ons_ConsumerId);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.ProducerId, Ons_ProducerID);
            factoryInfo.setFactoryProperty(ONSFactoryProperty.PublishTopics, Ons_Topic);
           return factoryInfo;
        }

        public static void  CreatePushConsumer()
        {

            _consumer = ONSFactory.getInstance().createPushConsumer(getFactoryProperty());
        }

        public static void CreateProducer()
        {

            _producer = ONSFactory.getInstance().createProducer(getFactoryProperty());
        }


        public static void CreateOrderConsumer()
        {

            _orderconsumer = ONSFactory.getInstance().createOrderConsumer(getFactoryProperty());
        }

        public static void CreateOrderProducer()
        {

            _orderproducer = ONSFactory.getInstance().createOrderProducer(getFactoryProperty());
        }
    }




    class ConsumerAndProducerForEx
    {
        static void Main(string[] args)
        {
            OnscSharp.CreateProducer();
            OnscSharp.CreatePushConsumer();
            OnscSharp.StartPushConsumer();
            OnscSharp.StartProducer();
            System.DateTime beforDt = System.DateTime.Now;
            byte[] data = Encoding.UTF8.GetBytes("     万会觉得  哎哎啊啊啊   啊哎啊啊啊 cds ");
            // string body = Encoding.Default.GetString(data);
            // string body = "     万会觉得  哎哎啊啊啊   啊哎啊啊啊 cds ";
            for (int i = 0;i < 2; ++i)
            {
                OnscSharp.SendMessage(data);
            }
            System.DateTime endDt = System.DateTime.Now;
            System.TimeSpan ts = endDt.Subtract(beforDt);
            Console.WriteLine("per message:{0}ms.", ts.TotalMilliseconds / 10000);
            Thread.Sleep(1000 * 100);
            OnscSharp.ShutdownProducer();
            OnscSharp.shutdownPushConsumer();
        }
    }
}
