using Meetuping.Domain.AggregatesModel.MeetupAggregate;
using System.Net;

namespace Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;

public class Meetup
    : Entity, IAggregateRoot
{    
    //public List<Tag> Tags { get; set; } = new();
    // DDD Patterns comment
    // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
    // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
    private DateTime _meetupDate;
    private string _ownerName;
    private int? _age;
    private int? _gender;
    private string _ownerPhotoUrl;

    // Address is a Value Object pattern example persisted as EF Core 2.0 owned entity
    public Address Address { get; private set; }

    public Guid GetOwnerId => _ownerId;
    private Guid _ownerId;

    public MeetupStatus MeetupStatus { get; private set; }
    private int _meetupStatusId;

    private string _description;

    // Draft orders have this set to true. Currently we don't check anywhere the draft status of an Order, but we could do it if needed
#pragma warning disable CS0414 // The field 'Order._isDraft' is assigned but its value is never used
    private bool _isDraft;
#pragma warning restore CS0414

    // DDD Patterns comment
    // Using a private collection field, better for DDD Aggregate's encapsulation
    // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
    // but only through the method OrderAggregateRoot.AddOrderItem() which includes behavior.
    private readonly List<Mate> _mates;
    public IReadOnlyCollection<Mate> Mates => _mates;

    //public List<Tag> Tags { get; } = new();

    public static Meetup NewDraft()
    {
        var meetup = new Meetup
        {
            _isDraft = true
        };
        return meetup;
    }

    protected Meetup()
    {
        _mates = new List<Mate>();
        _isDraft = false;
    }

    public Meetup(Guid userId, string ownerName, int? age, int? gender, string ownerPhotoUrl, DateTime meetupDate, Address address, IEnumerable<string> tags) : this()
    {
        ValidateTags(tags);
        _ownerId = userId;
        _ownerName = ownerName;
        _age = age;
        _gender = gender;
        _ownerPhotoUrl = ownerPhotoUrl;
        _meetupDate = meetupDate;

        _meetupStatusId = MeetupStatus.Submitted.Id;        
        Address = address;

        foreach (var tag in tags)
        {
            AddTag(new Tag() { Value = tag});
        }

        // Add the MeetupCreatedDomainEvent to the domain events collection 
        // to be raised/dispatched when committing changes into the Database [ After DbContext.SaveChanges() ]
        AddMeetupCreatedDomainEvent(userId, ownerName, age, gender);
    }

    // DDD Patterns comment
    // This Order AggregateRoot's method "AddOrderItem()" should be the only way to add Items to the Order,
    // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
    // in order to maintain consistency between the whole Aggregate. 
    public void AddMate(Guid userId, string name, int age, int gender, string pictureUrl)
    {
        var existingMate = _mates.Where(o => o.UserId == userId)
            .SingleOrDefault();

        if (existingMate != null)
        {
            
        }
        else
        {
            //add validated new order item

            var mate = new Mate(userId, name, age, gender, pictureUrl);
            _mates.Add(mate);
        }
    }

    public void SetOwnerId(Guid id)
    {
        _ownerId = id;
    }

    public void AddTag(Tag tag)
    {
        if (tag != null)
        {
            Metadata.Tags.Add(tag);
        }
        else
        {

        }
    }

    public void SetAwaitingValidationStatus()
    {
        if (_meetupStatusId == MeetupStatus.Submitted.Id)
        {
            AddDomainEvent(new MeetupStatusChangedToAwaitingValidationDomainEvent(Id, _mates));
            _meetupStatusId = MeetupStatus.AwaitingValidation.Id;
        }
    }

    public void SetStockConfirmedStatus()
    {
        if (_meetupStatusId == MeetupStatus.AwaitingValidation.Id)
        {
            AddDomainEvent(new MeetupStatusChangedToStockConfirmedDomainEvent(Id));

            _meetupStatusId = MeetupStatus.StockConfirmed.Id;
            _description = "All the items were confirmed with available stock.";
        }
    }

    public void SetPaidStatus()
    {
        if (_meetupStatusId == MeetupStatus.StockConfirmed.Id)
        {
            AddDomainEvent(new MeetuStatusChangedToPaidDomainEvent(Id, Mates));

            _meetupStatusId = MeetupStatus.Paid.Id;
            _description = "The payment was performed at a simulated \"American Bank checking bank account ending on XX35071\"";
        }
    }

    public void SetShippedStatus()
    {
        if (_meetupStatusId != MeetupStatus.Paid.Id)
        {
            StatusChangeException(MeetupStatus.Shipped);
        }

        _meetupStatusId = MeetupStatus.Shipped.Id;
        _description = "The order was shipped.";
        AddDomainEvent(new MeetupShippedDomainEvent(this));
    }

    public void SetCancelledStatus()
    {
        if (_meetupStatusId == MeetupStatus.Paid.Id ||
            _meetupStatusId == MeetupStatus.Shipped.Id)
        {
            StatusChangeException(MeetupStatus.Cancelled);
        }

        _meetupStatusId = MeetupStatus.Cancelled.Id;
        _description = $"The order was cancelled.";
        AddDomainEvent(new MeetupCancelledDomainEvent(this));
    }

    //public void SetCancelledStatusWhenStockIsRejected(IEnumerable<int> orderStockRejectedItems)
    //{
    //    if (_meetupStatusId == MeetupStatus.AwaitingValidation.Id)
    //    {
    //        _meetupStatusId = MeetupStatus.Cancelled.Id;

    //        var itemsStockRejectedProductNames = Participants
    //            .Where(c => orderStockRejectedItems.Contains(c.ProductId))
    //            .Select(c => c.GetOrderItemProductName());

    //        var itemsStockRejectedDescription = string.Join(", ", itemsStockRejectedProductNames);
    //        _description = $"The product items don't have stock: ({itemsStockRejectedDescription}).";
    //    }
    //}

    private void AddMeetupCreatedDomainEvent(Guid ownerId, string ownerName, int? age, int? gender)
    {
        var meetupCreatedDomainEvent = new MeetupCreatedDomainEvent(this, ownerId, ownerName, age, gender);

        this.AddDomainEvent(meetupCreatedDomainEvent);
    }

    private void StatusChangeException(MeetupStatus meetupStatusToChange)
    {
        throw new MeetupingDomainException($"Is not possible to change the order status from {MeetupStatus.Name} to {meetupStatusToChange.Name}.");
    }

    //public decimal GetTotal()
    //{
    //    return _participants.Sum(o => o.GetUnits() * o.GetUnitPrice());
    //}

    private static void ValidateTags(IEnumerable<string> tags)
    {
        if (tags is null || !tags.Any())
        {
            throw new MissingResourceTagsException();
        }

        if (tags.Any(string.IsNullOrWhiteSpace))
        {
            throw new InvalidResourceTagsException();
        }
    }

    public PostMetadata? Metadata { get; } = new();
}


#region PostMetadataAggregate
public class PostMetadata
{
    public List<Tag> Tags { get; } = new();
}

public class Tag
{
    public string Value { get; set; }
    public static implicit operator string(Tag tag) => tag.Value;
    public static implicit operator Tag(string tag) => new() { Value = tag };
}

#endregion
