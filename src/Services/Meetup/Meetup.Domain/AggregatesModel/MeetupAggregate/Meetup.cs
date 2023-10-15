namespace Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;

public class Meetup
    : Entity, IAggregateRoot
{
    // DDD Patterns comment
    // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
    // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
    private DateTime _meetupDate;

    // Address is a Value Object pattern example persisted as EF Core 2.0 owned entity
    public Address Address { get; private set; }

    public int? GetBuyerId => _buyerId;
    private int? _buyerId;

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
    private readonly List<MeetupItem> _meetupItems;
    public IReadOnlyCollection<MeetupItem> MeetupItems => _meetupItems;

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
        _meetupItems = new List<MeetupItem>();
        _isDraft = false;
    }

    public Meetup(string userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber,
            string cardHolderName, DateTime cardExpiration, int? buyerId = null) : this()
    {
        _buyerId = buyerId;
        _meetupStatusId = MeetupStatus.Submitted.Id;
        _meetupDate = DateTime.UtcNow;
        Address = address;

        // Add the OrderStarterDomainEvent to the domain events collection 
        // to be raised/dispatched when committing changes into the Database [ After DbContext.SaveChanges() ]
        AddMeetupStartedDomainEvent(userId, userName, cardTypeId, cardNumber,
                                    cardSecurityNumber, cardHolderName, cardExpiration);
    }

    // DDD Patterns comment
    // This Order AggregateRoot's method "AddOrderItem()" should be the only way to add Items to the Order,
    // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
    // in order to maintain consistency between the whole Aggregate. 
    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
    {
        var existingOrderForProduct = _meetupItems.Where(o => o.ProductId == productId)
            .SingleOrDefault();

        if (existingOrderForProduct != null)
        {
            //if previous line exist modify it with higher discount  and units..

            if (discount > existingOrderForProduct.GetCurrentDiscount())
            {
                existingOrderForProduct.SetNewDiscount(discount);
            }

            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            //add validated new order item

            var orderItem = new MeetupItem(productId, productName, unitPrice, discount, pictureUrl, units);
            _meetupItems.Add(orderItem);
        }
    }

    public void SetBuyerId(int id)
    {
        _buyerId = id;
    }

    public void SetAwaitingValidationStatus()
    {
        if (_meetupStatusId == MeetupStatus.Submitted.Id)
        {
            AddDomainEvent(new MeetupStatusChangedToAwaitingValidationDomainEvent(Id, _meetupItems));
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
            AddDomainEvent(new MeetuStatusChangedToPaidDomainEvent(Id, MeetupItems));

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

    public void SetCancelledStatusWhenStockIsRejected(IEnumerable<int> orderStockRejectedItems)
    {
        if (_meetupStatusId == MeetupStatus.AwaitingValidation.Id)
        {
            _meetupStatusId = MeetupStatus.Cancelled.Id;

            var itemsStockRejectedProductNames = MeetupItems
                .Where(c => orderStockRejectedItems.Contains(c.ProductId))
                .Select(c => c.GetOrderItemProductName());

            var itemsStockRejectedDescription = string.Join(", ", itemsStockRejectedProductNames);
            _description = $"The product items don't have stock: ({itemsStockRejectedDescription}).";
        }
    }

    private void AddMeetupStartedDomainEvent(string userId, string userName, int cardTypeId, string cardNumber,
            string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
    {
        var meetupStartedDomainEvent = new MeetupStartedDomainEvent(this, userId, userName, cardTypeId,
                                                                    cardNumber, cardSecurityNumber,
                                                                    cardHolderName, cardExpiration);

        this.AddDomainEvent(meetupStartedDomainEvent);
    }

    private void StatusChangeException(MeetupStatus orderStatusToChange)
    {
        throw new MeetupDomainException($"Is not possible to change the order status from {MeetupStatus.Name} to {orderStatusToChange.Name}.");
    }

    public decimal GetTotal()
    {
        return _meetupItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
    }
}
