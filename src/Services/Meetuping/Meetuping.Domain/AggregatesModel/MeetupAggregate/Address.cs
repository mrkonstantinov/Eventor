using Eventor.Services.Meetuping.Domain.SeedWork;
using System.IO;

namespace Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string Region { get; private set; }

    public Address() { }

    public Address(string street, string city, string region)
    {
        Street = street;
        City = city;
        Region = region;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return City;
        yield return Region;
    }
}
