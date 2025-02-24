using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SharingPlatform.Domain.Models;

public sealed class ServerModel
{
    public static ServerModel Empty { get; } = new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        UserId = Guid.Empty.ToString(),
        CreatedAt = DateTime.MinValue,
        GuildId = Guid.Empty.ToString()
    };

    public Guid Id { get; set; } 
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public string? About { get; set; }
    public string? PhotoUri { get; set; }
    public string InviteUri { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public bool Visible { get; set; }
    public string UserId { get; set; } = default!;
    public string GuildId { get; set; } = default!;
    public List<TagModel> Tags { get; set; } = default!;
    public List<RatingModel> Ratings { get; set; } = default!;
	public MembersInfoModel MembersInfo { get; set; } = default!;

	[ForeignKey(nameof(UserId))] 
	public IdentityUser User { get; set; } = default!;

    public double Rating => Ratings.Count == 0
		? 0
		: Ratings.Average(rating => rating.Value);

	public static ServerModel Create(
        string name,
        string? description,
        string? about,
        string? photoUri,
        string inviteUri,
        string userId,
        string guildId,
        MembersInfoModel membersInfo,
        IEnumerable<TagModel>? tags = null,
        bool visible = false)
    {
        return new ServerModel
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
			About = about,
			PhotoUri = photoUri,
            Visible = visible,
            UserId = userId,
            InviteUri = inviteUri,
			GuildId = guildId,
            MembersInfo = membersInfo,
            Tags = tags?.ToList() ?? [],
            CreatedAt = DateTime.UtcNow
        };
    }

    public static ServerModel Create(
        Guid id,
        string name,
        string? description,
        string? about,
        string? photoUri,
        string inviteUri,
		string userId,
        string guildId,
        MembersInfoModel membersInfo,
        IEnumerable<TagModel>? tags = null,
        bool visible = false)
    {
        return new ServerModel
        {
            Id = id,
            Name = name,
            Description = description,
			About = about,
			PhotoUri = photoUri,
			InviteUri = inviteUri,
			Visible = visible,
            UserId = userId,
            GuildId = guildId,
            MembersInfo = membersInfo,
            Tags = tags?.ToList() ?? [],
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(ServerModel model)
    {
        About = model.About;
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
            Id = Guid.NewGuid(),
            Online = online,
            Total = total
        };
    }
}