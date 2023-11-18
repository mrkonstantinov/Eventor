using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Meetuping.Domain.AggregatesModel.MeetupAggregate;

public class Mate
    : Entity
{
    // DDD Patterns comment
    // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
    // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
    private string _mateName;
    private int? _age;
    // 0 - Unknown
    // 1 - Male
    // 2 - Female
    private int? _gender;
    private string _pictureUrl;
    private DateTime? _approvalAt;

    public Guid UserId { get; private set; }

    protected Mate()
    {
    }

    public Mate(Guid userId, string mateName, int? age, int? gender, string PictureUrl) : this()
    {
        UserId = userId;

        _mateName = !string.IsNullOrWhiteSpace(mateName) ? mateName : throw new ArgumentNullException(nameof(mateName));
        _age = age;
        _gender = gender;
        _pictureUrl = PictureUrl;
    }

    public string GetPictureUri() => _pictureUrl;

    public DateTime? GetMateApprovalAt()
    {
        return _approvalAt;
    }

    public int? GetAge()
    {
        return _age;
    }

    public int? GetGender()
    {
        return _gender;
    }

    public string GetMeetupMateName() => _mateName;
}
