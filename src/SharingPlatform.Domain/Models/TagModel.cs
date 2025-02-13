namespace SharingPlatform.Domain.Models;

public sealed class TagModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;

    public static TagModel Create(string name)
    {
        return new TagModel
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
    
    public static TagModel Create(Guid id, string name)
    {
        return new TagModel
        {
            Id = id,
            Name = name
        };
    }

    public void Update(TagModel tag)
    {
        Name = tag.Name;
    }
}