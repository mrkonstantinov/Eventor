namespace Eventor.Services.Meetuping.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class MeetupingDomainException : Exception
{
    public MeetupingDomainException()
    { }

    public MeetupingDomainException(string message)
        : base(message)
    { }

    public MeetupingDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
