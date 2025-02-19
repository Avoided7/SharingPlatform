using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public sealed record ServerResponse(
	Guid Id, 
	string Name, 
	string? Description, 
	string? PhotoUri, 
    int MembersTotal,
    int MembersOnline,
	bool Visible,
	IEnumerable<string> Tags,
	DateTime CreatedAt)
{
    public static ServerResponse FromModel(ServerModel server)
    {
        return new ServerResponse(
            server.Id, 
            server.Name, 
            server.Description,
            server.PhotoUri,
			server.MembersInfo.Total,
			server.MembersInfo.Online,
			server.Visible,
			server.Tags.Select(tag => tag.Name),
			server.CreatedAt);
    }
};