// See https://aka.ms/new-console-template for more information

using Hjmos.MQProxy;

var consumer = ConnectionFactory.PushConsumer(new RocketMQPara
{
    //Topic= "device_systemAlarmCount",
    NameServerAddress = "10.51.9.130:30899",
    ConsumerGroupID = "Holoception"
});
consumer.Subscribe("emergency-command", "*");
consumer.OnConsume = Receive;
consumer.Start();
Console.WriteLine("Hello, World!");
Console.ReadLine();

static void Receive(IMessage message)
{
}