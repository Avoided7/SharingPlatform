namespace SharingPlatform.Application.Dtos;

public sealed record DiscordUserData(
	string Id,
	string Email,
	string Avatar,
	string Username);