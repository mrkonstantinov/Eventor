namespace Eventor.Services.Meetuping.API.Extensions;

public static class TagExtensions
{
    public static IEnumerable<TagItemDTO> ToTagItemsDTO(this IEnumerable<TagItem> tags)
    {
        foreach (var item in tags)
        {
            yield return item.ToTagItemDTO();
        }
    }

    public static TagItemDTO ToTagItemDTO(this TagItem item)
    {
        return new TagItemDTO()
        {
            Value = item.Value
        };
    }
}
