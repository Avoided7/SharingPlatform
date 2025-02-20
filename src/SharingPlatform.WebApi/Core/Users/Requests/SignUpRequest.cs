namespace SharingPlatform.WebApi.Core.Users.Requests;

public sealed record SignUpRequest(string Username, string Email, string Password);