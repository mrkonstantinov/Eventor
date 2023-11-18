namespace Eventor.Services.Meetuping.API.Application.Queries;

public record Mate
{
    public string name { get; init; }
    public int age { get; init; }
    public int gender { get; init; }
    public string pictureurl { get; init; }
}

public record Meetup
{
    public DateTime date { get; init; }
    public string status { get; init; }
    public string description { get; init; }
    public string street { get; init; }
    public string city { get; init; }
    public string region { get; init; }
    public List<Mate> mates { get; set; }
    public List<Tag> tags { get; set; }
}

public record MeetupSummary
{
    public int ordernumber { get; init; }
    public DateTime date { get; init; }
    public string status { get; init; }
    public double total { get; init; }
}

public record Tag
{
    public string value { get; init; }
}
