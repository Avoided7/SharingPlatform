using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.WebApi.Core.Ratings.Requests;
using SharingPlatform.WebApi.Extensions;

namespace SharingPlatform.WebApi.Core.Ratings;

[ApiController]
[Route("api/ratings")]
public sealed class RatingsController(IRatingsService ratingsService) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> Create(
		[FromBody] CreateRatingRequest request)
	{
		var userId = HttpContext.GetUserId();
		var model = request.ToModel(userId);

		await ratingsService.RateAsync(model);

		return NoContent();
	}

	[HttpDelete]
	public async Task<IActionResult> Remove(
		[FromBody] RemoveRatingRequest request)
	{
		var userId = HttpContext.GetUserId();
		var model = request.ToModel(userId);

		await ratingsService.DeleteAsync(model);

		return NoContent();
	}
}