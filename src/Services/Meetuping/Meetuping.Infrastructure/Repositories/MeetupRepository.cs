namespace Eventor.Services.Meetuping.Infrastructure.Repositories;

public class MeetupRepository
    : IMeetupRepository
{
    private readonly MeetupingContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public MeetupRepository(MeetupingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Meetup Add(Meetup meetup)
    {
        return _context.Meetups.Add(meetup).Entity;

    }

    public async Task<Meetup> GetAsync(int meetupId)
    {
        var meetup = await _context
                            .Meetups
                            .Include(x => x.Address)
                            .FirstOrDefaultAsync(o => o.Id == meetupId);
        if (meetup == null)
        {
            meetup = _context
                        .Meetups
                        .Local
                        .FirstOrDefault(o => o.Id == meetupId);
        }
        if (meetup != null)
        {
            await _context.Entry(meetup)
                .Collection(i => i.Mates).LoadAsync();
            await _context.Entry(meetup)
                .Reference(i => i.MeetupStatus).LoadAsync();
        }

        return meetup;
    }

    public void Update(Meetup meetup)
    {
        _context.Entry(meetup).State = EntityState.Modified;
    }
}
