namespace Eventor.Services.Meetuping.Domain.AggregatesModel.ParticipantAggregate;

//This is just the RepositoryContracts or Interface defined at the Domain Layer
//as requisite for the Participant Aggregate

public interface ParticipantRepository : IRepository<Participant>
{
    Participant Add(Participant participant);
    Participant Update(Participant participant);
    Task<Participant> FindAsync(string ParticipantIdentityGuid);
    Task<Participant> FindByIdAsync(string id);
}

