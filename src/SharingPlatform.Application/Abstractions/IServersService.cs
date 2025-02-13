using SharingPlatform.Domain.Models;

namespace SharingPlatform.Application.Abstractions;

public interface IServersService
{
    IQueryable<ServerModel> GetServers();
    IQueryable<ServerModel> GetUserServers(string userId);
    Task AddServerFromInviteLinkAsync(string inviteLink, string userId);
    Task UpdateServerAsync(ServerModel server);
    Task DeleteServerAsync(string id, string userId);
}