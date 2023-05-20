using Azure;
using Azure.Messaging.EventGrid;

namespace Tradmia.ApiTemplate.Infrastructure.Events;

public class EventGridPublisherService : IEventPublisher
{
    private readonly Uri _endpoint;
    private readonly AzureKeyCredential _credential;
    private EventGridPublisherClient _client;

    public EventGridPublisherService(string endpoint, string key)
    {
        _endpoint = new Uri(endpoint);
        _credential = new AzureKeyCredential(key);
        _client = new EventGridPublisherClient(_endpoint, _credential);
    }

    public async Task PublishEvent(string eventType, string subject, object data)
    {
        EventGridEvent eventGridEvent = new EventGridEvent(
            subject: subject,
            eventType: eventType,
            dataVersion: "1.0",
            data: new BinaryData(data));

        await _client.SendEventsAsync(new[] { eventGridEvent });
    }
}

