namespace Eventor.Services.Meetuping.API.Application.Commands;

// DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
// In this case, its immutability is achieved by having all the setters as private
// plus only being able to update the data just once, when creating the object through its constructor.
// References on Immutable Commands:  
// http://cqrs.nu/Faq
// https://docs.spine3.org/motivation/immutability.html 
// http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
// https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-a-lightweight-class-with-auto-implemented-properties

using Eventor.Services.Meetuping.API.Application.Models;
using Eventor.Services.Meetuping.API.Extensions;

[DataContract]
public class CreateMeetupCommand
    : IRequest<bool>
{
    [DataMember]
    private readonly List<TagItemDTO> _tags;

    [DataMember]
    public Guid OwnerId { get; private set; }

    [DataMember]
    public string OwnerName { get; private set; }

    [DataMember]
    public int Age { get; private set; }

    [DataMember]
    public int Gender { get; private set; }

    [DataMember]
    public DateTime MeetupDate { get; private set; }

    [DataMember]
    public string City { get; private set; }

    [DataMember]
    public string Street { get; private set; }

    [DataMember]
    public string Region { get; private set; }
  
    [DataMember]
    public IEnumerable<TagItemDTO> Tags => _tags;

    public CreateMeetupCommand()
    {
        _tags = new List<TagItemDTO>();
    }

    public CreateMeetupCommand(List<TagItem> tags, Guid ownerId, string ownerName, int age, int gender, DateTime meetupDate, string city, string street, string region) : this()
    {
        _tags = tags.ToTagItemsDTO().ToList();
        OwnerId = ownerId;
        OwnerName = ownerName;
        Age = age;
        Gender  = gender;

        MeetupDate = meetupDate;

        City = city;
        Street = street;
        Region = region;
    }
}

