using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public sealed class ServerDetailsResponse : ServerPreviewResponse
{
	private ServerDetailsResponse(
		Guid id,
		string name,
		string? photoUri,
		string inviteUri,
		int membersTotal,
		int membersOnline,
		double rating,
		int votersCount,
		bool visible,
		bool isFavourite,
		string? description,
		string? about,
		string ownerUsername,
		List<Voter> voters,
		IEnumerable<string> tags,
		DateTime createdAt)
		: base(id, name, photoUri, inviteUri, tags, membersTotal, membersOnline, rating, votersCount, visible, isFavourite)
	{
		Description = description;
		About = about;
		OwnerUsername = ownerUsername;
		Voters = voters;
		CreatedAt = createdAt;
	}
	
	public string? Description { get; }
	public string? About { get; }
	public string OwnerUsername { get; }
	public DateTime CreatedAt { get; }
	public List<Voter> Voters { get; }

	public new static ServerDetailsResponse FromModel(ServerModel server)
	{
		var rating = Math.Round(server.Rating, 1);
		var voters = server.Ratings
			.Select(rating => new Voter(rating.Value, rating.Comment, rating.User.UserName!))
			.ToList();
		var tags = server.Tags.Select(tag => tag.Name);

		return new ServerDetailsResponse(
			server.Id,
			server.Name,
			server.PhotoUri,
			server.InviteUri,
			server.MembersInfo.Total,
			server.MembersInfo.Online,
			rating,
			server.Ratings.Count,
			server.Visible,
			false,
			server.Description,
			server.About,
			server.User.UserName!,
			voters,
			tags,
			server.CreatedAt);
	}
}

public sealed record Voter(double Value, string? Comment, string Username);