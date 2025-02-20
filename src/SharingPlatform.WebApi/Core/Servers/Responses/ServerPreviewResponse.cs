using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public sealed record ServerPreviewResponse(
    Guid Id,
    string Name, 
    string? PhotoUri,
    string InviteUri,
	int MembersTotal,
    int MembersOnline,
    double Rating,
	bool Visible,
    bool IsFavourite)
{
    public static ServerPreviewResponse FromModel(ServerModel server)
    {
		var rating = double.Round(server.Rating, 1);

		return new ServerPreviewResponse(
            server.Id,
            server.Name,
            server.PhotoUri,
            server.InviteUri,
            server.MembersInfo.Total,
			server.MembersInfo.Online,
			rating,
			server.Visible,
            false);
    }
}