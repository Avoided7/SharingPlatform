namespace SharingPlatform.Application.Abstractions;

public interface ITokenFactory
{
    string GenerateToken(string userId, string roleName);
}