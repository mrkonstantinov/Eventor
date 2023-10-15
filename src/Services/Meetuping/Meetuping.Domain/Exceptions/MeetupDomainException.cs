namespace Eventor.Services.Meetuping.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class MeetupDomainException : Exception
{
    public MeetupDomainException()
    { }

    public MeetupDomainException(string message)
        : base(message)
    { }

    public MeetupDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
