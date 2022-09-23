using My.Kafka.Core.Demo;

KafkaService.KAFKA_SERVERS = "kafka1:9091,kafka2:9092,kafka3:9093";
var kafkaService = new KafkaService();
var topics = new List<string> { "testtopic" };
await kafkaService.SubscribeAsync<EventData>(topics, (eventData) =>
{
    Console.WriteLine($" - {eventData.EventTime: yyyy-MM-dd HH:mm:ss} 【{eventData.TopicName}】- > 已处理");
});