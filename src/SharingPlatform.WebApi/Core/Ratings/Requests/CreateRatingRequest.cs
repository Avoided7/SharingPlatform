using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Ratings.Requests;

public sealed record CreateRatingRequest(string? Comment, Guid ServerId, double Rating)
{
	public RatingModel ToModel(string userId)
	{
		return RatingModel.Create(Rating, Comment, userId, ServerId);
	}
}