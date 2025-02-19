using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Application.Dtos;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.Domain.Helpers;
using SharingPlatform.Domain.Models;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Application.Services;

internal sealed class ServersService(
    ApplicationDbContext dbContext,
    HttpClient client) : IServersService
{
    public PaginatedList<ServerModel> Get(int page, int pageSize)
    {
        var servers = dbContext.Servers
	        .Include(server => server.Tags)
	        .Include(server => server.MembersInfo);

        var result = PaginatedList.From(servers, page, pageSize);

		return result;
    }

    public PaginatedList<ServerModel> GetOnlyVisible(int page, int pageSize)
    {
        var servers = dbContext.Servers
            .Include(server => server.Tags)
			.Include(server => server.MembersInfo)
			.Where(server => server.Visible);

        var result = PaginatedList.From(servers, page, pageSize);
        
        return result;
    }

    public PaginatedList<ServerModel> GetUserOwned(string userId, int page, int pageSize)
    {
	    var servers = dbContext.Servers
		    .Where(server => server.UserId == userId)
		    .Include(server => server.Tags)
		    .Include(server => server.MembersInfo);

	    var result = PaginatedList.From(servers, page, pageSize);

		return result;
    }

    public PaginatedList<ServerModel> GetUserFavourites(string userId, int page, int pageSize)
    {
	    var servers = dbContext.Favourites
		    .Include(favourite => favourite.Server.Tags)
		    .Include(favourite => favourite.Server.MembersInfo)
		    .Select(favourite => favourite.Server);

		var result = PaginatedList.From(servers, page, pageSize);

		return result;
	}

	public async Task<ServerModel> AddFromInviteLinkAsync(string inviteLink, string userId)
    {
        var data = await GetServerDataAsync(inviteLink);
        var details = data.Details!;
        
        var photoUri = GetPhotoUri(details.Id, details.IconHash);

        var membersInfo = MembersInfoModel.Create(data.MembersOnline, data.MembersTotal);

        var model = ServerModel.Create(
            details.Name, 
            details.Description, 
            photoUri,
            userId,
            details.Id,
            membersInfo);
        
        await dbContext.Servers.AddAsync(model);
        await dbContext.SaveChangesAsync();

        return model;
    }
    
    public async Task UpdateAsync(ServerModel server)
    {
        var entity = await dbContext.Servers
            .Include(entity => entity.Tags)
            .FirstOrDefaultAsync(entity => entity.Id == server.Id);

        if (entity is null || entity.UserId != server.UserId)
        {
            NotFoundException.ThrowFromModel(typeof(ServerModel));
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

    public async Task DeleteAsync(Guid serverId, string userId)
    {
        var entity = await dbContext.Servers.FindAsync(serverId);

        if (entity is null || entity.UserId != userId)
        {
			NotFoundException.ThrowFromModel(typeof(ServerModel));
		}

        dbContext.Servers.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
    
    private async Task<ServerDataResponse> GetServerDataAsync(string inviteLink)
    {
        var code = inviteLink.Split('/')[^1];
        var link = $"https://discord.com/api/v10/invites/{code}?with_counts=true";

        var response = await client.GetAsync(link);

        if (!response.IsSuccessStatusCode)
        {
            IncorrectResponseException.Throw();
        }

        var content = await response.Content.ReadFromJsonAsync<ServerDataResponse>();

        if (!ServerDataResponse.IsValid(content))
        {
            ValidationException.Throw();
        }

        return content!;
    }

    private static string GetPhotoUri(
        string serverId,
        string iconHash) => $"https://cdn.discordapp.com/icons/{serverId}/{iconHash}.png";
}