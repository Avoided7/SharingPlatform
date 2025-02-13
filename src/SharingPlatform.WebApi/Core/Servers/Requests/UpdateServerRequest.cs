using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Requests;

public sealed record UpdateServerRequest(string Id, string? Description, bool Visible, Guid[] TagsId)
{
    public ServerModel ToModel(string userId)
    {
        return ServerModel.Create(
            Id,
            string.Empty,
            Description,
            string.Empty,
            userId,
            TagsId.Select(id => TagModel.Create(id, string.Empty)),
            visible: Visible);
    }
}
