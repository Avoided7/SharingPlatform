using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Favourites.Requests;

public sealed record CreateFavouriteRequest(Guid ServerId)
{
	public FavouriteModel ToModel(string userId)
	{
		return FavouriteModel.Create(ServerId, userId);
	}
}