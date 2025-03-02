using System.Text.Json.Serialization;

namespace SharingPlatform.Application.Dtos;

public sealed class DiscordTokenData
{
	[JsonPropertyName("access_token")] public string AccessToken { get; init; } = default!;
	[JsonPropertyName("refresh_token")] public string RefreshToken { get; init; } = default!;
	[JsonPropertyName("expires_in")] public long ExpiresIn { get; init; }
}