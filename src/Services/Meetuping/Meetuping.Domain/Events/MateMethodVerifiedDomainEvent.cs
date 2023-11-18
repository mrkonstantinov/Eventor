using Meetuping.Domain.AggregatesModel.MeetupAggregate;

namespace Eventor.Services.Meetuping.Domain.Events;

public class MateMethodVerifiedDomainEvent
    : INotification
{
    public Mate Mate { get; private set; }
    public int MeetupId { get; private set; }

    public MateMethodVerifiedDomainEvent(Mate mate, int meetupId)
    {
        Mate = mate;
        MeetupId = meetupId;
    }
}
