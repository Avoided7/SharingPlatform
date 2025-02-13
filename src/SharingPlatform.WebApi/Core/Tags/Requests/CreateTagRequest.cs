using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Tags.Requests;

public sealed record CreateTagRequest(string Name)
{
    public TagModel ToModel()
    {
        return TagModel.Create(Name);
    }
}