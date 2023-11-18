namespace Eventor.Services.Meetuping.Domain.Exceptions;

public class InvalidResourceTagsException : Exception
{
    public InvalidResourceTagsException() : base("Meetup tags are invalid.")
    { }

    public InvalidResourceTagsException(string message)
        : base(message)
    { }

    public InvalidResourceTagsException(string message, Exception innerException)
    : base(message, innerException)
    { }
}