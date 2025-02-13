namespace SharingPlatform.Domain.Models;

public sealed class ServerModel
{
    public static ServerModel Empty { get; } = new()
    {
        Id = Guid.Empty.ToString(),
        Name = string.Empty,
        UserId = Guid.Empty.ToString(),
        CreatedAt = DateTime.MinValue,
    };

    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhotoUri { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Visible { get; set; }
    public string UserId { get; set; } = default!;
    public List<TagModel> Tags { get; set; } = default!;

    public static ServerModel Create(
        string id,
        string name,
        string? description,
        string? photoUri,
        string userId,
        IEnumerable<TagModel>? tags = null,
        bool visible = false)
    {
        return new ServerModel
        {
            Id = id,
            Name = name,
            Description = description,
            PhotoUri = photoUri,
            Visible = visible,
            UserId = userId,
            Tags = tags?.ToList() ?? [],
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(ServerModel model)
    {
        Description = model.Description;
        Visible = model.Visible;
    }

    public void SetTags(IEnumerable<TagModel> tags)
    {
        Tags.Clear();
        Tags.AddRange(tags);
    }
}