namespace Eventor.Services.Meetuping.Domain.Events;

public class ParticipantMethodVerifiedDomainEvent
    : INotification
{
    public Participant Participant { get; private set; }
    public int MeetupId { get; private set; }

    public ParticipantMethodVerifiedDomainEvent(Participant participant, int meetupId)
    {
        Participant = participant;
        MeetupId = meetupId;
    }
}
