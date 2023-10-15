
namespace Eventor.Services.Meetuping.Domain.Events
{
    /// <summary>
    /// Event used when an order is created
    /// </summary>
    public class MeetupStartedDomainEvent : INotification
    {
        public string UserId { get; }
        public string UserName { get; }
        public int CardTypeId { get; }
        public string CardNumber { get; }
        public string CardSecurityNumber { get; }
        public string CardHolderName { get; }
        public DateTime CardExpiration { get; }
        public Meetup Meetup { get; }

        public MeetupStartedDomainEvent(Meetup meetup, string userId, string userName,
                                       int cardTypeId, string cardNumber,
                                       string cardSecurityNumber, string cardHolderName,
                                       DateTime cardExpiration)
        {
            Meetup = meetup;
            UserId = userId;
            UserName = userName;
            CardTypeId = cardTypeId;
            CardNumber = cardNumber;
            CardSecurityNumber = cardSecurityNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
        }
    }
}
