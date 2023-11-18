namespace Eventor.Services.Meetuping.API.Application.DomainEventHandlers;

internal static partial class MeetupingApiTrace
{
    [LoggerMessage(EventId = 1, EventName = "MeetupStatusUpdated", Level = LogLevel.Trace, Message = "Meetup with Id: {MeetupId} has been successfully updated to status {Status} ({Id})")]
    public static partial void LogMeetupStatusUpdated(ILogger logger, int meetupId, string status, int id);

    [LoggerMessage(EventId = 2, EventName = "PaymentMethodUpdated", Level = LogLevel.Trace, Message = "Meetup with Id: {MeetupId} has been successfully updated with a payment method {PaymentMethod} ({Id})")]
    public static partial void LogMeetupPaymentMethodUpdated(ILogger logger, int meetupId, string paymentMethod, int id);

    [LoggerMessage(EventId = 3, EventName = "BuyerAndPaymentValidatedOrUpdated", Level = LogLevel.Trace, Message = "Buyer {BuyerId} and related payment method were validated or updated for meetup Id: {MeetupId}.")]
    public static partial void LogMeetupBuyerAndPaymentValidatedOrUpdated(ILogger logger, int buyerId, int meetupId);
}
