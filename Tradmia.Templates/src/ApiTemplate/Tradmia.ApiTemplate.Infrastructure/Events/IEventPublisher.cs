namespace Tradmia.ApiTemplate.Infrastructure.Events
{
    public interface IEventPublisher
    {
        Task PublishEvent(string eventType, string subject, object data);
    }
}
