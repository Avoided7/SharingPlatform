using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Domain.Constants;
using SharingPlatform.WebApi.Core.Users.Requests;
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
        var user = await userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            return NotFound();
        }
        
        var isCorrectPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword)
        {
            return Unauthorized();
        }

        var roles = await userManager.GetRolesAsync(user);

        var token = tokenFactory.GenerateToken(user.Id, roles[0]);
        var response = new TokenResponse(token);
        
        return Ok(response);
    }
    
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpRequest request)
    {
        var user = new IdentityUser
        {
            UserName = request.Username,
            Email = request.Email
        };
        
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await userManager.AddToRoleAsync(user, Roles.Default);

        var token = tokenFactory.GenerateToken(user.Id, Roles.Default);
        var response = new TokenResponse(token);
        
        return Ok(response);
    }
}