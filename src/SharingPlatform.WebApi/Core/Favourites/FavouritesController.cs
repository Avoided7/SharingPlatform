using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.WebApi.Core.Favourites.Requests;
using SharingPlatform.WebApi.Extensions;

namespace SharingPlatform.WebApi.Core.Favourites;

[ApiController]
[Route("api/favourites")]
public class FavouritesController(IFavouritesService favouritesService) : ControllerBase
{
	[Authorize, HttpPost]
	public async Task<IActionResult> Create(
		[FromBody] CreateFavouriteRequest request)
	{
		var userId = HttpContext.GetUserId();
		var favourite = request.ToModel(userId);

		await favouritesService.CreateAsync(favourite);

		return NoContent();
	}

	[Authorize, HttpDelete]
	public async Task<IActionResult> Remove(
		[FromBody] RemoveFavouriteRequest request)
	{
		var userId = HttpContext.GetUserId();
		var favourite = request.ToModel(userId);

		await favouritesService.DeleteAsync(favourite);

		return NoContent();
	}
}