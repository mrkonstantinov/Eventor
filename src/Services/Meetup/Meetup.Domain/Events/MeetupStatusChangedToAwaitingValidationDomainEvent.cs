﻿namespace Eventor.Services.Meetuping.Domain.Events;

/// <summary>
/// Event used when the grace period order is confirmed
/// </summary>
public class MeetupStatusChangedToAwaitingValidationDomainEvent
        : INotification
{
    public int MeetupId { get; }
    public IEnumerable<MeetupItem> MeetupItems { get; }

    public MeetupStatusChangedToAwaitingValidationDomainEvent(int meetupId,
        IEnumerable<MeetupItem> meetupItems)
    {
        MeetupId = meetupId;
        MeetupItems = meetupItems;
    }
}
