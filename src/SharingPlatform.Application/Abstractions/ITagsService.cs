using SharingPlatform.Domain.Models;

namespace SharingPlatform.Application.Abstractions;

public interface ITagsService
{
    IQueryable<TagModel> GetTags();
    Task CreateTagAsync(TagModel tag);
    Task UpdateTagAsync(TagModel tag);
    Task DeleteTagAsync(Guid tagId);
}