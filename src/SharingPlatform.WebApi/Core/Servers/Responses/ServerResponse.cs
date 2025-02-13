using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Responses;

public sealed record ServerResponse(string Id, string Name, string? Description, string? PhotoUri, DateTime CreatedAt)
{
    public static ServerResponse FromModel(ServerModel server)
    {
        return new ServerResponse(
            server.Id, 
            server.Name, 
            server.Description,
            server.PhotoUri, 
            server.CreatedAt);
    }
};