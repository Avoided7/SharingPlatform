using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Tags.Requests;

public sealed record UpdateTagRequest(Guid Id, string Name)
{
    public TagModel ToModel()
    {
        return TagModel.Create(Id, Name);
    }
}