using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public sealed record ServerPreviewResponse(
    string Id,
    string Name, 
    string? Description, 
    string? PhotoUri, 
    IEnumerable<string> Tags)
{
    public static ServerPreviewResponse FromModel(ServerModel server)
    {
        return new ServerPreviewResponse(
            server.Id,
            server.Name,
            server.Description,
            server.PhotoUri,
            server.Tags.Select(tag => tag.Name));
    }
}