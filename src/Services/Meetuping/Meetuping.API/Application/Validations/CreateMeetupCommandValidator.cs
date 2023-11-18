using FluentValidation;

namespace Eventor.Services.Meetuping.API.Application.Validations;
public class CreateMeetupCommandValidator : AbstractValidator<CreateMeetupCommand>
{
    public CreateMeetupCommandValidator(ILogger<CreateMeetupCommandValidator> logger)
    {
        RuleFor(command => command.City).NotEmpty();
        RuleFor(command => command.Street).NotEmpty();
        RuleFor(command => command.Region).NotEmpty();
        //RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date");
        RuleFor(command => command.Tags).Must(ContainTagItems).WithMessage("No order items found");
        logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
    }

    private bool BeValidExpirationDate(DateTime dateTime)
    {
        return dateTime >= DateTime.UtcNow;
    }

    private bool ContainTagItems(IEnumerable<TagItemDTO> tagItems)
    {
        return tagItems.Any();
    }
}
