namespace Eventor.Services.Meetuping.Domain.Events;

public class MeetupCancelledDomainEvent : INotification
{
    public Meetup Meetup { get; }

    public MeetupCancelledDomainEvent(Meetup meetup)
    {
        Meetup = meetup;
    }
}

