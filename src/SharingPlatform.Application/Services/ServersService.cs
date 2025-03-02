using Microsoft.EntityFrameworkCore;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.Domain.Helpers;
using SharingPlatform.Domain.Models;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Application.Services;

internal sealed class ServersService(
    ApplicationDbContext dbContext,
    IDiscordService discordService) : IServersService
{
    public PaginatedList<ServerModel> Get(int page, int pageSize, IEnumerable<Guid>? tagsIds = null)
    {
	    var servers = GetOrderedServers(dbContext.Servers);

	    if (tagsIds is not null)
	    {
		    servers = GetServersWithTags(servers, tagsIds);
	    }

        var result = PaginatedList.From(servers, page, pageSize);

		return result;
    }

    public PaginatedList<ServerModel> GetOnlyVisible(int page, int pageSize, IEnumerable<Guid>? tagsIds = null)
    {
        var servers = GetOrderedServers(dbContext.Servers).Where(server => server.Visible);

        if (tagsIds is not null)
        {
	        servers = GetServersWithTags(servers, tagsIds);
        }

        var result = PaginatedList.From(servers, page, pageSize);
        
        return result;
    }

    public PaginatedList<ServerModel> GetUserOwned(string userId, int page, int pageSize)
    {
	    var servers = GetOrderedServers(dbContext.Servers.Where(server => server.UserId == userId));

	    var result = PaginatedList.From(servers, page, pageSize);

		return result;
    }

    public PaginatedList<ServerModel> GetUserFavourites(string userId, int page, int pageSize)
    {
	    var servers = GetOrderedServers(dbContext.Favourites.Select(favourite => favourite.Server));

		var result = PaginatedList.From(servers, page, pageSize);

		return result;
	}

    public async Task<ServerModel> GetByIdAsync(Guid serverId)
	{
		var server = await dbContext.Servers
			.Include(server => server.User)
			.Include(server => server.Ratings)
				.ThenInclude(rating => rating.User)
			.FirstOrDefaultAsync(server => server.Id == serverId);

		if(server is null)
		{
			NotFoundException.ThrowFromModel(typeof(ServerModel));
		}

		return server;
	}

	public async Task<ServerModel> AddFromInviteLinkAsync(string inviteLink, string userId)
    {
        var data = await discordService.GetServerDataAsync(inviteLink);
        var details = data.Details!;

        var existedEntity = await dbContext.Servers.AnyAsync(server => server.GuildId == details.GuildId);

        if (existedEntity)
        {
            AlreadyExistException.ThrowFromModel(nameof(ServerModel));
		}

        var model = data.ToModel(inviteLink, userId);
        
        await dbContext.Servers.AddAsync(model);
        await dbContext.SaveChangesAsync();

        return model;
    }
    
    public async Task UpdateAsync(ServerModel server)
    {
        var entity = await dbContext.Servers
            .FirstOrDefaultAsync(entity => entity.Id == server.Id);

        if (entity is null)
        {
            NotFoundException.ThrowFromModel(typeof(ServerModel));
        }

        if (entity.UserId != server.UserId)
        {
            ValidationException.Throw();
        }
        
        var tags = new List<TagModel>();
        foreach (var tag in server.Tags)
        {
            var existedTag = await dbContext.Tags.FindAsync(tag.Id);

            if (existedTag is null)
            {
                NotFoundException.ThrowFromModel(typeof(TagModel));
            }
            
            tags.Add(existedTag);
        }

        entity.Update(server);
        entity.SetTags(tags);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ServerModel server)
    {
        var entity = await dbContext.Servers.FindAsync(server.Id);

        if (entity is null)
        {
			NotFoundException.ThrowFromModel(typeof(ServerModel));
		}

        if (entity.UserId != server.UserId)
        {
            ValidationException.Throw();
        }

        dbContext.Servers.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

	private static IQueryable<ServerModel> GetOrderedServers(IQueryable<ServerModel> servers) =>
        servers.OrderByDescending(server => server.CreatedAt);
    
    private static IQueryable<ServerModel> GetServersWithTags(IQueryable<ServerModel> servers, IEnumerable<Guid> tagsIds)
	{
		foreach(var tagId in tagsIds)
		{
			servers = servers.Where(server => server.Tags.Any(tag => tag.Id == tagId));
		}

		return servers;
	}

    private static string GetPhotoUri(
        string serverId,
        string iconHash) => $"https://cdn.discordapp.com/icons/{serverId}/{iconHash}.png";
}