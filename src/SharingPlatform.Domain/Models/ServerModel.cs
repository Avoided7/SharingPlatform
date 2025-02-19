namespace SharingPlatform.Domain.Models;

public sealed class ServerModel
{
    public static ServerModel Empty { get; } = new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        UserId = Guid.Empty.ToString(),
        CreatedAt = DateTime.MinValue,
        ServerId = Guid.Empty.ToString()
    };

    public Guid Id { get; set; } 
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhotoUri { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Visible { get; set; }
    public string UserId { get; set; } = default!;
    public string ServerId { get; set; } = default!;
    public List<TagModel> Tags { get; set; } = default!;
    public MembersInfoModel MembersInfo { get; set; } = default!;

    public static ServerModel Create(
        string name,
        string? description,
        string? photoUri,
        string userId,
        string serverId,
        MembersInfoModel membersInfo,
        IEnumerable<TagModel>? tags = null,
        bool visible = false)
    {
        return new ServerModel
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            PhotoUri = photoUri,
            Visible = visible,
            UserId = userId,
            ServerId = serverId,
            MembersInfo = membersInfo,
            Tags = tags?.ToList() ?? [],
            CreatedAt = DateTime.UtcNow
        };
    }

    public static ServerModel Create(
        Guid id,
        string name,
        string? description,
        string? photoUri,
        string userId,
        string serverId,
        MembersInfoModel membersInfo,
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
            ServerId = serverId,
            MembersInfo = membersInfo,
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

public sealed class MembersInfoModel
{
	public Guid Id { get; set; }    
    public int Online { get; set; }
    public int Total { get; set; }

    public static MembersInfoModel Create(int online, int total)
    {
        return new MembersInfoModel
        {
            Online = online,
            Total = total
        };
    }
}