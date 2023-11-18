namespace Eventor.Services.Meetuping.Domain.Exceptions;

public class MissingResourceTagsException : Exception
{
    public MissingResourceTagsException() : base("Meetup tags are missing.")
    { }

    public MissingResourceTagsException(string message)
        : base(message)
    { }

    public MissingResourceTagsException(string message, Exception innerException)
    : base(message, innerException)
    { }
}
