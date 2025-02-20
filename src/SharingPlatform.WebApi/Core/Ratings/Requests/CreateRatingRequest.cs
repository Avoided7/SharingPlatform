using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Ratings.Requests;

public sealed record CreateRatingRequest(Guid ServerId, double Rating)
{
	public RatingModel ToModel(string userId)
	{
		return RatingModel.Create(Rating, userId, ServerId);
	}
}