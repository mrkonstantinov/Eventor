namespace Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;

//This is just the RepositoryContracts or Interface defined at the Domain Layer
//as requisite for the Order Aggregate

public interface IMeetupRepository : IRepository<Meetup>
{
    Meetup Add(Meetup meetup);

    void Update(Meetup meetup);

    Task<Meetup> GetAsync(int meetupId);
}
