using Eventor.Services.Meetuping.Domain.SeedWork;

namespace Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;

public class MeetupStatus
    : Enumeration
{
    public static MeetupStatus Submitted = new MeetupStatus(1, nameof(Submitted).ToLowerInvariant());
    public static MeetupStatus AwaitingValidation = new MeetupStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
    public static MeetupStatus StockConfirmed = new MeetupStatus(3, nameof(StockConfirmed).ToLowerInvariant());
    public static MeetupStatus Paid = new MeetupStatus(4, nameof(Paid).ToLowerInvariant());
    public static MeetupStatus Shipped = new MeetupStatus(5, nameof(Shipped).ToLowerInvariant());
    public static MeetupStatus Cancelled = new MeetupStatus(6, nameof(Cancelled).ToLowerInvariant());

    public MeetupStatus(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<MeetupStatus> List() =>
        new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };

    public static MeetupStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new MeetupingDomainException($"Possible values for MeetupStatus: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static MeetupStatus From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new MeetupingDomainException($"Possible values for MeetupStatus: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}
