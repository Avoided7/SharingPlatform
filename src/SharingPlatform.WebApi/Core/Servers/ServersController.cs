using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.WebApi.Core.Requests;
using SharingPlatform.WebApi.Core.Responses;
using SharingPlatform.WebApi.Core.Servers.Requests;
using SharingPlatform.WebApi.Core.Servers.Responses;
using SharingPlatform.WebApi.Extensions;

namespace SharingPlatform.WebApi.Core.Servers;

[ApiController]
[Route("api/servers")]
public sealed class ServersController(
    IServersService serversService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] PaginatedRequest request)
    {
        var servers = serversService.GetServers();
        var response = PaginatedResponse.From(
            request, 
            servers, 
            ServerPreviewResponse.FromModel);
        
        return Ok(response);
    }

    [Authorize, HttpGet("owned")]
    public async Task<IActionResult> GetUserServers(
        [FromQuery] PaginatedRequest request)
    {
        var userId = HttpContext.GetUserId();
        var servers = serversService.GetUserServers(userId);
        var response = PaginatedResponse.From(request, servers, ServerPreviewResponse.FromModel);
        
        return Ok(response);
    }
    
    [Authorize, HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateServerRequest request)
    {
        var userId = HttpContext.GetUserId();
        await serversService.AddServerFromInviteLinkAsync(request.InviteUri, userId);
        
        return NoContent();
    }
    
    [Authorize, HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateServerRequest request)
    {
        var userId = HttpContext.GetUserId();
        var server = request.ToModel(userId);
        await serversService.UpdateServerAsync(server);
        
        return NoContent();
    }

    [Authorize, HttpDelete]
    public async Task<IActionResult> Remove(
        [FromBody] DeleteServerRequest request)
    {
        var userId = HttpContext.GetUserId();
        await serversService.DeleteServerAsync(request.Id, userId);
        
        return NoContent();
    }
}

