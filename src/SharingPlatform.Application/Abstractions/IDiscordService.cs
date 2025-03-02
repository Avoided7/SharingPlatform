using SharingPlatform.Application.Dtos;

namespace SharingPlatform.Application.Abstractions;

public interface IDiscordService
{
	Task<DiscordTokenData?> GetTokenDataAsync(string authorizeCode);
	Task<DiscordTokenData?> GetTokenDataFromRefreshTokenAsync(string refreshToken);
	Task<DiscordUserData?> GetUserDataAsync(string discordAccessToken);
	Task<DiscordServerData> GetServerDataAsync(string inviteLink);
	string GetAuthorizeLink();
}