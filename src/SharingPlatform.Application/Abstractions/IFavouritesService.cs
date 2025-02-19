using SharingPlatform.Domain.Models;

namespace SharingPlatform.Application.Abstractions;

public interface IFavouritesService
{
	Task CreateAsync(FavouriteModel favourite);
	Task DeleteAsync(FavouriteModel favourite);
}