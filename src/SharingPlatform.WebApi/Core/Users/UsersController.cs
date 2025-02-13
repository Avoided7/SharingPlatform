using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.WebApi.Core.Users.Responses;

namespace SharingPlatform.WebApi.Core.Users;

[ApiController]
[Route("api/users")]
public sealed class UsersController(
    UserManager<IdentityUser> userManager,
    ITokenFactory tokenFactory): ControllerBase
{
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return NotFound();
        }
        
        var isCorrectPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword)
        {
            return Unauthorized();
        }

        var token = tokenFactory.GenerateToken(user.Id);
        var response = new TokenResponse(token);
        
        return Ok(response);
    }
    
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpRequest request)
    {
        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
        };
        
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var token = tokenFactory.GenerateToken(user.Id);
        var response = new TokenResponse(token);
        
        return Ok(response);
    }
}