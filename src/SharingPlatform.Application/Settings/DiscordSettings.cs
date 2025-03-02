namespace SharingPlatform.Application.Settings;

public sealed record DiscordSettings(
	string ClientId,
	string ClientSecret,
	string RedirectUri);