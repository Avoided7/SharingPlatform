using Microsoft.EntityFrameworkCore;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.Domain.Models;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Application.Services;

internal sealed class FavouritesService(ApplicationDbContext dbContext) : IFavouritesService
{
	public async Task CreateAsync(FavouriteModel favourite)
	{
		var serverExist = await dbContext.Servers.AnyAsync(server => server.Id == favourite.ServerId);
		if(!serverExist)
		{
			NotFoundException.ThrowFromModel(typeof(ServerModel));
		}

		var userExist = await dbContext.Users.AnyAsync(user => user.Id == favourite.UserId);
		if (!userExist)
		{
			NotFoundException.Throw("User");
		}

		await dbContext.Favourites.AddAsync(favourite);
		await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(FavouriteModel favourite)
	{
		var existed = await dbContext.Favourites.FindAsync(favourite.Id);

		if (existed is null)
		{
			NotFoundException.ThrowFromModel(typeof(FavouriteModel));
		}

		dbContext.Favourites.Remove(existed);
		await dbContext.SaveChangesAsync();
	}
}