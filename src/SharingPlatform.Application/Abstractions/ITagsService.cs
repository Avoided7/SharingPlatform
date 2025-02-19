using SharingPlatform.Domain.Helpers;
using SharingPlatform.Domain.Models;

namespace SharingPlatform.Application.Abstractions;

public interface ITagsService
{
    PaginatedList<TagModel> Get(int page, int pageSize);
    Task CreateAsync(TagModel tag);
    Task UpdateAsync(TagModel tag);
    Task DeleteAsync(Guid tagId);
}