namespace SharingPlatform.Application.Abstractions;

public interface ITokenFactory
{
    string GenerateAccessToken(string userId, string username, string accessToken);
}