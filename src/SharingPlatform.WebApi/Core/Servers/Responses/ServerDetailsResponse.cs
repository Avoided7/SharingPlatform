using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public sealed record ServerDetailsResponse(
	Guid Id, 
	string Name, 
	string? Description, 
	string? PhotoUri, 
	string InviteUri,
    int MembersTotal,
    int MembersOnline,
	double Rating,
	bool Visible,
	IEnumerable<string> Tags,
	DateTime CreatedAt)
{
    public static ServerDetailsResponse FromModel(ServerModel server)
    {
		var rating = double.Round(server.Rating,
			1);

		return new ServerDetailsResponse(
            server.Id, 
            server.Name, 
            server.Description,
            server.PhotoUri,
			server.InviteUri,
			server.MembersInfo.Total,
			server.MembersInfo.Online,
			rating,
			server.Visible,
			server.Tags.Select(tag => tag.Name),
			server.CreatedAt);
    }
};