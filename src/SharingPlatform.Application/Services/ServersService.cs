using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Application.Dtos;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.Domain.Models;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Application.Services;

internal sealed class ServersService(
    ApplicationDbContext dbContext,
    HttpClient client) : IServersService
{
    public IQueryable<ServerModel> GetServers()
    {
        return dbContext.Servers.Include(server => server.Tags);
    }

    public IQueryable<ServerModel> GetUserServers(string userId)
    {
        return dbContext.Servers.Where(server => server.UserId == userId).Include(server => server.Tags);
    }

    public async Task AddServerFromInviteLinkAsync(string inviteLink, string userId)
    {
        var data = await GetServerDataAsync(inviteLink);
        var details = data.Details!;
        
        var iconUrl = GetIconUrl(details.Id, details.IconHash);

        var model = ServerModel.Create(
            details.Id, 
            details.Name, 
            details.Description, 
            iconUrl,
            userId);
        
        await dbContext.Servers.AddAsync(model);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateServerAsync(ServerModel server)
    {
        var entity = await dbContext.Servers
            .Include(entity => entity.Tags)
            .FirstOrDefaultAsync(entity => entity.Id == server.Id);

        if (entity is null || entity.UserId != server.UserId)
        {
            throw new ServerNotFoundException();
        }
        
        var tags = new List<TagModel>();
        foreach (var tag in server.Tags)
        {
            var existedTag = await dbContext.Tags.FindAsync(tag.Id);

            if (existedTag is null)
            {
                throw new TagNotFoundException();
            }
            
            tags.Add(existedTag);
        }

        entity.Update(server);
        entity.SetTags(tags);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteServerAsync(string id, string userId)
    {
        var entity = await dbContext.Servers.FindAsync(id);

        if (entity is null || entity.UserId != userId)
        {
            throw new ServerNotFoundException();
        }

        dbContext.Servers.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
    
    private async Task<ServerDataResponse> GetServerDataAsync(string inviteLink)
    {
        var code = inviteLink.Split('/')[^1];
        var link = $"https://discordapp.com/api/v9/invites/{code}";

        var response = await client.GetAsync(link);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(); // TODO: Create custom exception
        }

        var content = await response.Content.ReadFromJsonAsync<ServerDataResponse>();

        if (!ServerDataResponse.IsValid(content))
        {
            throw new Exception();
        }

        return content!;
    }

    private string GetIconUrl(
        string serverId,
        string iconHash) => $"https://cdn.discordapp.com/icons/{serverId}/{iconHash}.png";
}