using SharingPlatform.Domain.Models;

namespace SharingPlatform.Application.Abstractions;

public interface IRatingsService
{
	Task RateAsync(RatingModel rating);
	Task DeleteAsync(RatingModel rating);
}