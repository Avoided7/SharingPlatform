using SharingPlatform.Application.Abstractions;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.Domain.Models;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Application.Services;

internal sealed class TagsService(ApplicationDbContext dbContext) : ITagsService
{
    public IQueryable<TagModel> GetTags()
    {
        return dbContext.Tags;
    }

    public async Task CreateTagAsync(TagModel tag)
    {
        await dbContext.Tags.AddAsync(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateTagAsync(TagModel tag)
    {
        var entity = await dbContext.Tags.FindAsync(tag.Id);

        if (entity is null)
        {
            throw new TagNotFoundException();
        }
        
        entity.Update(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteTagAsync(Guid tagId)
    {
        var entity = await dbContext.Tags.FindAsync(tagId);
        
        if (entity is null)
        {
            throw new TagNotFoundException();
        }
        
        dbContext.Tags.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}