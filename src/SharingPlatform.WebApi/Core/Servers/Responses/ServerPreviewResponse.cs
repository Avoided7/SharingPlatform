using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public class ServerPreviewResponse
{
	protected ServerPreviewResponse(
		Guid id,
		string name,
		string? photoUri,
		string inviteUri,
		IEnumerable<string> tags,
		int membersTotal,
		int membersOnline,
		double rating,
		int votersCount,
		bool visible,
		bool isFavourite)
	{
		Id = id;
		Name = name;
		PhotoUri = photoUri;
		InviteUri = inviteUri;
		Tags = tags;
		MembersTotal = membersTotal;
		MembersOnline = membersOnline;
		Rating = rating;
		VotersCount = votersCount;
		Visible = visible;
		IsFavourite = isFavourite;
	}

	public Guid Id { get; }
	public string Name { get; }
	public string? PhotoUri { get; }
	public string InviteUri { get; }
	public IEnumerable<string> Tags { get; }
	public int MembersTotal { get; }
	public int MembersOnline { get; }
	public double Rating { get; }
	public int VotersCount { get; }
	public bool Visible { get; }
	public bool IsFavourite { get; }
	
	public static ServerPreviewResponse FromModel(ServerModel server)
	{
		var rating = Math.Round(server.Rating, 1);
		var tags = server.Tags.Select(tag => tag.Name).ToArray();

		return new ServerPreviewResponse(
			server.Id,
			server.Name,
			server.PhotoUri,
			server.InviteUri,
			tags,
			server.MembersInfo.Total,
			server.MembersInfo.Online,
			rating,
			server.Ratings.Count,
			server.Visible,
			false);
	}
}