namespace Eventor.Services.Meetuping.Domain.AggregatesModel.ParticipantAggregate;

public class Participant
    : Entity, IAggregateRoot
{
    public string IdentityGuid { get; private set; }

    public string Name { get; private set; }

    protected Participant()
    {
    }

    public Participant(string identity, string name) : this()
    {
        IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    }
}
