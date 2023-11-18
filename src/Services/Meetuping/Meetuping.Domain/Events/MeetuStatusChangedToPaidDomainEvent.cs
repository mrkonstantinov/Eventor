using Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;
using Meetuping.Domain.AggregatesModel.MeetupAggregate;

namespace Eventor.Services.Meetuping.Domain.Events;

/// <summary>
/// Event used when the order is paid
/// </summary>
public class MeetuStatusChangedToPaidDomainEvent
    : INotification
{
    public int MeetupId { get; }
    public IEnumerable<Mate> Mates { get; }

    public MeetuStatusChangedToPaidDomainEvent(int meetupId,
        IEnumerable<Mate> mates)
    {
        MeetupId = meetupId;
        Mates = mates;
    }
}
