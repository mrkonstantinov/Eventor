namespace Eventor.Services.Meetuping.Domain.Events;
/// <summary>
/// Event used when the order stock items are confirmed
/// </summary>
public class MeetupStatusChangedToStockConfirmedDomainEvent
    : INotification
{
    public int OrderId { get; }

    public MeetupStatusChangedToStockConfirmedDomainEvent(int orderId)
        => OrderId = orderId;
}
