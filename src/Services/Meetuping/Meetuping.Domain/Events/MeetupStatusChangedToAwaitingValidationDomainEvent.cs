using Meetuping.Domain.AggregatesModel.MeetupAggregate;

namespace Eventor.Services.Meetuping.Domain.Events;

/// <summary>
/// Event used when the grace period order is confirmed
/// </summary>
public class MeetupStatusChangedToAwaitingValidationDomainEvent
        : INotification
{
    public int MeetupId { get; }
    public IEnumerable<Mate> Mates { get; }

    public MeetupStatusChangedToAwaitingValidationDomainEvent(int meetupId,
        IEnumerable<Mate> mates)
    {
        MeetupId = meetupId;
        Mates = mates;
    }
}
