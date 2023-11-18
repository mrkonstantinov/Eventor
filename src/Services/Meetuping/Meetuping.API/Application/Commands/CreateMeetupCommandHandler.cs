namespace Eventor.Services.Meetuping.API.Application.Commands;

using Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;
using Eventor.Services.Meetupingg.API.Application.Commands;

// Regular CommandHandler
public class CreateMeetupCommandHandler
    : IRequestHandler<CreateMeetupCommand, bool>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly IMeetupingIntegrationEventService _meetupingIntegrationEventService;
    private readonly ILogger<CreateMeetupCommandHandler> _logger;

    // Using DI to inject infrastructure persistence Repositories
    public CreateMeetupCommandHandler(IMediator mediator,
        IMeetupingIntegrationEventService meetupingIntegrationEventService,
        IMeetupRepository meetupRepository,
        IIdentityService identityService,
        ILogger<CreateMeetupCommandHandler> logger)
    {
        _meetupRepository = meetupRepository ?? throw new ArgumentNullException(nameof(meetupRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _meetupingIntegrationEventService = meetupingIntegrationEventService ?? throw new ArgumentNullException(nameof(meetupingIntegrationEventService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(CreateMeetupCommand message, CancellationToken cancellationToken)
    {
        // Add Integration event to clean the basket
        var meetupCreatedIntegrationEvent = new MeetupCreatedIntegrationEvent(message.OwnerId);
        await _meetupingIntegrationEventService.AddAndSaveEventAsync(meetupCreatedIntegrationEvent);

        // Add/Update the Buyer AggregateRoot
        // DDD patterns comment: Add child entities and value-objects through the Order Aggregate-Root
        // methods and constructor so validations, invariants and business logic 
        // make sure that consistency is preserved across the whole aggregate
        var address = new Address(message.Street, message.City, message.Region);
        var meetup = new Meetup(message.OwnerId, message.OwnerName, message.Age, message.Gender, message.MeetupDate, address, null);

        //foreach (var item in message.Tags)
        //{
        //    meetup.AddTag(item);
        //}

        _logger.LogInformation("Creating Order - Order: {@Order}", meetup);

        _meetupRepository.Add(meetup);

        return await _meetupRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}


// Use for Idempotency in Command process
public class CreateOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CreateMeetupCommand, bool>
{
    public CreateOrderIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CreateMeetupCommand, bool>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true; // Ignore duplicate requests for creating order.
    }
}

public record TagItemDTO
{
    public string Value { get; init; }
}