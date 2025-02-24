using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Requests;

public sealed record UpdateServerRequest(Guid Id, string? Description, string? About, bool Visible, Guid[] TagsId)
{
    public ServerModel ToModel(string userId)
    {
        return ServerModel.Create(
            Id,
            string.Empty,
            Description,
            About,
            string.Empty,
            string.Empty,
            userId,
            string.Empty,
            null!,
            TagsId.Select(id => TagModel.Create(id, string.Empty)),
            visible: Visible);
    }
}
