using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Favourites.Requests;

public sealed record RemoveFavouriteRequest(Guid ServerId)
{
	public FavouriteModel ToModel(string userId)
	{
		return FavouriteModel.Create(ServerId, userId);
	}
}