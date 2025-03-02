using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http.Extensions;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Application.Dtos;
using SharingPlatform.Application.Settings;
using SharingPlatform.Domain.Exceptions;

namespace SharingPlatform.Application.Services;

public sealed class DiscordService(
	HttpClient client,
	DiscordSettings settings) : IDiscordService
{
	public async Task<DiscordTokenData?> GetTokenDataAsync(string authorizeCode)
	{
		var tokenRequest = new Dictionary<string, string>
		{
			{ "client_id", settings.ClientId },
			{ "client_secret", settings.ClientSecret },
			{ "grant_type", "authorization_code" },
			{ "code", authorizeCode },
			{ "redirect_uri", settings.RedirectUri }
		};

		var tokenResponse = await client.PostAsync(new Uri("oauth2/token", UriKind.Relative), new FormUrlEncodedContent(tokenRequest));

		if(!tokenResponse.IsSuccessStatusCode)
		{
			return null;
		}

		var tokenData = await tokenResponse.Content.ReadFromJsonAsync<DiscordTokenData>();

		return tokenData;
	}

	public async Task<DiscordTokenData?> GetTokenDataFromRefreshTokenAsync(string refreshToken)
	{
		var body = new Dictionary<string, string>
		{
			{ "grant_type", "refresh_token" },
			{ "refresh_token", refreshToken },
			{ "client_id", settings.ClientId },
			{ "client_secret", settings.ClientSecret }
		};

		var response = await client.PostAsync("oauth2/token", new FormUrlEncodedContent(body));

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var tokenInfo = await response.Content.ReadFromJsonAsync<DiscordTokenData>();

		return tokenInfo;
	}

	public async Task<DiscordUserData?> GetUserDataAsync(string discordAccessToken)
	{
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", discordAccessToken);

		var response = await client.GetAsync("users/@me");

		if(!response.IsSuccessStatusCode)
		{
			return null;
		}

		var userResponse = await response.Content.ReadFromJsonAsync<DiscordUserData>();

		return userResponse!;
	}

	public async Task<DiscordServerData> GetServerDataAsync(string inviteLink)
	{
		var code = inviteLink.Split('/')[^1];
		var link = $"api/v10/invites/{code}?with_counts=true";

		var response = await client.GetAsync(link);

		if(!response.IsSuccessStatusCode)
		{
			IncorrectResponseException.Throw();
		}

		var content = await response.Content.ReadFromJsonAsync<DiscordServerData>();

		if(!DiscordServerData.IsValid(content))
		{
			ValidationException.Throw();
		}

		return content!;
	}

	public string GetAuthorizeLink()
	{
		var uri = "https://discord.com/oauth2/authorize";

		var queryBuilder = new QueryBuilder
		{
			{ "grant_type", "authorization_code" },
			{ "client_id", settings.ClientId },
			{ "redirect_uri", settings.RedirectUri },
			{ "response_type", "code" },
			{ "scope", "identify email" }
		};

		var query = queryBuilder.ToQueryString();

		return $"{uri}{query}";
	}
}