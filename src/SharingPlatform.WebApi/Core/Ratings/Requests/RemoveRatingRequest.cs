using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Ratings.Requests;

public sealed record RemoveRatingRequest(Guid ServerId)
{
	public RatingModel ToModel(string userId)
	{
		return RatingModel.Create(0, null, userId, ServerId);
	}
}