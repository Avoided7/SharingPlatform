using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Domain.Constants;
using SharingPlatform.WebApi.Core.Servers.Requests;
using SharingPlatform.WebApi.Core.Servers.Responses;
using SharingPlatform.WebApi.Extensions;
using SharingPlatform.WebApi.Models.Requests;

namespace SharingPlatform.WebApi.Core.Servers;

[ApiController]
[Route("api/servers")]
public sealed class ServersController(IServersService serversService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get(
        [FromQuery] GetServersRequest request)
    {
        var paginatedServers = serversService.GetOnlyVisible(request.Page, request.PageSize, request.TagsIds);
        var response = paginatedServers.ChangeType(ServerPreviewResponse.FromModel);
        
        return Ok(response);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetDetails(
	    [FromQuery] GetServerDetailsRequest request)
    {
        var server = await serversService.GetByIdAsync(request.ServerId);
        var response = ServerDetailsResponse.FromModel(server);

        return Ok(response);
    }

    [Authorize(Roles = Roles.Admin), HttpGet("all")]
    public IActionResult GetAll(
		[FromQuery] GetServersRequest request)
	{
		var paginatedServers = serversService.Get(request.Page, request.PageSize);
		var response = paginatedServers.ChangeType(ServerPreviewResponse.FromModel);

		return Ok(response);
	}

	[Authorize, HttpGet("owned")]
    public IActionResult GetUserOwned(
        [FromQuery] PaginatedRequest request)
    {
        var userId = HttpContext.GetUserId();
        var paginatedServers = serversService.GetUserOwned(userId, request.Page, request.PageSize);
        var response = paginatedServers.ChangeType(ServerPreviewResponse.FromModel);

		return Ok(response);
    }

    [Authorize, HttpGet("favourites")]
    public IActionResult GetUserFavourites(
		[FromQuery] PaginatedRequest request)
	{
		var userId = HttpContext.GetUserId();
		var paginatedServers = serversService.GetUserFavourites(userId, request.Page, request.PageSize);
		var response = paginatedServers.ChangeType(ServerPreviewResponse.FromModel);

		return Ok(response);
	}

	[Authorize, HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateServerRequest request)
    {
        var userId = HttpContext.GetUserId();
        var server = await serversService.AddFromInviteLinkAsync(request.InviteUri, userId);
        
        return Ok(server.Id);
    }
    
    [Authorize, HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateServerRequest request)
    {
        var userId = HttpContext.GetUserId();
        var server = request.ToModel(userId);

        await serversService.UpdateAsync(server);
        
        return NoContent();
    }

    [Authorize, HttpDelete]
    public async Task<IActionResult> Remove(
        [FromBody] DeleteServerRequest request)
    {
        var userId = HttpContext.GetUserId();
        var server = request.ToModel(userId);

        await serversService.DeleteAsync(server);
        
        return NoContent();
    }
}