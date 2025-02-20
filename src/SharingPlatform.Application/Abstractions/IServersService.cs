using SharingPlatform.Domain.Helpers;
using SharingPlatform.Domain.Models;

namespace SharingPlatform.Application.Abstractions;

public interface IServersService
{
    PaginatedList<ServerModel> Get(int page, int pageSize);
    PaginatedList<ServerModel> GetOnlyVisible(int page, int pageSize);
    PaginatedList<ServerModel> GetUserOwned(string userId, int page, int pageSize);
    PaginatedList<ServerModel> GetUserFavourites(string userId, int page, int pageSize);
    Task<ServerModel> GetByIdAsync(Guid serverId);
	Task<ServerModel> AddFromInviteLinkAsync(string inviteLink, string userId);
    Task UpdateAsync(ServerModel server);
    Task DeleteAsync(ServerModel server);
}