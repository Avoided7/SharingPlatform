using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Application.Dtos;

namespace SharingPlatform.WebApi.Core.Users;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController(
	IDiscordService discordService,
	ITokenFactory tokenFactory): ControllerBase
{
    [HttpGet("sign-in")]
    public IActionResult SignIn()
    {
	    var authorizeLink = discordService.GetAuthorizeLink();

		return Redirect(authorizeLink);
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshToken(
		[FromQuery] string? refreshToken)
    {
	    return await GetAccessTokenAsync(discordService.GetTokenDataFromRefreshTokenAsync, refreshToken);
    }

	[HttpGet("discord-callback")]
	public async Task<IActionResult> Callback(
		[FromQuery] string code)
	{
		return await GetAccessTokenAsync(discordService.GetTokenDataAsync, code);
	}

	private async Task<IActionResult> GetAccessTokenAsync(
		Func<string, Task<DiscordTokenData?>> getTokenData,
		string? token)
	{
		if(string.IsNullOrEmpty(token))
		{
			return BadRequest();
		}

		var tokenData = await getTokenData(token);

		if(tokenData is null)
		{
			return Unauthorized();
		}

		var userInfo = await discordService.GetUserDataAsync(tokenData.AccessToken);

		if(userInfo is null)
		{
			return BadRequest();
		}

		var accessToken = tokenFactory.GenerateAccessToken(
			userInfo.Id,
			userInfo.Username,
			tokenData.AccessToken);

		return Ok(new { AccessToken = accessToken, tokenData.RefreshToken });
	}
}