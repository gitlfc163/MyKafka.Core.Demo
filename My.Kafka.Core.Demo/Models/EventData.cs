namespace My.Kafka.Core.Demo;

public class EventData
{
    public string TopicName { get; set; }

    public string Message { get; set; }

    public DateTime EventTime { get; set; }
}