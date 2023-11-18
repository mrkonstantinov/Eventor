namespace Eventor.Services.Meetuping.API.Application.Queries;

public interface IMeetupQueries
{
    Task<Meetup> GetMeetupAsync(int id);

    Task<IEnumerable<Meetup>> GetMeetupsFromUserAsync(Guid userId);
}
