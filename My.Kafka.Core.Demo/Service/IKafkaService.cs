namespace My.Kafka.Core.Demo;

public interface IKafkaService
{
    /// <summary>
    /// 发布
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="topicName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task PublishAsync<T>(string topicName, T message) where T : class;

    /// <summary>
    /// 订阅
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="topics"></param>
    /// <param name="messageFunc"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SubscribeAsync<T>(IEnumerable<string> topics, Action<T> messageFunc, CancellationToken cancellationToken = default) where T : class;
}
