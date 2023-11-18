
using System.ComponentModel.DataAnnotations;

namespace Eventor.Services.Meetuping.Domain.Events
{
    /// <summary>
    /// Event used when an order is created
    /// </summary>
    public class MeetupCreatedDomainEvent : INotification
    {
        public Guid OwnerId { get; }
        public string OwnerName { get; }
        public int? Age { get; }
        public int? Gender { get; }
        public Meetup Meetup { get; }

        public MeetupCreatedDomainEvent(Meetup meetup, Guid ownerId, string ownerName, int? age, int? gender)
        {
            Meetup = meetup;
            OwnerId = ownerId;
            OwnerName = ownerName;
            Age = age;
            Gender = gender;
        }
    }
}