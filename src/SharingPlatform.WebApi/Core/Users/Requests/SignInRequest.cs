namespace SharingPlatform.WebApi.Core.Users;

public sealed record SignInRequest(string Email, string Password);