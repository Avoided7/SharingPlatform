namespace SharingPlatform.Application.Settings;

public sealed record JwtSettings(
    string SecretKey,
    string Audience,
    string Issuer);