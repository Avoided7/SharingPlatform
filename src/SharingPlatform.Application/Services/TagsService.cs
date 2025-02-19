using SharingPlatform.Application.Abstractions;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.Domain.Helpers;
using SharingPlatform.Domain.Models;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Application.Services;

internal sealed class TagsService(ApplicationDbContext dbContext) : ITagsService
{
    public PaginatedList<TagModel> Get(int page, int pageSize)
    {
		var tags = dbContext.Tags;
        var result = PaginatedList.From(tags, page, pageSize);

		return result;
    }

    public async Task CreateAsync(TagModel tag)
    {
        await dbContext.Tags.AddAsync(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TagModel tag)
    {
        var entity = await dbContext.Tags.FindAsync(tag.Id);

        if (entity is null)
        {
            NotFoundException.ThrowFromModel(typeof(TagModel));
        }
        
        entity.Update(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid tagId)
    {
        var entity = await dbContext.Tags.FindAsync(tagId);
        
        if (entity is null)
        {
	        NotFoundException.ThrowFromModel(typeof(TagModel));
		}

		dbContext.Tags.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}