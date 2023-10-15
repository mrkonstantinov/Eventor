namespace Eventor.Services.Meetuping.Domain.Events;

public class MeetupShippedDomainEvent : INotification
{
    public Meetup Meetup { get; }

    public MeetupShippedDomainEvent(Meetup meetup)
    {
        Meetup = meetup;
    }
}
