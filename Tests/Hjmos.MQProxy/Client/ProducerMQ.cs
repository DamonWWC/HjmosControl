using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.MQProxy
{
    //public class ProducerMQ : MQBase
    //{
    //    Producer _producer;
    //    public string ProducerGroupID { get; set; } = "*";
    //    public override bool Start()
    //    {
    //        try
    //        {
    //            ONSFactoryProperty oNSFactoryProperty = getFactoryProperty();
    //            oNSFactoryProperty.setFactoryProperty(ONSFactoryProperty.ProducerId, ProducerGroupID);
    //            _producer = ONSFactory.getInstance().createProducer(oNSFactoryProperty);
    //            _producer.start();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }

    //    public override void Stop()
    //    {
    //        if (_producer != null)
    //            _producer.shutdown();
    //    }
    //    public void SendMessage(object msgBody, string topic, String tag = "*")
    //    {
    //        if (!(msgBody is String str)) str = JsonNewtonsoft.ToJSON(msgBody);
    //        Message msg = new Message(topic, tag, str);
    //        msg.setKey(Guid.NewGuid().ToString());
    //        try
    //        {
    //            SendResultONS sendResult = _producer.send(msg);      
    //        }
    //        catch (Exception ex)
    //        {
               
    //        }
    //        finally
    //        {

    //        }
    //    }

    //}
}
