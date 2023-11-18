namespace Eventor.Services.Meetuping.API.Application.IntegrationEvents;

public interface IMeetupingIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}
