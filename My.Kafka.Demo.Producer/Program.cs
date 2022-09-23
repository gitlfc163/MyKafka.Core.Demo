
using My.Kafka.Core.Demo;

KafkaService.KAFKA_SERVERS = "kafka1:9091,kafka2:9092,kafka3:9093";
var kafkaService = new KafkaService();
for (int i = 0; i < 50; i++)
{
    var eventData = new EventData
    {
        TopicName = "testtopic",
        Message = $"This is a message from Producer, Index : {i + 1}",
        EventTime = DateTime.Now
    };
    await kafkaService.PublishAsync<EventData>(eventData.TopicName, eventData);
}
Console.WriteLine("发布结束!");
Console.ReadKey();