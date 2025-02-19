using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public record ServerPreviewResponse(
    Guid Id,
    string Name, 
    string? Description,
    string? PhotoUri,
    int MembersTotal,
    int MembersOnline,
	bool Visible,
    IEnumerable<string> Tags)
{
    public static ServerPreviewResponse FromModel(ServerModel server)
    {
        return new ServerPreviewResponse(
            server.Id,
            server.Name,
            server.Description,
            server.PhotoUri,
            server.MembersInfo.Total,
			server.MembersInfo.Online,
			server.Visible,
            server.Tags.Select(tag => tag.Name));
    }
}